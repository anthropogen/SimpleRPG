using EpicRPG.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace EpicRPG.Infrastructure.GameStateMachine
{
    public class BootstrapState : IGameEnterState
    {
        private const string InitialScene = "Initial";
        private readonly GameStateMachine gameStateMachine;
        private readonly SceneLoader sceneLoader;

        public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader)
        {
            this.gameStateMachine = gameStateMachine;
            this.sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            RegisterServices();
            sceneLoader.Load(InitialScene, LoadLevel);
        }


        public void Exit()
        {
        }

        private void RegisterServices()
        {
            Game.InputService = CreateInputService();
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
