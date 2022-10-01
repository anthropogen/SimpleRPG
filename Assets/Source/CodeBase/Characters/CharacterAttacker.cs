using EpicRPG.Hero;
using EpicRPG.Items;
using EpicRPG.Services;
using EpicRPG.Services.PersistentData;
using UnityEngine;

namespace EpicRPG.Characters
{
    public class CharacterAttacker : GameEntity, ISavable
    {
        [SerializeField] private LayerMask enemyMask;
        [SerializeField] private Unarmed unarmed;
        [SerializeField] private WeaponPositioner weaponPositioner;
        private WeaponModel weaponModel;
        private IStaticDataService staticDataService;
        private readonly Collider[] hitResults = new Collider[5];
        public WeaponItem Weapon { get; private set; }

        private void Awake()
        {
            staticDataService = ServiceLocator.Container.Single<IStaticDataService>();
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

        private void HitEnemies(Vector3 attackPoint, float attackRadius, float damage)
        {
            var hitCount = Physics.OverlapSphereNonAlloc(attackPoint, attackRadius, hitResults, enemyMask);
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