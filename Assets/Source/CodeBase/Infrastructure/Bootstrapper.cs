using UnityEngine;

namespace EpicRPG.Infrastructure
{
    public class Bootstrapper : MonoBehaviour
    {
        private Game game;

        private void Awake()
        {
            game = new Game();
            DontDestroyOnLoad(this);
        }
    }
}