using UnityEngine;
namespace EpicRPG.Characters.Enemies
{
    public class EnemyAttackState : EnemyState
    {
        [SerializeField, Range(0, 20)] private float delay = 2f;
        private float cooldown;
        public override void Enter()
        {

        }

        public override void Exit()
        {
            animator.ResetAttack();
        }


        public override void Run()
        {
            RotateToPlayer();
            if (CanAttack())
            {
                Attack();
            }
        }

        private void RotateToPlayer()
            => parent.forward = player.transform.position - transform.position;

        private void Attack()
        {
            animator.Attack();
            SetCooldown();
        }

        private void SetCooldown()
            => cooldown = Time.time + delay;

        private bool CanAttack()
            => cooldown <= Time.time;
    }
}