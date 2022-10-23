using UnityEngine;

namespace EpicRPG.Items
{
    [CreateAssetMenu(fileName = "newItem", menuName = "Items/New Item", order = 51)]
    public class InventoryItem : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }
    }
}