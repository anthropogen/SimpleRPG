using SimpleRPG.Infrastructure;
using SimpleRPG.StaticData;
using UnityEngine;

namespace SimpleRPG.Characters
{
    public class Attacker : GameEntity
    {
        [SerializeField] private AnimationReceiver animationReceiver;
        [SerializeField] private Transform meleeAttackPoint;
        [SerializeField, Range(0, 5)] private float meleeRadius = 0.7f;
        [SerializeField] private LayerMask enemyMask;
        private readonly Collider[] hitResults = new Collider[5];
        private EnemyStaticData staticData;

        public void Construct(EnemyStaticData staticData)
        {
            this.staticData = staticData;
        }

        private void OnEnable()
        {
            animationReceiver.MeleeAttack += MeleeAttack;
        }

        private void OnDisable()
        {
            animationReceiver.MeleeAttack -= MeleeAttack;
        }

        public void MeleeAttack()
        {
            var hitsCount = Physics.OverlapSphereNonAlloc(meleeAttackPoint.position, meleeRadius, hitResults, enemyMask);
            if (hitsCount == 0) return;
            foreach (var hit in hitResults)
            {
                if (hit == null) continue;

                if (hit.gameObject.TryGetComponent(out Health health))
                    health.ApplyDamage(staticData.Damage);
            }

        }
        private void OnDrawGizmos()
        {
            if (meleeAttackPoint == null) return;
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(meleeAttackPoint.position, meleeRadius);
        }
    }
}