using EpicRPG.Services;
using EpicRPG.Services.AssetManagement;
using EpicRPG.Services.GameFactory;
using UnityEngine;

namespace EpicRPG.Infrastructure.GameStateMachine
{
    public class BootstrapState : IGameEnterState
    {
        private const string InitialScene = "Initial";
        private readonly GameStateMachine gameStateMachine;
        private readonly SceneLoader sceneLoader;
        private readonly ServiceLocator services;
        public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, ServiceLocator services)
        {
            this.gameStateMachine = gameStateMachine;
            this.sceneLoader = sceneLoader;
            this.services = services;
            RegisterServices();
        }

        public void Enter()
        {
            sceneLoader.Load(InitialScene, LoadLevel);
        }

        public void Exit()
        {
        }

        private void RegisterServices()
        {
            services.RegisterSingle<IInputService>(CreateInputService());
            services.RegisterSingle<IAssetProvider>(new AssetProvider());
            services.RegisterSingle<IGameFactory>(new GameFactory(
            services.Single<IAssetProvider>()));
        }

        private void LoadLevel()
        {
            gameStateMachine.Enter<LoadLevelState, string>("Main");
        }

        private IInputService CreateInputService()
        {
            if (Application.isEditor)
                return new StandaloneInputService();

            return new MobileInputService();
        }
    }
}
