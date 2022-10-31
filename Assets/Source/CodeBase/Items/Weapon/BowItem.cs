using UnityEngine;

namespace SimpleRPG.Items
{
    [CreateAssetMenu(fileName = "newBow", menuName = "Items/Weapon/Bow", order = 51)]
    public class BowItem : LaunchProjctileWeaponItem
    {
        public override int AnimationHash => AnimationConstants.Bow;
    }
}
