using System;
using System.Collections.Generic;

namespace EpicRPG.Infrastructure.GameStateMachine
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IGameExitState> states;
        private readonly SceneLoader sceneLoader;
        private IGameExitState currentState;

        public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain curtain)
        {
            states = new Dictionary<Type, IGameExitState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, curtain),
                [typeof(GameLoopState)] = new GameLoopState()
            };
            this.sceneLoader = sceneLoader;
        }

        public void Enter<TState>() where TState : class, IGameEnterState
        {
            var state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TParam>(TParam param) where TState : class, IGameEnterParamState<TParam>
        {
            var state = ChangeState<TState>();
            state.Enter(param);
        }

        private TState ChangeState<TState>() where TState : class, IGameExitState
        {
            currentState?.Exit();
            var state = GetState<TState>();
            currentState = state;
            return state;
        }

        private TState GetState<TState>() where TState : class, IGameExitState
            => states[typeof(TState)] as TState;
    }
}