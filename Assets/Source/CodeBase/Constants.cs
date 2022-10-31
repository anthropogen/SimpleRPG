using UnityEngine;

namespace SimpleRPG
{
    public static class AnimationConstants
    {
        public static readonly int Unarmed = Animator.StringToHash("Unarmed");
        public static readonly int MeleeWeapon = Animator.StringToHash("Melee");
        public static readonly int Bow = Animator.StringToHash("Bow");
        public static readonly int Spells = Animator.StringToHash("Spells");
    }
}