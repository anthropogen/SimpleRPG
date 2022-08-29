namespace EpicRPG.Infrastructure.GameStateMachine
{
    public interface IGameEnterState : IGameExitState
    {
        void Enter();
    }
}