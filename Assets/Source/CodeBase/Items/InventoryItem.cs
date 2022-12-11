using UnityEngine;

namespace SimpleRPG.Items
{
    [CreateAssetMenu(fileName = "newItem", menuName = "Items/New Item", order = 51)]
    public class InventoryItem : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public string Description { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public bool IsStackable { get; private set; }
    }
}