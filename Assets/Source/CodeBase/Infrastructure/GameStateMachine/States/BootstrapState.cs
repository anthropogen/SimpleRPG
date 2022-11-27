﻿using SimpleRPG.Services;
using SimpleRPG.Services.AssetManagement;
using SimpleRPG.Services.GameFactory;
using SimpleRPG.Services.PersistentData;
using SimpleRPG.Services.SaveLoad;
using UnityEngine;

namespace SimpleRPG.Infrastructure.GameStateMachine
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
            services.Single<IAssetProvider>().Initialize();
        }

        public void Enter()
        {
            sceneLoader.Load(InitialScene, LoadProgress);
        }

        public void Exit()
        {
        }

        private void RegisterServices()
        {
            RegisterStaticData();
            services.RegisterSingle<IGameStateMachine>(gameStateMachine);
            services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
            services.RegisterSingle<IInputService>(CreateInputService());
            services.RegisterSingle<IAssetProvider>(new AssetProvider());
            services.RegisterSingle<IGameFactory>(new GameFactory(services.Single<IAssetProvider>(), services.Single<IStaticDataService>(), services.Single<IPersistentProgressService>(), gameStateMachine));
            services.RegisterSingle<ISaveLoadService>(new SaveLoadService(services.Single<IGameFactory>(), services.Single<IPersistentProgressService>()));
        }

        private void RegisterStaticData()
        {
            services.RegisterSingle<IStaticDataService>(new StaticDataService());
            services.Single<IStaticDataService>().Load();
        }

        private void LoadProgress()
            => gameStateMachine.Enter<LoadProgressState>();

        private IInputService CreateInputService()
        {
            if (Application.isEditor)
                return new StandaloneInputService();

            return new MobileInputService();
        }
    }
}
