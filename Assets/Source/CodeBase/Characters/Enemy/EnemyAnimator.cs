using UnityEngine;

namespace EpicRPG.Characters.Enemies
{
    public class EnemyAnimator : GameEntity
    {
        [SerializeField] private Animator animator;
        [SerializeField] private Health health;
        private readonly int DieHash = Animator.StringToHash("Die");
        private readonly int VictoryHash = Animator.StringToHash("Victory");
        private readonly int HitHash = Animator.StringToHash("Hit");
        private readonly int MoveHash = Animator.StringToHash("IsMove");
        private readonly int SpeedHash = Animator.StringToHash("Speed");
        private readonly int Attack01Hash = Animator.StringToHash("Attack01");
        private readonly int Attack02Hash = Animator.StringToHash("Attack02");

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
            animator.SetBool(MoveHash, true);
            animator.SetFloat(SpeedHash, speed);
        }

        public void StopMove()
        {
            animator.SetBool(MoveHash, false);
            animator.SetFloat(SpeedHash, 0);
        }

        public void Attack()
            => animator.SetTrigger(Attack01Hash);

        public void ForceAttack()
            => animator.SetTrigger(Attack02Hash);

        public void Victory()
            => animator.SetTrigger(VictoryHash);

        public void Hit()
         => animator.SetTrigger(HitHash);

        public void Die()
            => animator.SetTrigger(DieHash);

        public void ResetAttack()
             => animator.ResetTrigger(Attack01Hash);
        public void ResetForceAttack()
            => animator.ResetTrigger(Attack02Hash);
    }
}