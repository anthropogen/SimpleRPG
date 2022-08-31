using EpicRPG.Player;
using EpicRPG.Services.GameFactory;
using UnityEngine;

namespace EpicRPG.Infrastructure.GameStateMachine
{
    public class LoadLevelState : IGameEnterParamState<string>
    {
        private readonly GameStateMachine gameStateMachine;
        private readonly SceneLoader sceneLoader;
        private readonly LoadingCurtain curtain;
        private readonly IGameFactory gameFactory;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain curtain, IGameFactory gameFactory)
        {
            this.gameStateMachine = gameStateMachine;
            this.sceneLoader = sceneLoader;
            this.curtain = curtain;
            this.gameFactory = gameFactory;
        }

        public void Enter(string sceneName)
        {
            sceneLoader.Load(sceneName, OnLoaded);
            curtain.Show();
        }

        public void Exit()
        {
            curtain.Hide();
        }
        private void OnLoaded()
        {
            var hero = gameFactory.CreateHero();
            gameFactory.CreateHUD();
            GameObject.FindObjectOfType<FollowingCamera>().SetTarget(hero.transform);
            gameStateMachine.Enter<GameLoopState>();
        }
    }
}
