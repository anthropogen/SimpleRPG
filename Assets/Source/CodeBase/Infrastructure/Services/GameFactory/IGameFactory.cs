using EpicRPG.Characters.Enemies;
using EpicRPG.Characters.Enemy;
using EpicRPG.Hero;
using EpicRPG.Items;
using EpicRPG.Levels;
using EpicRPG.Services.PersistentData;
using System.Collections.Generic;
using UnityEngine;

namespace EpicRPG.Services.GameFactory
{
    public interface IGameFactory : IService
    {
        List<ISavable> Savables { get; }
        List<IProgressReader> ProgressReaders { get; }
        GameObject HeroGameObject { get; }
        LazyInitializy<Player> LazyPlayer { get; }
        void CleanUp();
        Enemy CreateEnemy(EnemyTypeID enemyTypeID, Transform transform);
        GameObject CreateHero();
        GameObject CreateHUD();
        PickupItem CreateLoot(InventoryItem itemToSpawn);
        PickupItem CreateLootFor(EnemyTypeID enemyTypeID);
        Projectile CreateProjectile(ProjectileType projectileType);
        EnemySpawner CreateSpawner(Vector3 position, EnemyTypeID enemyTypeID, string iD);
        GameObject CreateWeapon(WeaponItem weapon);
    }
}