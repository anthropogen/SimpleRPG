using SimpleRPG.Characters.Enemies;
using SimpleRPG.Characters.Enemy;
using SimpleRPG.Hero;
using SimpleRPG.Infrastructure.GameStateMachine;
using SimpleRPG.Items;
using SimpleRPG.Levels;
using SimpleRPG.Services.AssetManagement;
using SimpleRPG.Services.PersistentData;
using SimpleRPG.Services.WindowsService;
using SimpleRPG.UI;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AI;

namespace SimpleRPG.Services.GameFactory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider assets;
        private readonly IStaticDataService staticData;
        private readonly IPersistentProgressService progressService;
        private readonly IGameStateMachine gameStateMachine;
        private readonly IWindowsService windowsService;

        public LazyInitializy<Player> LazyPlayer { get; private set; } = new LazyInitializy<Player>();
        public List<ISavable> Savables { get; } = new List<ISavable>();
        public List<IProgressReader> ProgressReaders { get; } = new List<IProgressReader>();

        public GameObject HeroGameObject { get; private set; }

        public GameFactory(IAssetProvider assets, IStaticDataService staticData, IPersistentProgressService progressService, IGameStateMachine gameStateMachine, IWindowsService windowsService)
        {
            this.assets = assets;
            this.staticData = staticData;
            this.progressService = progressService;
            this.gameStateMachine = gameStateMachine;
            this.windowsService = windowsService;
        }

        public async void WarmUp()
        {
            await assets.Load<GameObject>(AssetsAddress.Loot);
            await assets.Load<GameObject>(AssetsAddress.Spawner);
            await assets.Load<GameObject>(AssetsAddress.LevelTransfer);
            await assets.Load<GameObject>(AssetsAddress.Player);
        }

        public async Task<GameObject> CreateHero()
        {
            var template = await assets.Load<GameObject>(AssetsAddress.Player);
            HeroGameObject = InstantiateRegisteredObject(template);
            LazyPlayer.Value = HeroGameObject.GetComponent<Player>();
            return HeroGameObject;
        }

        public async Task<GameObject> CreateEnemy(EnemyTypeID enemyTypeID, Transform parent)
        {
            var data = staticData.GetDataForEnemy(enemyTypeID);

            var handle = Addressables.LoadAssetAsync<GameObject>(data.Prefab);
            var prefab = await handle.Task;
            Addressables.Release(handle);

            var enemy = GameObject.Instantiate(prefab, parent.position, Quaternion.identity, parent);

            enemy.GetComponent<NavMeshAgent>().speed = data.Speed;
            enemy.GetComponent<Enemy>().Construct(data, LazyPlayer);
            enemy.GetComponent<LootSpawner>().Construct(this, data.ItemToSpawn);
            return enemy;
        }
        public async Task<GameObject> CreateHUD()
        {
            var prefab = await assets.Load<GameObject>(AssetsAddress.HUD);
            var result = InstantiateRegisteredObject(prefab);
            foreach (var button in result.GetComponentsInChildren<WindowButton>())
            {
                button.Construct(windowsService);
            }

            return result;
        }
        public void CleanUp()
        {
            ProgressReaders.Clear();
            Savables.Clear();
            assets.Cleanup();
        }

        public void Register(IProgressReader reader)
        {
            if (reader is ISavable)
                Savables.Add(reader as ISavable);
            ProgressReaders.Add(reader);
        }

        public GameObject InstantiateRegisteredObject(GameObject template)
        {
            var regObject = GameObject.Instantiate(template);
            RegisterProgressWatchers(regObject);
            return regObject;
        }

        private GameObject InstantiateRegisteredObject(GameObject template, Vector3 position)
        {
            var regObject = GameObject.Instantiate(template, position, Quaternion.identity); ;
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

        public async Task<PickupItem> CreateLoot(InventoryItem itemToSpawn)
        {
            var template = await assets.Load<GameObject>(AssetsAddress.Loot);
            var pickUp = InstantiateRegisteredObject(template).GetComponent<PickupItem>();
            pickUp.Construct(progressService.Progress);
            pickUp.Item = itemToSpawn;
            return pickUp;
        }

        public async Task<PickupItem> CreateLootFor(EnemyTypeID enemyTypeID)
        {
            var template = await assets.Load<GameObject>(AssetsAddress.Loot);
            var pickUp = InstantiateRegisteredObject(template).GetComponent<PickupItem>();
            pickUp.Construct(progressService.Progress);
            pickUp.Item = staticData.GetDataForEnemy(enemyTypeID).ItemToSpawn;
            return pickUp;
        }

        public async Task<EnemySpawner> CreateSpawner(Vector3 position, EnemyTypeID enemyType, string ID)
        {
            var template = await assets.Load<GameObject>(AssetsAddress.Spawner);
            var spawner = InstantiateRegisteredObject(template, position).GetComponent<EnemySpawner>();
            spawner.Construct(this);
            spawner.EnemyTypeID = enemyType;
            spawner.SaveID = ID;
            return spawner;
        }

        public async Task<LevelTransfer> CreateLevelTransfer(Vector3 position, string nextLevel)
        {
            var template = await assets.Load<GameObject>(AssetsAddress.LevelTransfer);
            var levelTransfer = InstantiateRegisteredObject(template).GetComponent<LevelTransfer>();
            levelTransfer.Construct(gameStateMachine, nextLevel);
            levelTransfer.transform.position = position;
            return levelTransfer;
        }
    }
}
