using System;

namespace EpicRPG.Services.PersistentData
{
    [Serializable]
    public class PersistentProgress
    {
        public WorldData WorldData;

        public PersistentProgress(string sceneName)
        {
            WorldData = new WorldData(sceneName);
        }
    }
}