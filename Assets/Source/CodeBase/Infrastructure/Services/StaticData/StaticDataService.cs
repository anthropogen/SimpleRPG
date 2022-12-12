using SimpleRPG.Characters.Enemy;
using SimpleRPG.Items;
using SimpleRPG.StaticData;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StaticDataService : IStaticDataService
{
    private Dictionary<EnemyTypeID, EnemyStaticData> monsters;
    private List<InventoryItem> allItems;
    private ProjectileStaticData projectileStaticData;
    private List<LevelStaticData> levelsData;
    private const string monsterpath = "StaticData/Monsters";
    private const string itemsPath = "Items";
    private const string projectilesPath = "StaticData/ProjectileData";
    private const string levelsPath = "StaticData/Levels";

    public EnemyStaticData GetDataForEnemy(EnemyTypeID typeID)
    {
        if (monsters.TryGetValue(typeID, out EnemyStaticData enemyData))
            return enemyData;
        return null;
    }

    public Projectile GetProjectile(ProjectileType type)
        => projectileStaticData.GetProjectileTempate(type);

    public LevelStaticData GetLevelDataFor(string sceneKey)
        => levelsData.FirstOrDefault(l => l.Name == sceneKey);

    public WeaponItem GetWeapon(string weapon)
        => allItems.FirstOrDefault(i => i.Name == weapon) as WeaponItem;

    public InventoryItem GetDataForItem(string name)
        => allItems.FirstOrDefault(i => i.Name == name);

    public void Load()
    {
        LoadMonsters();
        LoadInventoryItems();
        LoadLevelData();
        LoadProjectilesData();
    }

    private void LoadMonsters()
        => monsters = Resources.LoadAll<EnemyStaticData>(monsterpath).
            ToDictionary(x => x.TypeID, x => x);
    private void LoadInventoryItems()
        => allItems = Resources.LoadAll<InventoryItem>(itemsPath).ToList();

    private void LoadLevelData()
        => levelsData = Resources.LoadAll<LevelStaticData>(levelsPath).ToList();

    private void LoadProjectilesData()
        => projectileStaticData = Resources.Load<ProjectileStaticData>(projectilesPath);


}

