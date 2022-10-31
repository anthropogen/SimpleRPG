namespace SimpleRPG.Infrastructure.GameStateMachine
{
    public interface IGameEnterState : IGameExitState
    {
        void Enter();
    }
}