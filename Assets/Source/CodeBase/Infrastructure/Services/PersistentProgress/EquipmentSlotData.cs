using SimpleRPG.Items;
using System;

namespace SimpleRPG.Services.PersistentData
{
    [Serializable]
    public class EquipmentSlotData
    {
        public EquipLocation Location;
        public string Name;

        public EquipmentSlotData(EquipLocation location, EquipItem item)
        {
            Location = location;
            Name = item.Name;
        }
    }
}