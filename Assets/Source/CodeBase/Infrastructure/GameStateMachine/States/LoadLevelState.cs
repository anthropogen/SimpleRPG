using EpicRPG.Player;
using System;
using UnityEngine;

namespace EpicRPG.Infrastructure.GameStateMachine
{
    public class LoadLevelState : IGameEnterParamState<string>
    {
        private readonly GameStateMachine gameStateMachine;
        private readonly SceneLoader sceneLoader;
        private readonly LoadingCurtain curtain;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain curtain)
        {
            this.gameStateMachine = gameStateMachine;
            this.sceneLoader = sceneLoader;
            this.curtain = curtain;
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
            var playerPos = GameObject.FindObjectOfType<PlayerInitPoint>().Point;
            var hero = InstantiateAt("Player", playerPos);
            Instantiate("HUD");
            GameObject.FindObjectOfType<FollowingCamera>().SetTarget(hero.transform);
            gameStateMachine.Enter<GameLoopState>();
        }

        private static GameObject Instantiate(string path)
        {
            var prefab = Resources.Load<GameObject>(path);
            return GameObject.Instantiate<GameObject>(prefab);
        }
        private static GameObject InstantiateAt(string path, Vector3 position)
        {
            var prefab = Resources.Load<GameObject>(path);
            return GameObject.Instantiate<GameObject>(prefab, position, Quaternion.identity);
        }
    }
}
