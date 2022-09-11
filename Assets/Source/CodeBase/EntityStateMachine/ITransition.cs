namespace EpicRPG.EntityFSM
{
    public interface ITransition<TState> where TState : IEntityState
    {
        TState NextState { get; }

        bool NeedTransit();
    }
}