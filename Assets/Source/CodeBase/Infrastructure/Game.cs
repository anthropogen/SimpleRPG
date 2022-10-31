using SimpleRPG.Services;
using SimpleRPG.Infrastructure.GameStateMachine;
using SimpleRPG.UI;

namespace SimpleRPG.Infrastructure
{
    public class Game
    {
        public GameStateMachine.GameStateMachine StateMachine { get; private set; }
        private readonly SceneLoader sceneLoader;
        public Game(ICoroutineStarter coroutineStarter, LoadingCurtain curtain)
        {
            sceneLoader = new SceneLoader(coroutineStarter);
            StateMachine = new GameStateMachine.GameStateMachine(sceneLoader, curtain, ServiceLocator.Container);
            StateMachine.Enter<BootstrapState>();
        }
    }
}