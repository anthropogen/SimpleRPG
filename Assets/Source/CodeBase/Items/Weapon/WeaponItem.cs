using UnityEngine;

namespace SimpleRPG.Items
{
    public abstract class WeaponItem : EquipItem
    {
        [field: SerializeField, Range(0, 1000)] public float Damage { get; private set; }
        [field: SerializeField] public WeaponModel Model { get; private set; }
        public abstract int AnimationHash { get; }
        public override EquipLocation Location => EquipLocation.Weapon;
    }
}
