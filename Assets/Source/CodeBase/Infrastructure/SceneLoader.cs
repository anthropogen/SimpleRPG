using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace EpicRPG.Infrastructure
{
    public class SceneLoader
    {
        private readonly ICoroutineStarter coroutineStarter;

        public SceneLoader(ICoroutineStarter coroutineStarter)
            => this.coroutineStarter = coroutineStarter;

        public void Load(string name, Action onLoaded = null)
            => coroutineStarter.StartCoroutine(LoadScene(name, onLoaded));

        private IEnumerator LoadScene(string name, Action onLoaded = null)
        {
            if (SceneManager.GetActiveScene().name == name)
            {
                onLoaded?.Invoke();
                yield break;
            }

            var waitOperation = SceneManager.LoadSceneAsync(name);
            while (!waitOperation.isDone)
                yield return null;

            onLoaded?.Invoke();
        }
    }
}