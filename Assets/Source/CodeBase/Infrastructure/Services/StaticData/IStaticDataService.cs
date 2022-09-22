using EpicRPG.Characters.Enemy;
using EpicRPG.Services;
using EpicRPG.StaticData;

public interface IStaticDataService : IService
{
    void LoadMonsters();
    EnemyStaticData GetDataForEnemy(EnemyTypeID typeID);
}