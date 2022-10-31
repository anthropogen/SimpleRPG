using SimpleRPG.UI;
using UnityEngine;

namespace SimpleRPG.Infrastructure
{
    public class Bootstrapper : MonoBehaviour, ICoroutineStarter
    {
        [SerializeField] private LoadingCurtain curtainTemplate;
        private Game game;
        private void Awake()
        {
            game = new Game(this, Instantiate(curtainTemplate));
            DontDestroyOnLoad(this);
        }
    } 
}