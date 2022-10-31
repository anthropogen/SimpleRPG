using SimpleRPG.Services;

namespace SimpleRPG.Infrastructure.GameStateMachine
{
    public interface IGameStateMachine : IService
    {
        void Enter<TState, TParam>(TParam param) where TState : class, IGameEnterParamState<TParam>;
        void Enter<TState>() where TState : class, IGameEnterState;
    }
}