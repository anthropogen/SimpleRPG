using UnityEngine;

namespace SimpleRPG.Items
{
    [CreateAssetMenu(fileName = "newMeleeWeapon", menuName = "Items/Weapon/MeleeWeapon", order = 51)]
    public class MeleeWeapon : WeaponItem
    {
        public override int AnimationHash => AnimationConstants.MeleeWeapon;
    }

}
