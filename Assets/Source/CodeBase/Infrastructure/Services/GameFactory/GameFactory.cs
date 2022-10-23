using EpicRPG.Characters.Enemies;
using EpicRPG.Characters.Enemy;
using EpicRPG.Hero;
using EpicRPG.Items;
using EpicRPG.Levels;
using EpicRPG.Services.AssetManagement;
using EpicRPG.Services.PersistentData;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace EpicRPG.Services.GameFactory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider assets;
        private readonly IStaticDataService staticData;
        private readonly IPersistentProgressService progressService;
        public LazyInitializy<Player> LazyPlayer { get; private set; } = new LazyInitializy<Player>();
        public List<ISavable> Savables { get; } = new List<ISavable>();
        public List<IProgressReader> ProgressReaders { get; } = new List<IProgressReader>();

        public GameObject HeroGameObject { get; private set; }

        public GameFactory(IAssetProvider assets, IStaticDataService staticData, IPersistentProgressService progressService)
        {
            this.assets = assets;
            this.staticData = staticData;
            this.progressService = progressService;
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
            enemy.GetComponent<NavMeshAgent>().speed = data.Speed;
            enemy.Construct(data, LazyPlayer);
            enemy.GetComponent<LootSpawner>().Construct(this, data.ItemToSpawn);
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

        public Projectile CreateProjectile(ProjectileType projectileType)
        {
            var template = staticData.GetProjectile(projectileType);
            return GameObject.Instantiate(template);
        }

        public PickupItem CreateLoot(InventoryItem itemToSpawn)
        {
            var pickUp = InstantiateRegisteredObject(AssetsPath.Loot).GetComponent<PickupItem>();
            pickUp.Construct(progressService.Progress);
            pickUp.Item = itemToSpawn;
            return pickUp;
        }

        public PickupItem CreateLootFor(EnemyTypeID enemyTypeID)
        {
            var pickUp = InstantiateRegisteredObject(AssetsPath.Loot).GetComponent<PickupItem>();
            pickUp.Construct(progressService.Progress);
            pickUp.Item = staticData.GetDataForEnemy(enemyTypeID).ItemToSpawn;
            return pickUp;
        }

        public EnemySpawner CreateSpawner(Vector3 position, EnemyTypeID enemyType, string ID)
        {
            var spawner = InstantiateRegisteredObject(AssetsPath.Spawner).GetComponent<EnemySpawner>();
            spawner.Construct(this);
            spawner.transform.position = position;
            spawner.EnemyTypeID = enemyType;
            spawner.SaveID = ID;
            return spawner;
        }
    }
}
