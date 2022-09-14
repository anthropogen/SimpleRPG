using EpicRPG.Hero;
using EpicRPG.Services.AssetManagement;
using EpicRPG.Services.PersistentData;
using System.Collections.Generic;
using UnityEngine;

namespace EpicRPG.Services.GameFactory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider assets;
        public List<ISavable> Savables { get; } = new List<ISavable>();
        public List<IProgressReader> ProgressReaders { get; } = new List<IProgressReader>();

        public GameObject HeroGameObject { get; private set; }

        public GameFactory(IAssetProvider assets)
        {
            this.assets = assets;
        }

        public GameObject CreateHero()
            => HeroGameObject = InstantiateRegisteredObject(AssetsPath.Player, GameObject.FindObjectOfType<PlayerInitPoint>().Point);


        public GameObject CreateHUD()
            => InstantiateRegisteredObject(AssetsPath.HUD);

        public void CleanUp()
        {
            ProgressReaders.Clear();
            Savables.Clear();
        }

        private GameObject InstantiateRegisteredObject(string path)
        {
            var regObject = assets.Instantiate(path);
            RegisterProgressWatchers(regObject);
            return regObject;
        }

        private GameObject InstantiateRegisteredObject(string path, Vector3 position)
        {
            var regObject = assets.InstantiateAt(path, position);
            RegisterProgressWatchers(regObject);
            return regObject;
        }

        private void RegisterProgressWatchers(GameObject gObject)
        {
            foreach (var reader in gObject.GetComponentsInChildren<IProgressReader>())
                Register(reader);
        }

        private void Register(IProgressReader reader)
        {
            if (reader is ISavable)
                Savables.Add(reader as ISavable);
            ProgressReaders.Add(reader);
        }
    }
}
