﻿using SimpleRPG.Characters;
using SimpleRPG.Dialogue;
using SimpleRPG.Hero;
using SimpleRPG.Services.GameFactory;
using SimpleRPG.Services.PersistentData;
using SimpleRPG.Services.WindowsService;
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
        private readonly IUIFactory uiFactory;
        private readonly IPersistentProgressService progressService;
        private readonly IStaticDataService staticData;
        private readonly IWindowsService windowsService;
        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain curtain, IGameFactory gameFactory, IPersistentProgressService progressService, IStaticDataService staticData, IUIFactory uiFactory, IWindowsService windowsService)
        {
            this.gameStateMachine = gameStateMachine;
            this.sceneLoader = sceneLoader;
            this.curtain = curtain;
            this.gameFactory = gameFactory;
            this.progressService = progressService;
            this.staticData = staticData;
            this.uiFactory = uiFactory;
            this.windowsService = windowsService;
        }

        public void Enter(string sceneName)
        {
            gameFactory.CleanUp();
            gameFactory.WarmUp();
            uiFactory.WarmUp();
            sceneLoader.Load(sceneName, OnLoaded);
            curtain.Show();
        }

        public void Exit()
        {
            GC.Collect();
            curtain.Hide();
        }

        private async void OnLoaded()
        {
            LevelStaticData levelData = GetLevelData();
            windowsService.Clear();
            await InitSpawners(levelData);
            InitLevelTransfers(levelData);
            uiFactory.CreateUIRoot();
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
            GameObject enemyParent = CreateSpawnerParent("EnemyRoot");
            foreach (var spawnerData in levelData.EnemySpawners)
            {
                var spawner = await gameFactory.CreateEnemySpawner(spawnerData.Position, spawnerData.EnemyTypeID, spawnerData.ID);
                spawner.transform.SetParent(enemyParent.transform);
            }

            GameObject npcParent = CreateSpawnerParent("NPCRoot");
            foreach (var spawnerData in levelData.NPCSpawners)
            {
                var spawner = await gameFactory.CreateNPCSpawner(spawnerData.Position, spawnerData.Rotation, spawnerData.ID, spawnerData.SaveID);
                spawner.transform.SetParent(npcParent.transform);
            }
        }

        private static GameObject CreateSpawnerParent(string name)
        {
            GameObject spawnParent = new GameObject();
            spawnParent.name = name;
            spawnParent.transform.position = Vector3.zero;
            return spawnParent;
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
            var inventoryWindow = await windowsService.OpenWindow(WindowsID.Inventory);
            var dialogueWindow = await windowsService.OpenWindow(WindowsID.Dialogue);
            dialogueWindow.Close();
            hero.GetComponent<PlayerConversant>().Construct(dialogueWindow as DialogueWindow);
            inventoryWindow.Close();
        }
    }
}
