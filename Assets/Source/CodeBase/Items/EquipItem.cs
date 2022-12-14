using UnityEngine;

namespace SimpleRPG.Items
{
    public abstract class EquipItem : InventoryItem
    {
        [field: SerializeField] public virtual EquipLocation Location { get; private set; }
    }
}