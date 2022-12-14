using SimpleRPG.Characters;
using SimpleRPG.Items;
using SimpleRPG.Services;
using System;
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
            attacker.ShowWeaponModel(true);
            SetHashState(true);
        }



        public override void Exit()
        {
            SetHashState(false);
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
        public void SetHashState(bool value)
               => animator.SetBoolState(attacker.Weapon.AnimationHash, value);
        public void ResetHashFor(WeaponItem oldWeapon)
        {
            if (oldWeapon == null)
                return;
            animator.SetBoolState(oldWeapon.AnimationHash, false);
        }

        private void OnDisable()
        {
            eventReceiver.MeleeHit -= attacker.MeleeAttack;
            eventReceiver.Fired -= attacker.RangeAttack;
        }


    }
}