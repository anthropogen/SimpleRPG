using EpicRPG.Characters.Enemy;
using EpicRPG.Items;
using EpicRPG.Services;
using EpicRPG.StaticData;

public interface IStaticDataService : IService
{
    void LoadMonsters();
    void LoadInventoryItems();
    EnemyStaticData GetDataForEnemy(EnemyTypeID typeID);
    WeaponItem GetWeapon(string weapon);
    void LoadProjectilesData();
    Projectile GetProjectile(ProjectileType type);
}