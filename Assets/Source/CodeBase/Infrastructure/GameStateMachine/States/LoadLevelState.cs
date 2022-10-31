using SimpleRPG.Characters;
using SimpleRPG.Hero;
using SimpleRPG.Services.GameFactory;
using SimpleRPG.Services.PersistentData;
using SimpleRPG.StaticData;
using SimpleRPG.UI;
using System;
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
            sceneLoader.Load(sceneName, OnLoaded);
            curtain.Show();
        }

        public void Exit()
        {
            curtain.Hide();
        }

        private void OnLoaded()
        {
            LevelStaticData levelData = GetLevelData();

            InitSpawners(levelData);
            InitLevelTransfers(levelData);
            CreateHero(levelData);
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

        private void InitSpawners(LevelStaticData levelData)
        {
            foreach (var spawnerData in levelData.EnemySpawners)
            {
                gameFactory.CreateSpawner(spawnerData.Position, spawnerData.EnemyTypeID, spawnerData.ID);
            }
        }

        private void UpdateProgressReaders()
        {
            foreach (var readers in gameFactory.ProgressReaders)
                readers.LoadProgress(progressService.Progress);
        }

        private void CreateHero(LevelStaticData levelData)
        {
            var hero = gameFactory.CreateHero();
            hero.transform.position = levelData.InitialPlayerPoint;
            CreateHeroHUD(hero);
            GameObject.FindObjectOfType<FollowingCamera>().SetTarget(hero.transform);
        }

        private void CreateHeroHUD(GameObject hero)
        {
            var hud = gameFactory.CreateHUD();
            hud.GetComponent<CharacterUI>().Construct(hero.GetComponent<PlayerHealth>());
        }
    }
}
