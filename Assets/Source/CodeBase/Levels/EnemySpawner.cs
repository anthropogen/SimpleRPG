using EpicRPG.Characters.Enemies;
using EpicRPG.Characters.Enemy;
using EpicRPG.Services.GameFactory;
using EpicRPG.Services.PersistentData;
using UnityEngine;

namespace EpicRPG.Levels
{
    public class EnemySpawner : GameEntity, ISavable
    {
        [field: SerializeField] public EnemyTypeID EnemyTypeID { get; private set; }
        [field: SerializeField] public UniqueID UniqueID { get; private set; }

        public bool enemyIsDied;
        private IGameFactory factory;
        private Enemy enemy;

        public void Construct(IGameFactory factory)
        {
            this.factory = factory;
        }

        public void SaveProgress(PersistentProgress progress)
        {
            if (enemyIsDied)
                progress.KillData.ClearedSpawners.Add(UniqueID.SaveID);
        }

        public void LoadProgress(PersistentProgress progress)
        {
            if (progress.KillData.ClearedSpawners.Contains(UniqueID.SaveID))
            {
                enemyIsDied = true;
                if (!progress.WorldData.LootData.PickedItems.ContainsKey(UniqueID.SaveID))
                    factory.CreateLootFor(EnemyTypeID);
            }
            else
                Spawn();
        }

        private void Spawn()
        {
            enemy = factory.CreateEnemy(EnemyTypeID, transform);
            enemy.EnemyDeath += OnEnemyDeath;
        }

        private void OnEnemyDeath(Enemy enemy)
        {
            enemyIsDied = true;
            if (enemy != null)
                enemy.EnemyDeath -= OnEnemyDeath;
        }

        private void OnDestroy()
        {
            if (enemy != null)
                enemy.EnemyDeath -= OnEnemyDeath;
        }
    }
}