using System;

namespace EpicRPG.Services.PersistentData
{
    [Serializable]
    public class HeroState
    {
        public float MaxHP;
        public float CurrentHP;

        public void ResetHP() => CurrentHP = MaxHP;
    }
}