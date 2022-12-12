using System;
using System.Collections.Generic;

namespace SimpleRPG.Services.PersistentData
{
    [Serializable]
    public class InventoriesState
    {
        public List<InventoryData> Inventories = new List<InventoryData>();
    }
}