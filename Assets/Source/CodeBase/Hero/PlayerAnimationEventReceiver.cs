using System;

namespace EpicRPG.Hero
{
    public class PlayerAnimationEventReceiver : GameEntity
    {
        public event Action MeleeHit;
        public event Action MeleeForceHit;


        private void Melee()
            => MeleeHit?.Invoke();

        private void MeleeForce()
            => MeleeForceHit?.Invoke();
    }
}