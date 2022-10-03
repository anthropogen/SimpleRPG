using UnityEngine;

namespace EpicRPG.Items
{
    public abstract class LaunchProjctileWeaponItem : WeaponItem
    {
        [field: SerializeField] public ProjectileType ProjectileType { get; private set; }
    }
}
