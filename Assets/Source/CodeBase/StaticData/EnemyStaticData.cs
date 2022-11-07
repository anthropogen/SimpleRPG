using SimpleRPG.Characters.Enemy;
using SimpleRPG.Items;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SimpleRPG.StaticData
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Static Data/Enemy")]
    public class EnemyStaticData : ScriptableObject
    {
        [field: SerializeField, Range(1, 1000)] public float HP { get; private set; }
        [field: SerializeField, Range(1, 1000)] public float Damage { get; private set; }
        [field: SerializeField, Range(1, 1000)] public float Speed { get; private set; }
        [field: SerializeField, Range(1, 1000)] public float Delay { get; private set; }
        [field: SerializeField] public InventoryItem ItemToSpawn { get; private set; }
        [field: SerializeField] public EnemyTypeID TypeID { get; private set; }
        [field: SerializeField] public AssetReferenceGameObject Prefab { get; private set; }
    }
}