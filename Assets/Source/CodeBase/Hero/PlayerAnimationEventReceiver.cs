using SimpleRPG.Infrastructure;
using System;

namespace SimpleRPG.Hero
{
    public class PlayerAnimationEventReceiver : GameEntity
    {
        public event Action MeleeHit;
        public event Action MeleeForceHit;
        public event Action Fired;

        private void Melee()
            => MeleeHit?.Invoke();

        private void MeleeForce()
            => MeleeForceHit?.Invoke();

        private void Shot()
            => Fired?.Invoke();
    }
}