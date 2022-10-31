using UnityEngine;
namespace SimpleRPG.Characters.Enemies
{
    public class EnemyAttackState : EnemyState
    {
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
            => cooldown = Time.time + staticData.Delay;

        private bool CanAttack()
            => cooldown <= Time.time;
    }
}