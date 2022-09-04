using EpicRPG.Player;
using EpicRPG.Services.GameFactory;
using EpicRPG.Services.PersistentData;
using UnityEngine;

namespace EpicRPG.Infrastructure.GameStateMachine
{
    public class LoadLevelState : IGameEnterParamState<string>
    {
        private readonly GameStateMachine gameStateMachine;
        private readonly SceneLoader sceneLoader;
        private readonly LoadingCurtain curtain;
        private readonly IGameFactory gameFactory;
        private readonly IPersistentProgressService progressService;
        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain curtain, IGameFactory gameFactory, IPersistentProgressService progressService)
        {
            this.gameStateMachine = gameStateMachine;
            this.sceneLoader = sceneLoader;
            this.curtain = curtain;
            this.gameFactory = gameFactory;
            this.progressService = progressService;
        }

        public void Enter(string sceneName)
        {
            gameFactory.CleanUp();
            sceneLoader.Load(sceneName, OnLoaded);
            curtain.Show();
        }

        public void Exit()
        {
            curtain.Hide();
        }
        private void OnLoaded()
        {
            CreateGameEntities();
            UpdateProgressReaders();
            gameStateMachine.Enter<GameLoopState>();
        }

        private void UpdateProgressReaders()
        {
            foreach (var readers in gameFactory.ProgressReaders)
                readers.LoadProgress(progressService.Progress);
        }

        private void CreateGameEntities()
        {
            var hero = gameFactory.CreateHero();
            gameFactory.CreateHUD();
            GameObject.FindObjectOfType<FollowingCamera>().SetTarget(hero.transform);
        }
    }
}
