using EpicRPG.Characters;
using EpicRPG.Services;
using UnityEngine;

namespace EpicRPG.Hero
{
    public class PlayerMeleeState : PlayerState
    {
        [SerializeField] private PlayerAnimationEventReceiver eventReceiver;
        [SerializeField] private Transform attackPoint;
        [SerializeField, Range(0, 5)] private float attackRadius;
        [SerializeField, Range(0, 50)] private float damage;
        [SerializeField] private LayerMask enemyMask;
        private readonly Collider[] hitResults = new Collider[5];
        private IInputService inputService;

        public override void Enter()
        {
            inputService = ServiceLocator.Container.Single<IInputService>();
            eventReceiver.MeleeHit += OnMeleeHit;
            eventReceiver.MeleeForceHit += OnMeleeForceHit;
            animator.Unarmed(true);
        }


        public override void Exit()
        {
            animator.Unarmed(false);
            eventReceiver.MeleeHit -= OnMeleeHit;
            eventReceiver.MeleeForceHit -= OnMeleeForceHit;
        }

        public override void Run()
        {
            if (inputService.IsAttackButtonUp() && !animator.IsAttacking)
            {
                animator.Attack();
            }
            else if (inputService.IsForceAttackButtonUp() && !animator.IsAttacking)
            {
                animator.ForceAttack();
            }
            mover.Run();
        }

        private void OnMeleeForceHit()
        {
            HitEnemies();
        }

        private void OnMeleeHit()
        {
            HitEnemies();
        }

        private void HitEnemies()
        {
            var hitCount = Physics.OverlapSphereNonAlloc(attackPoint.position, attackRadius, hitResults, enemyMask);
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

        private void OnDisable()
        {
            eventReceiver.MeleeHit -= OnMeleeHit;
            eventReceiver.MeleeForceHit -= OnMeleeForceHit;
        }
    }
}