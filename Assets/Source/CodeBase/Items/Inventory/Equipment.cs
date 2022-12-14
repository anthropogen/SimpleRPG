using SimpleRPG.Infrastructure;
using SimpleRPG.Services.PersistentData;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleRPG.Items
{
    public class Equipment : GameEntity, ISavable
    {
        private Dictionary<EquipLocation, EquipItem> equipedItems = new Dictionary<EquipLocation, EquipItem>();
        private IStaticDataService staticData;
        public event Action EquipedItemsChanged;

        public void Construct(IStaticDataService staticData)
            => this.staticData = staticData;

        public EquipItem GetItemFrom(EquipLocation location)
        {
            if (equipedItems.TryGetValue(location, out var item))
                return item;

            return null;
        }

        public void AddItem(EquipLocation location, EquipItem item)
        {
            Debug.Assert(item.Location == location);

            equipedItems[location] = item;
            EquipedItemsChanged?.Invoke();
        }

        public void RemoveItemFrom(EquipLocation location)
        {
            equipedItems.Remove(location);
            EquipedItemsChanged?.Invoke();
        }

        public void SaveProgress(PersistentProgress progress)
        {
            progress.Equipment.Clear();
            foreach (var location in equipedItems.Keys)
            {
                if (equipedItems[location] != null)
                    progress.Equipment.Add(new EquipmentSlotData(location, equipedItems[location]));
            }
        }
        public void LoadProgress(PersistentProgress progress)
        {
            equipedItems.Clear();
            foreach (var item in progress.Equipment)
            {
                equipedItems[item.Location] = staticData.GetDataForItem(item.Name) as EquipItem;
            }
            EquipedItemsChanged?.Invoke();
        }
    }

}