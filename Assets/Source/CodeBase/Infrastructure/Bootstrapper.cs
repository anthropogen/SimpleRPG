using System.Collections;
using UnityEngine;

namespace EpicRPG.Infrastructure
{
    public class Bootstrapper : MonoBehaviour, ICoroutineStarter
    {
        [SerializeField] private LoadingCurtain curtain;
        private Game game;
        private void Awake()
        {
            game = new Game(this,curtain);
            DontDestroyOnLoad(this);
        }
    }
}