using System;

namespace SimpleRPG.Services.PersistentData
{
    [Serializable]
    public class PersistentProgress
    {
        public WorldData WorldData;
        public HeroState HeroState;
        public KillData KillData;
        public string Weapon;
        public PersistentProgress(string sceneName)
        {
            KillData = new KillData();
            WorldData = new WorldData(sceneName);
            HeroState = new HeroState();
        }
    }
}