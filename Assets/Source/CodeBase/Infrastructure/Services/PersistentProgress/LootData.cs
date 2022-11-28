using SimpleRPG.Items;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleRPG.Services.PersistentData
{
    [Serializable]
    public class LootOnLevelData
    {
        public List<LootData> PickedItems = new List<LootData>();

        public void Pickup(PickupItem pickupItem)
        {
            var loodData = PickedItems.FirstOrDefault(l => l.ID == pickupItem.SaveID);
            if (loodData == null)
            {
                loodData = new LootData();
                loodData.ID = pickupItem.SaveID;
                PickedItems.Add(loodData);
            }
            loodData.Items.Add(pickupItem.Item);
        }

        public bool Has(string saveID)
            => PickedItems.FirstOrDefault(l => l.ID == saveID) != null;

        [Serializable]
        public class LootData
        {
            public string ID;
            public List<InventoryItem> Items = new List<InventoryItem>();
        }
    }
}