namespace SimpleRPG.Characters.Enemies
{
    public class ChaseEnemyState : EnemyState
    {
        public override void Enter()
        {

        }

        public override void Exit()
        {
            animator.StopMove();
            agent.ResetPath();
        }

        public override void Run()
        {
            animator.Move(agent.velocity.magnitude / agent.speed);
            agent.SetDestination(player.transform.position);
        }
    }
}