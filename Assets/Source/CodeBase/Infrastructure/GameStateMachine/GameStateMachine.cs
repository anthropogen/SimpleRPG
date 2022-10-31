using SimpleRPG.Services;
using SimpleRPG.Services.GameFactory;
using SimpleRPG.Services.PersistentData;
using SimpleRPG.Services.SaveLoad;
using SimpleRPG.UI;
using System;
using System.Collections.Generic;

namespace SimpleRPG.Infrastructure.GameStateMachine
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IGameExitState> states;
        private readonly SceneLoader sceneLoader;
        private readonly ServiceLocator container;
        private IGameExitState currentState;
        public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain curtain, ServiceLocator container)
        {
            this.sceneLoader = sceneLoader;
            this.container = container;
            states = new Dictionary<Type, IGameExitState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, container),
                [typeof(LoadProgressState)] = new LoadProgressState(this, container.Single<IPersistentProgressService>(), container.Single<ISaveLoadService>()),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, curtain, container.Single<IGameFactory>(), container.Single<IPersistentProgressService>(), container.Single<IStaticDataService>()),
                [typeof(GameLoopState)] = new GameLoopState()
            };
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