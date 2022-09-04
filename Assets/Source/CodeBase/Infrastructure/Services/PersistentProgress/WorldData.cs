using System;

namespace EpicRPG.Services.PersistentData
{
    [Serializable]
    public class WorldData
    {
        public PositionOnLevel PositionOnLevel;

        public WorldData(string sceneName)
        {
            PositionOnLevel = new PositionOnLevel(sceneName, null);
        }
    }
}