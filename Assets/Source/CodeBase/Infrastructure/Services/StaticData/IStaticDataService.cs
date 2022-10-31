using SimpleRPG.Characters.Enemy;
using SimpleRPG.Items;
using SimpleRPG.Services;
using SimpleRPG.StaticData;

public interface IStaticDataService : IService
{
    void Load();
    EnemyStaticData GetDataForEnemy(EnemyTypeID typeID);
    WeaponItem GetWeapon(string weapon);
    Projectile GetProjectile(ProjectileType type);
    LevelStaticData GetLevelDataFor(string sceneKey);
}