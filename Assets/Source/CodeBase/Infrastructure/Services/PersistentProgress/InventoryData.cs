using System;
using System.Collections.Generic;

namespace SimpleRPG.Services.PersistentData
{
    [Serializable]
    public class InventoryData
    {
        public string ID;
        public List<SlotData> Items = new List<SlotData>();
    }
}