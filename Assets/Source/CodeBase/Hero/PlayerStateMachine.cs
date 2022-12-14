using SimpleRPG.Characters;
using SimpleRPG.EntityFSM;
using SimpleRPG.Items;
using System;
using System.Linq;
using UnityEngine;

namespace SimpleRPG.Hero
{
    public class PlayerStateMachine : EntityStateMachine<PlayerState>
    {
        [SerializeField] private PlayerMover mover;
        [SerializeField] private PlayerAnimator animator;
        [SerializeField] private PlayerHealth health;
        [SerializeField] private PlayerDeathState deathState;
        [SerializeField] private CharacterAttacker attacker;
        private void Start()
        {
            attacker.WeaponChanged += OnWeaponChanged;
        }

        protected override void Disable()
        {
            health.Death -= OnDeath;
        }

        protected override void InitStates(Transform transform)
        {
            foreach (var state in allStates)
                state.Init(transform, mover, animator);
            health.Death += OnDeath;
        }
        private void OnDeath()
            => ChangeState(deathState);

        private void OnWeaponChanged(WeaponItem oldWeapon, WeaponItem newWeapon)
        {
            if (current is PlayerFightingState)
            {
                var state = allStates.FirstOrDefault(s => s is PlayerFightingState) as PlayerFightingState;
                state.ResetHashFor(oldWeapon);
                state.SetHashState(true);
            }
        }
    }
}