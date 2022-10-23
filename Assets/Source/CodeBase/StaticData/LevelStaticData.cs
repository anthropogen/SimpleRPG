using EpicRPG.Levels;
using System.Collections.Generic;
using UnityEngine;

namespace EpicRPG.StaticData
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Static Data/Level")]
    public class LevelStaticData : ScriptableObject
    {
        [field: SerializeField] public string Name { get;  set; }
        [field: SerializeField] public List<EnemySpawnerData> EnemySpawners { get; set; }
    }
}