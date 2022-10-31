using SimpleRPG.Characters;
using SimpleRPG.Services;
using UnityEngine;

namespace SimpleRPG.Hero
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
            eventReceiver.Fired += attacker.RangeAttack;
            animator.SetBoolState(attacker.Weapon.AnimationHash, true);
            attacker.ShowWeaponModel(true);
        }


        public override void Exit()
        {
            animator.SetBoolState(attacker.Weapon.AnimationHash, false);
            eventReceiver.MeleeHit -= attacker.MeleeAttack;
            eventReceiver.Fired -= attacker.RangeAttack;
            attacker.ShowWeaponModel(false);
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
            eventReceiver.Fired -= attacker.RangeAttack;
        }
    }
}