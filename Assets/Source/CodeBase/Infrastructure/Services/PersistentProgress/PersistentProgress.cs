using System;
using System.Collections.Generic;

namespace SimpleRPG.Services.PersistentData
{
    [Serializable]
    public class PersistentProgress
    {
        public WorldData WorldData;
        public HeroState HeroState;
        public KillData KillData;
        public InventoriesState InventoriesState;
        public List<EquipmentSlotData> Equipment = new List<EquipmentSlotData>();
        public List<SlotData> Actions = new List<SlotData>();

        public PersistentProgress(string sceneName)
        {
            KillData = new KillData();
            WorldData = new WorldData(sceneName);
            HeroState = new HeroState();
            InventoriesState = new InventoriesState();
        }
    }
}