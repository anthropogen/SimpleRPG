using EpicRPG.EntityFSM;
using UnityEngine;

namespace EpicRPG.Hero
{
    public abstract class PlayerState : EntityState
    {
        protected PlayerMover mover;
        protected Transform parent;
        protected PlayerAnimator animator;
        public void Init(Transform transform, PlayerMover mover,PlayerAnimator animator)
        {
            this.animator = animator;
            this.mover = mover;
            Init(transform);
        }
        public override void Init(Transform transform)
            => parent = transform;
    }
}