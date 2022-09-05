using UnityEngine;

namespace EpicRPG.EntityFSM
{
    public class EntityStateMachine : GameEntity
    {
        [SerializeField] private EntityState[] allStates;
        [SerializeField] private EntityState firstState;
        private EntityState current;

        private void Awake()
        {
            InitStates();
            ChangeState(firstState);
        }
        private void Update()
        {
            if (current == null)
                return;
            current.Run();
            CheckTransitions();
        }


        public void ChangeState(EntityState next)
        {
            current?.Exit();
            current = next;
            current.Enter();
        }
        protected virtual void InitStates()
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
                    ChangeState(transition.NextState);
                    return;
                }
            }
        }

    }
}