namespace SimpleRPG.EntityFSM
{
    public interface IEntityState
    {
        void Enter();
        void Run();
        void Exit();
    }
}