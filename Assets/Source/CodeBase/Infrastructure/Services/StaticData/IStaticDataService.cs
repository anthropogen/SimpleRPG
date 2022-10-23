using EpicRPG.Characters.Enemy;
using EpicRPG.Items;
using EpicRPG.Services;
using EpicRPG.StaticData;

public interface IStaticDataService : IService
{
    void Load();
    EnemyStaticData GetDataForEnemy(EnemyTypeID typeID);
    WeaponItem GetWeapon(string weapon);
    Projectile GetProjectile(ProjectileType type);
    LevelStaticData GetLevelDataFor(string sceneKey);
}