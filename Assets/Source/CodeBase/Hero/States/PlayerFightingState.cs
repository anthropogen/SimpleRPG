using EpicRPG.Characters;
using EpicRPG.Services;
using UnityEngine;

namespace EpicRPG.Hero
{
    public class PlayerFightingState : PlayerState
    {
        [SerializeField] private PlayerAnimationEventReceiver eventReceiver;
        [SerializeField] private CharacterAttacker attacker;
        private IInputService inputService;

        public override void Enter()
        {
            inputService = ServiceLocator.Container.Single<IInputService>();
            eventReceiver.MeleeHit += attacker.MeleeAttack;
            animator.SetBoolState(attacker.Weapon.AnimationHash, true);
        }


        public override void Exit()
        {
            animator.SetBoolState(attacker.Weapon.AnimationHash, false);
            eventReceiver.MeleeHit -= attacker.MeleeAttack;
        }

        public override void Run()
        {
            if (inputService.IsAttackButtonUp() && !animator.IsAttacking)
            {
                animator.Attack();
            }
            mover.Run();
        }


        private void OnDisable()
        {
            eventReceiver.MeleeHit -= attacker.MeleeAttack;
        }
    }
}