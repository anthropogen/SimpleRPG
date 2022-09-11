using UnityEngine;

namespace EpicRPG.EntityFSM
{
    public abstract class EntityStateMachine<TState> : GameEntity where TState : EntityState
    {
        [SerializeField] protected TState[] allStates;
        [SerializeField] protected TState firstState;
        protected TState current;

        public virtual void Construct(Transform transform)
        {
            InitStates(transform);
            ChangeState(firstState);
        }

        private void Update()
        {
            if (current == null)
                return;
            current.Run();
            CheckTransitions();
        }

        public void ChangeState(TState next)
        {
            current?.Exit();
            current = next;
            current.Enter();
        }

        protected virtual void InitStates(Transform transform)
        {
            foreach (var state in allStates)
                state.Init(transform);
        }

        private void CheckTransitions()
        {
            foreach (var transition in current.Transitions)
            {
                if (transition.NeedTransit())
                {
                    ChangeState(transition.NextState as TState);
                    return;
                }
            }
        }
    }
}