namespace EpicRPG.Hero
{
    public class TraversalState : PlayerState
    {

        public override void Enter()
        {
            animator.Traversal(true);
        }

        public override void Exit()
        {
            animator.Traversal(false);
        }

        public override void Run()
        {
            mover.Run();
        }
    }
}