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
        Task<GameObject> CreateEnemy(EnemyTypeID enemyTypeID, Transform transform);
        GameObject CreateHero();
        GameObject CreateHUD();
        LevelTransfer CreateLevelTransfer(Vector3 position, string nextLevel);
        PickupItem CreateLoot(InventoryItem itemToSpawn);
        PickupItem CreateLootFor(EnemyTypeID enemyTypeID);
        Projectile CreateProjectile(ProjectileType projectileType);
        EnemySpawner CreateSpawner(Vector3 position, EnemyTypeID enemyTypeID, string iD);
        GameObject CreateWeapon(WeaponItem weapon);
    }
}