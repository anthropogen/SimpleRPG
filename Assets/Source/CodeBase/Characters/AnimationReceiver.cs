using System;

namespace EpicRPG.Characters
{
    public class AnimationReceiver : GameEntity
    {
        public event Action MeleeAttack;

        public void Melee()
            => MeleeAttack?.Invoke();
    }
}