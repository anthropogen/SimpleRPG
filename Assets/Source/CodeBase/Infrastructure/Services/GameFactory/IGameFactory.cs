using SimpleRPG.Characters.Enemies;
using SimpleRPG.Characters.Enemy;
using SimpleRPG.Hero;
using SimpleRPG.Items;
using SimpleRPG.Levels;
using SimpleRPG.Services.PersistentData;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace SimpleRPG.Services.GameFactory
{
    public interface IGameFactory : IService
    {
        List<ISavable> Savables { get; }
        List<IProgressReader> ProgressReaders { get; }
        GameObject HeroGameObject { get; }
        LazyInitializy<Player> LazyPlayer { get; }
        void CleanUp();
        Task<GameObject> CreateHUD();
        Task<GameObject> CreateNPC(string id, Transform transform);
        Task<GameObject> CreateEnemy(EnemyTypeID enemyTypeID, Transform transform);
        Task<GameObject> CreateHero();
        Task<LevelTransfer> CreateLevelTransfer(Vector3 position, string nextLevel);
        Task<PickupItem> CreateLoot(InventoryItem itemToSpawn, int count);
        Task<PickupItem> CreateLootFor(EnemyTypeID enemyTypeID);
        Projectile CreateProjectile(ProjectileType projectileType);
        Task<EnemySpawner> CreateEnemySpawner(Vector3 position, EnemyTypeID enemyTypeID, string iD);
        GameObject CreateWeapon(WeaponItem weapon);
        GameObject InstantiateRegisteredObject(GameObject template);
        void WarmUp();
        Task<NPCSpawner> CreateNPCSpawner(Vector3 position, Quaternion rotation, string ID, string saveID);
    }
}