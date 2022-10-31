using SimpleRPG.Services.GameFactory;
using SimpleRPG.Services.PersistentData;
using UnityEngine;

namespace SimpleRPG.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string ProgressKey = "Progress";
        private readonly IGameFactory gameFactory;
        private readonly IPersistentProgressService progressService;

        public SaveLoadService(IGameFactory gameFactory, IPersistentProgressService progressService)
        {
            this.progressService = progressService;
            this.gameFactory = gameFactory;
        }

        public PersistentProgress Load()
            => PlayerPrefs.GetString(ProgressKey)?.ToDeserialize<PersistentProgress>();
      
        public void Save()
        {
            foreach (var savable in gameFactory.Savables)
                savable.SaveProgress(progressService.Progress);

            PlayerPrefs.SetString(ProgressKey, progressService.Progress.ToSerialize());
        }
    }
}
