using SimpleRPG.Characters.Enemy;
using System;
using UnityEngine;

namespace SimpleRPG.StaticData
{
    [Serializable]
    public class EnemySpawnerData
    {
        public EnemyTypeID EnemyTypeID;
        public string ID;
        public Vector3 Position;

        public EnemySpawnerData(EnemyTypeID enemyTypeID, string iD, Vector3 position)
        {
            EnemyTypeID = enemyTypeID;
            ID = iD;
            Position = position;
        }
    }
}