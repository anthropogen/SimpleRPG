using SimpleRPG.Levels;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleRPG.StaticData
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Static Data/Level")]
    public class LevelStaticData : ScriptableObject
    {
        [field: SerializeField] public string Name { get; set; }
        [field: SerializeField] public List<EnemySpawnerData> EnemySpawners { get; set; }
        [field: SerializeField] public List<LevelTransferData> LevelTransfers { get; set; }
        [field: SerializeField] public List<NPCSpawnerData> NPCSpawners { get; set; }
        [field: SerializeField] public Vector3 InitialPlayerPoint { get; set; }
    }
}