using Codice.CM.Common;
using UnityEngine;

namespace EpicRPG.Items
{
    public abstract class WeaponItem : InventoryItem
    {
        [field: SerializeField, Range(0, 1000)] public float Damage { get; private set; }
        [field: SerializeField] public WeaponModel Model { get; private set; }
        public abstract int AnimationHash { get; }
        // [field: SerializeField] public WeaponGripType GripType { get; private set; }
    }
}
