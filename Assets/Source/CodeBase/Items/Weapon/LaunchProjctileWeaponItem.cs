using UnityEngine;

namespace SimpleRPG.Items
{
    public abstract class LaunchProjctileWeaponItem : WeaponItem
    {
        [field: SerializeField] public ProjectileType ProjectileType { get; private set; }
    }
}
