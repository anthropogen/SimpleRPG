using EpicRPG.Services;
using EpicRPG.Infrastructure.GameStateMachine;

namespace EpicRPG.Infrastructure
{
    public class Game
    {
        public static IInputService InputService;
        public GameStateMachine.GameStateMachine StateMachine { get; private set; }
        private readonly SceneLoader sceneLoader;
        public Game(ICoroutineStarter coroutineStarter)
        {
            sceneLoader = new SceneLoader(coroutineStarter);
            StateMachine = new GameStateMachine.GameStateMachine(sceneLoader);
            StateMachine.Enter<BootstrapState>();
        }
    }
}