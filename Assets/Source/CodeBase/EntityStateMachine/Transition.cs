using UnityEngine;

namespace EpicRPG.EntityFSM
{
    public abstract class Transition<TState> : MonoBehaviour, ITransition<TState> where TState : IEntityState
    {
        [field: SerializeField] public TState NextState { get; private set; }
        public abstract void Construct<TArgument>(TArgument transitionArguments);
        public abstract bool NeedTransit();
    }

}