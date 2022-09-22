using EpicRPG.Characters.Enemies;
using EpicRPG.Characters.Enemy;
using UnityEngine;

namespace EpicRPG.StaticData
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Static Data/Enemy")]
    public class EnemyStaticData : ScriptableObject
    {
        [field: SerializeField, Range(1, 1000)] public float HP { get; private set; }
        [field: SerializeField, Range(1, 1000)] public float Damage { get; private set; }
        [field: SerializeField, Range(1, 1000)] public float Delay { get; private set; }
        [field: SerializeField] public EnemyTypeID TypeID { get; private set; }
        [field: SerializeField] public Enemy Prefab { get; private set; }
    }
}