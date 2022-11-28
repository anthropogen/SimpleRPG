using SimpleRPG.Characters.Enemies;
using SimpleRPG.Infrastructure;
using SimpleRPG.Items;
using SimpleRPG.Services.GameFactory;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class LootSpawner : GameEntity
{
    [SerializeField] private Enemy enemy;
    public string SaveId;
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

    public async void SpawnLoot()
    {
        var loot = await gameFactory.CreateLoot(itemToSpawn);
        loot.transform.position = transform.position + Random.insideUnitSphere * 2;
        loot.SaveID = SaveId;
    }

    private void OnEnemyDeath(Enemy enemy)
    {
        this.enemy.EnemyDeath -= OnEnemyDeath;
        SpawnLoot();
    }
}

