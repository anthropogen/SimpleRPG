namespace EpicRPG.EntityFSM
{
    public interface IEntityState
    {
        void Enter();
        void Run();
        void Exit();
    }
}