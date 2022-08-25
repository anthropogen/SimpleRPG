using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EpicRPG.Infrastructure.GameStateMachine
{
    public class GameStateMachine
    {
        private Dictionary<Type, IGameState> states;
        private IGameState currentState;

        public GameStateMachine()
        {
            states = new Dictionary<Type, IGameState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this)
            };
        }

        public void Enter<TState>() where TState : IGameState
        {
            var state = states[typeof(TState)];
            ChangeCurrentState(state);
        }

        private void ChangeCurrentState(IGameState state)
        {
            currentState?.Exit();
            currentState = state;
            currentState.Enter();
        }
    }
}