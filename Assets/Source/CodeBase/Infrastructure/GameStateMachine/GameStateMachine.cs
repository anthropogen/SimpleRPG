using EpicRPG.Services;
using EpicRPG.Services.GameFactory;
using System;
using System.Collections.Generic;

namespace EpicRPG.Infrastructure.GameStateMachine
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IGameExitState> states;
        private readonly SceneLoader sceneLoader;
        private IGameExitState currentState;
        private ServiceLocator container;
        public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain curtain, ServiceLocator container)
        {
            states = new Dictionary<Type, IGameExitState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, container),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, curtain, container.Single<IGameFactory>()),
                [typeof(GameLoopState)] = new GameLoopState()
            };
            this.sceneLoader = sceneLoader;
            this.container = container;
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