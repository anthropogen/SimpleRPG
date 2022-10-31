using UnityEngine;

namespace SimpleRPG.Items
{
    public abstract class WeaponItem : InventoryItem
    {
        [field: SerializeField, Range(0, 1000)] public float Damage { get; private set; }
        [field: SerializeField] public WeaponModel Model { get; private set; }
        public abstract int AnimationHash { get; }
    }
}
