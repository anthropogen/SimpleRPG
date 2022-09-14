using System;

namespace EpicRPG.Services.PersistentData
{
    [Serializable]
    public class PersistentProgress
    {
        public WorldData WorldData;
        public HeroState HeroState;
        public PersistentProgress(string sceneName)
        {
            WorldData = new WorldData(sceneName);
            HeroState = new HeroState();
        }
    }
}