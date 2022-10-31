using SimpleRPG.Characters;
using SimpleRPG.EntityFSM;
using UnityEngine;

namespace SimpleRPG.Hero
{
    public class PlayerStateMachine : EntityStateMachine<PlayerState>
    {
        [SerializeField] private PlayerMover mover;
        [SerializeField] private PlayerAnimator animator;
        [SerializeField] private PlayerHealth health;
        [SerializeField] private PlayerDeathState deathState;

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
    }
}