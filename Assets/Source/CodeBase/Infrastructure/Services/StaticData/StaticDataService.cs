using EpicRPG.Characters.Enemy;
using EpicRPG.Services;
using EpicRPG.StaticData;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StaticDataService : IStaticDataService
{
    private Dictionary<EnemyTypeID, EnemyStaticData> monsters;
    private const string path = "StaticData/Monsters";

    public void LoadMonsters()
    {
        monsters = Resources.LoadAll<EnemyStaticData>(path).
            ToDictionary(x => x.TypeID, x => x);
    }

    public EnemyStaticData GetDataForEnemy(EnemyTypeID typeID)
    {
        if (monsters.TryGetValue(typeID, out EnemyStaticData enemyData))
            return enemyData;
        return null;
    }
}

