using System.Collections;
using UnityEngine;

namespace EpicRPG.Infrastructure
{
    public class Bootstrapper : MonoBehaviour, ICoroutineStarter
    {
        private Game game;


        private void Awake()
        {
            game = new Game(this);
            DontDestroyOnLoad(this);
        }
    }
}