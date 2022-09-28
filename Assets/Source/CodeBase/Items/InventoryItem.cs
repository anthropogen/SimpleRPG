using UnityEngine;

namespace EpicRPG.Items
{
    public class InventoryItem : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }
    }
}