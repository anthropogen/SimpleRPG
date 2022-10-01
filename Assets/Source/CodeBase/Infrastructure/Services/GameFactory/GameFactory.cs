using EpicRPG.Characters.Enemies;
using EpicRPG.Characters.Enemy;
using EpicRPG.Hero;
using EpicRPG.Items;
using EpicRPG.Services.AssetManagement;
using EpicRPG.Services.PersistentData;
using System.Collections.Generic;
using UnityEngine;

namespace EpicRPG.Services.GameFactory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider assets;
        private readonly IStaticDataService staticData;
        public LazyInitializy<Player> LazyPlayer { get; private set; } = new LazyInitializy<Player>();
        public List<ISavable> Savables { get; } = new List<ISavable>();
        public List<IProgressReader> ProgressReaders { get; } = new List<IProgressReader>();

        public GameObject HeroGameObject { get; private set; }

        public GameFactory(IAssetProvider assets, IStaticDataService staticData)
        {
            this.assets = assets;
            this.staticData = staticData;
        }

        public GameObject CreateHero()
        {
            HeroGameObject = InstantiateRegisteredObject(AssetsPath.Player, GameObject.FindObjectOfType<PlayerInitPoint>().Point);
            LazyPlayer.Value = HeroGameObject.GetComponent<Player>();
            return HeroGameObject;
        }

        public GameObject CreateHUD()
            => InstantiateRegisteredObject(AssetsPath.HUD);
        public Enemy CreateEnemy(EnemyTypeID enemyTypeID, Transform parent)
        {
            var data = staticData.GetDataForEnemy(enemyTypeID);
            var enemy = GameObject.Instantiate<Enemy>(data.Prefab, parent.position, Quaternion.identity, parent);
            enemy.Construct(data, LazyPlayer);
            return enemy;
        }

        public void CleanUp()
        {
            ProgressReaders.Clear();
            Savables.Clear();
        }
        public void Register(IProgressReader reader)
        {
            if (reader is ISavable)
                Savables.Add(reader as ISavable);
            ProgressReaders.Add(reader);
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

        public GameObject CreateWeapon(WeaponItem weapon)
            => GameObject.Instantiate(weapon.Model.gameObject);
    }
}
