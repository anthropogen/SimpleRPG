using System;

namespace EpicRPG.Services.PersistentData
{
    [Serializable]
    public class WorldData
    {
        public PositionOnLevel PositionOnLevel;
        public LootOnLevelData LootData;
        public WorldData(string sceneName)
        {
            PositionOnLevel = new PositionOnLevel(sceneName, null);
            LootData = new LootOnLevelData();
        }
    }
}