using UnityEngine;

namespace SimpleRPG.Items
{
    public abstract class ActionItem : InventoryItem
    {
        [field: SerializeField] public bool IsConsumable;

        public abstract void Use(GameObject user);
    }
}