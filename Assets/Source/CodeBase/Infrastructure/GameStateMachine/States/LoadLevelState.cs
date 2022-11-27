using SimpleRPG.Characters;
using SimpleRPG.Hero;
using SimpleRPG.Services.GameFactory;
using SimpleRPG.Services.PersistentData;
using SimpleRPG.StaticData;
using SimpleRPG.UI;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SimpleRPG.Infrastructure.GameStateMachine
{
    public class LoadLevelState : IGameEnterParamState<string>
    {
        private readonly GameStateMachine gameStateMachine;
        private readonly SceneLoader sceneLoader;
        private readonly LoadingCurtain curtain;
        private readonly IGameFactory gameFactory;
        private readonly IPersistentProgressService progressService;
        private readonly IStaticDataService staticData;
        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain curtain, IGameFactory gameFactory, IPersistentProgressService progressService, IStaticDataService staticData)
        {
            this.gameStateMachine = gameStateMachine;
            this.sceneLoader = sceneLoader;
            this.curtain = curtain;
            this.gameFactory = gameFactory;
            this.progressService = progressService;
            this.staticData = staticData;
        }

        public void Enter(string sceneName)
        {
            gameFactory.CleanUp();
            gameFactory.WarmUp();
            sceneLoader.Load(sceneName, OnLoaded);
            curtain.Show();
        }

        public void Exit()
        {
            curtain.Hide();
        }

        private async void OnLoaded()
        {
            LevelStaticData levelData = GetLevelData();

            await InitSpawners(levelData);
           InitLevelTransfers(levelData);
            await CreateHero(levelData);
            UpdateProgressReaders();
            gameStateMachine.Enter<GameLoopState>();
        }

        private void InitLevelTransfers(LevelStaticData levelData)
        {
            foreach (var transferData in levelData.LevelTransfers)
            {
                gameFactory.CreateLevelTransfer(transferData.Position, transferData.NextLevel);
            }
        }

        private LevelStaticData GetLevelData()
        {
            var sceneKey = SceneManager.GetActiveScene().name;
            LevelStaticData levelData = staticData.GetLevelDataFor(sceneKey);
            return levelData;
        }

        private async Task InitSpawners(LevelStaticData levelData)
        {
            foreach (var spawnerData in levelData.EnemySpawners)
            {
              await  gameFactory.CreateSpawner(spawnerData.Position, spawnerData.EnemyTypeID, spawnerData.ID);
            }
        }

        private void UpdateProgressReaders()
        {
            foreach (var readers in gameFactory.ProgressReaders)
                readers.LoadProgress(progressService.Progress);
        }

        private async Task CreateHero(LevelStaticData levelData)
        {
            var hero = await gameFactory.CreateHero();
            hero.transform.position = levelData.InitialPlayerPoint;
            await CreateHeroHUD(hero);
            GameObject.FindObjectOfType<FollowingCamera>().SetTarget(hero.transform);
        }

        private async Task CreateHeroHUD(GameObject hero)
        {
            var hud = await gameFactory.CreateHUD();
            hud.GetComponent<CharacterUI>().Construct(hero.GetComponent<PlayerHealth>());
        }
    }
}
