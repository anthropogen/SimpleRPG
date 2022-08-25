namespace EpicRPG.Infrastructure.GameStateMachine
{
    public interface IGameState
    {
        void Enter();
        void Exit();
    }
}