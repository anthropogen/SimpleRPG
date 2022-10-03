using EpicRPG.Characters.Enemy;
using EpicRPG.Items;
using EpicRPG.StaticData;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StaticDataService : IStaticDataService
{
    private Dictionary<EnemyTypeID, EnemyStaticData> monsters;
    private List<InventoryItem> allItems;
    private const string monsterpath = "StaticData/Monsters";
    private const string itemsPath = "Items";
    private const string projectilesPath = "StaticData/ProjectileData";
    public void LoadMonsters()
    {
        monsters = Resources.LoadAll<EnemyStaticData>(monsterpath).
            ToDictionary(x => x.TypeID, x => x);
    }
    public void LoadInventoryItems()
    {
        allItems = Resources.LoadAll<InventoryItem>(itemsPath).ToList();
    }

    public EnemyStaticData GetDataForEnemy(EnemyTypeID typeID)
    {
        if (monsters.TryGetValue(typeID, out EnemyStaticData enemyData))
            return enemyData;
        return null;
    }


    public WeaponItem GetWeapon(string weapon)
    {
        return allItems.FirstOrDefault(i => i.Name == weapon) as WeaponItem;
    }
}

