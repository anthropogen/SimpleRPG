namespace EpicRPG.Infrastructure.GameStateMachine
{
    public interface IGameEnterParamState<TParam> : IGameExitState
    {
        void Enter(TParam param);
    }
}