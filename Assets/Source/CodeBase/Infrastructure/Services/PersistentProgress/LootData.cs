using EpicRPG.Items;
using System;
using System.Collections.Generic;

namespace EpicRPG.Services.PersistentData
{
    [Serializable]
    public class LootOnLevelData
    {
        public Dictionary<string, LootData> PickedItems = new Dictionary<string, LootData>();
        public void Pickup(PickupItem pickupItem)
        {
            if (!PickedItems.ContainsKey(pickupItem.UniqueID.SaveID))
            {
                PickedItems[pickupItem.UniqueID.SaveID] = new LootData();
            }
            PickedItems[pickupItem.UniqueID.SaveID].Items.Add(pickupItem.Item);
        }

        [Serializable]
        public class LootData
        {
            public List<InventoryItem> Items = new List<InventoryItem>();
        }
    }
}