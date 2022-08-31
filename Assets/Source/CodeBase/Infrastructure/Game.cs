using EpicRPG.Services;
using EpicRPG.Infrastructure.GameStateMachine;

namespace EpicRPG.Infrastructure
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