using UnityEngine;

namespace EpicRPG.Items
{
    [CreateAssetMenu(fileName = "newUnarmed", menuName = "Items/Weapon/Unarmed", order = 51)]
    public class Unarmed : MeleeWeapon
    {
        public override int AnimationHash => AnimationConstants.Unarmed;
    }
}
