using SimpleRPG.Hero;
using SimpleRPG.Infrastructure;
using SimpleRPG.Items;
using SimpleRPG.Services;
using SimpleRPG.Services.GameFactory;
using SimpleRPG.Services.PersistentData;
using UnityEngine;

namespace SimpleRPG.Characters
{
    public class CharacterAttacker : GameEntity, ISavable
    {
        [SerializeField] private LayerMask enemyMask;
        [SerializeField] private Unarmed unarmed;
        [SerializeField] private WeaponPositioner weaponPositioner;
        [SerializeField, Range(0, 360)] private float maxAngleRangeAttack = 90f;
        private WeaponModel weaponModel;
        private IGameFactory gameFactory;
        private IStaticDataService staticDataService;
        private readonly Collider[] hitResults = new Collider[5];
        public WeaponItem Weapon { get; private set; }

        private void Awake()
        {
            staticDataService = ServiceLocator.Container.Single<IStaticDataService>();
            gameFactory = ServiceLocator.Container.Single<IGameFactory>();
        }

        public void EquipWeapon(WeaponItem weaponItem)
        {
            Weapon = weaponItem == null ? unarmed : weaponItem;
            if (weaponModel != null)
                weaponModel.gameObject.SetActive(false);
            weaponModel = weaponPositioner.SetWeapon(Weapon);
        }

        public void ShowWeaponModel(bool isActive)
        {
            if (weaponModel != null)
                weaponModel.gameObject.SetActive(isActive);
        }

        public void MeleeAttack()
            => HitEnemies(weaponModel.AttackPoint, weaponModel.RadiusAttack, Weapon.Damage);

        public void RangeAttack()
        {
            int hitCount = Physics.OverlapSphereNonAlloc(weaponModel.AttackPoint, weaponModel.RadiusAttack, hitResults, enemyMask);
            var projectileWeapon = Weapon as LaunchProjctileWeaponItem;
            var projectile = gameFactory.CreateProjectile(projectileWeapon.ProjectileType);
            projectile.transform.position = weaponModel.AttackPoint;

            if (hitCount > 0 && TryGetNearbyHit(out Collider nearbyHit))
            {
                var rotationToTarget = Quaternion.LookRotation(nearbyHit.transform.position - weaponModel.AttackPoint);
                projectile.transform.rotation = rotationToTarget;
            }
            else
            {
                var rotationToForward = Quaternion.LookRotation((transform.position + transform.forward) - transform.position);
                projectile.transform.rotation = rotationToForward;
            }
        }

        private bool TryGetNearbyHit(out Collider nearbyHit)
        {
            nearbyHit = null;
            float minDistance = float.MaxValue;

            foreach (var hit in hitResults)
            {
                if (hit == null) continue;
                if (GetAngleToHit(hit) > maxAngleRangeAttack) continue;
                var sqrDistance = Vector3.SqrMagnitude(hit.transform.position - transform.position);
                if (sqrDistance < minDistance)
                {
                    minDistance = sqrDistance;
                    nearbyHit = hit;
                }
            }
            return nearbyHit;
        }

        private float GetAngleToHit(Collider hit)
            => Vector3.Angle(transform.forward, hit.transform.position - transform.position);

        private void HitEnemies(Vector3 attackPoint, float attackRadius, float damage)
        {
            int hitCount = Physics.OverlapSphereNonAlloc(attackPoint, attackRadius, hitResults, enemyMask);
            if (hitCount > 0)
            {
                foreach (var hit in hitResults)
                {
                    if (hit == null) continue;
                    if (hit.TryGetComponent(out Health health))
                    {
                        health.ApplyDamage(damage);
                    }
                }
            }
        }


        public void SaveProgress(PersistentProgress progress)
        {
            progress.Weapon = Weapon.Name;
        }

        public void LoadProgress(PersistentProgress progress)
        {
            if (string.IsNullOrEmpty(progress.Weapon))
            {
                EquipWeapon(unarmed);
            }
            else
            {
                EquipWeapon(staticDataService.GetWeapon(progress.Weapon));
            }
        }

    }
}