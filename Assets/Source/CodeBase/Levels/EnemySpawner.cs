using SimpleRPG.Characters.Enemies;
using SimpleRPG.Characters.Enemy;
using SimpleRPG.Infrastructure;
using SimpleRPG.Services.GameFactory;
using SimpleRPG.Services.PersistentData;

namespace SimpleRPG.Levels
{
    public class EnemySpawner : GameEntity, ISavable
    {
        private IGameFactory factory;
        private Enemy enemy;
        private bool enemyIsDied;
        public string SaveID { get; set; }
        public EnemyTypeID EnemyTypeID { get; set; }

        public void Construct(IGameFactory factory)
        {
            this.factory = factory;
        }

        public void SaveProgress(PersistentProgress progress)
        {
            if (enemyIsDied)
                progress.KillData.ClearedSpawners.Add(SaveID);
        }

        public async void LoadProgress(PersistentProgress progress)
        {
            if (progress.KillData.ClearedSpawners.Contains(SaveID))
            {
                enemyIsDied = true;
                if (!progress.WorldData.LootData.PickedItems.ContainsKey(SaveID))
                {
                    var loot = await factory.CreateLootFor(EnemyTypeID);
                    loot.SaveID = SaveID;
                }
            }
            else
                Spawn();
        }

        private async void Spawn()
        {
            var enemyObject = await factory.CreateEnemy(EnemyTypeID, transform);
            enemy = enemyObject.GetComponent<Enemy>();
            enemy.GetComponent<LootSpawner>().SaveId = SaveID;
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