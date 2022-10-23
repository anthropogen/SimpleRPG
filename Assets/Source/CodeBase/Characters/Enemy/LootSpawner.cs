using EpicRPG.Characters.Enemies;
using EpicRPG.Items;
using EpicRPG.Services.GameFactory;
using UnityEngine;

public class LootSpawner : GameEntity
{
    [SerializeField] private Enemy enemy;
    private IGameFactory gameFactory;
    private InventoryItem itemToSpawn;

    public void Construct(IGameFactory gameFactory, InventoryItem spawnItem)
    {
        this.gameFactory = gameFactory;
        itemToSpawn = spawnItem;
    }

    protected override void Enable()
    {
        enemy.EnemyDeath += OnEnemyDeath;
    }


    protected override void Disable()
    {
        enemy.EnemyDeath -= OnEnemyDeath;
    }

    public void SpawnLoot()
    {
        var loot = gameFactory.CreateLoot(itemToSpawn);
        loot.transform.position = transform.position + UnityEngine.Random.insideUnitSphere * 2;
    }
    private void OnEnemyDeath(Enemy obj)
    {
        enemy.EnemyDeath -= OnEnemyDeath;
        SpawnLoot();
    }
}

