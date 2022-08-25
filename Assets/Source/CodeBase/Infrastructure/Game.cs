using EpicRPG.Services;
using EpicRPG.Infrastructure.GameStateMachine;

namespace EpicRPG.Infrastructure
{
    public class Game
    {
        public static IInputService InputService;
        public GameStateMachine.GameStateMachine StateMachine { get; private set; }
        public Game()
        {
            StateMachine = new GameStateMachine.GameStateMachine();
            StateMachine.Enter<BootstrapState>();
        }
    }
}