using SimpleRPG.Characters;
using SimpleRPG.Infrastructure;
using UnityEngine;

namespace SimpleRPG.Hero
{
    public class PlayerAnimator : GameEntity
    {
        [SerializeField] private Animator animator;
        [SerializeField] private Health health;
        private readonly int moveHash = Animator.StringToHash("IsMove");
        private readonly int speedHash = Animator.StringToHash("Speed");
        private readonly int deathHash = Animator.StringToHash("Death");
        private readonly int hitHash = Animator.StringToHash("Hit");
        private readonly int attackHash = Animator.StringToHash("Attack00");
        private readonly int forceAttackHash = Animator.StringToHash("Attack01");
        private readonly int traversalHash = Animator.StringToHash("IsTraversal");
        public bool IsAttacking { get; set; }

        protected override void Enable()
        {
            health.AppliedDamage += Hit;
        }

        protected override void Disable()
        {
            health.AppliedDamage -= Hit;
        }

        public void Move(float speed)
        {
            animator.SetBool(moveHash, true);
            animator.SetFloat(speedHash, speed);
        }

        public void StopMove()
            => animator.SetBool(moveHash, false);

        public void PlayDeath()
            => animator.Play(deathHash);

        public void Hit()
            => animator.SetTrigger(hitHash);

        public void Attack()
            => animator.SetTrigger(attackHash);

        public void ForceAttack()
            => animator.SetTrigger(forceAttackHash);

        public void Traversal(bool isTraversal)
            => animator.SetBool(traversalHash, isTraversal);

        public void SetBoolState(int hash, bool state)
            => animator.SetBool(hash, state);
    }

}