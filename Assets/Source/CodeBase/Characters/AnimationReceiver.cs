using SimpleRPG.Infrastructure;
using System;

namespace SimpleRPG.Characters
{
    public class AnimationReceiver : GameEntity
    {
        public event Action MeleeAttack;

        public void Melee()
            => MeleeAttack?.Invoke();
    }
}