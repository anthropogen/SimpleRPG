using SimpleRPG.Characters.Enemy;
using SimpleRPG.Infrastructure;
using UnityEngine;

namespace SimpleRPG.Levels
{
    public class EnemySpawnMarker : SpawnMarker
    {
        public EnemyTypeID EnemyTypeID;
        public override Color DrawColor => Color.red;
    }
}