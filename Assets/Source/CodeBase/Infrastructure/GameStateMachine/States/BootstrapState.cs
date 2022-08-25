using EpicRPG.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace EpicRPG.Infrastructure.GameStateMachine
{
    public class BootstrapState : IGameState
    {
        private GameStateMachine gameStateMachine;

        public BootstrapState(GameStateMachine gameStateMachine)
        {
            this.gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            RegisterServices();
        }
        public void Exit()
        {
        }

        private void RegisterServices()
        {
            Game.InputService = CreateInputService();
        }
        private IInputService CreateInputService()
        {
            if (Application.isEditor)
                return new StandaloneInputService();

            return new MobileInputService();
        }
    }
}
