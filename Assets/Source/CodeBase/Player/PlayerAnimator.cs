using UnityEngine;

namespace EpicRPG.Hero
{
    public class PlayerAnimator : GameEntity
    {
        [SerializeField] private Animator animator;
        [SerializeField] private CharacterController controller;
        private readonly int moveHash = Animator.StringToHash("IsMove");
        private readonly int speedHash = Animator.StringToHash("Speed");

        public void Move(float speed)
        {
            animator.SetBool(moveHash, true);
            animator.SetFloat(speedHash, speed);
        }

        public void StopMove()
            => animator.SetBool(moveHash, false);
    }
}