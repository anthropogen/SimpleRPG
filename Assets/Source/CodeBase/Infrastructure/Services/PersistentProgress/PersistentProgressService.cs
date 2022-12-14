
using UnityEngine.SceneManagement;

namespace SimpleRPG.Services.PersistentData
{
    public class PersistentProgressService : IPersistentProgressService
    {
        public PersistentProgress Progress { get; set; }

        public static string GetActiveSceneName()
            => SceneManager.GetActiveScene().name;
    }
}

