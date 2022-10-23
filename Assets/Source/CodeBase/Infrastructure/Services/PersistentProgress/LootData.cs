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
            if (!PickedItems.ContainsKey(pickupItem.SaveID))
            {
                PickedItems[pickupItem.SaveID] = new LootData();
            }
            PickedItems[pickupItem.SaveID].Items.Add(pickupItem.Item);
        }

        [Serializable]
        public class LootData
        {
            public List<InventoryItem> Items = new List<InventoryItem>();
        }
    }
}