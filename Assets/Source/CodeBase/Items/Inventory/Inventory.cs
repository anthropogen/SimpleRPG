using SimpleRPG.Infrastructure;
using SimpleRPG.Levels;
using SimpleRPG.Services.PersistentData;
using System;
using System.Linq;
using UnityEngine;

namespace SimpleRPG.Items
{
    public class Inventory : GameEntity, ISavable
    {
        [SerializeField] private UniqueID uniqueID;
        [field: SerializeField, Min(1)] public int Capacity { get; private set; } = 16;
        private InventorySlot[] slots;
        private IStaticDataService staticData;
        public event Action InventoryUpdated;

        private void Awake()
        {
            slots = new InventorySlot[Capacity];
            for (int i = 0; i < slots.Length; i++)
                slots[i] = new InventorySlot();
        }

        public void Construct(IStaticDataService staticData)
            => this.staticData = staticData;

        public bool HasFreeSlot()
            => slots.FirstOrDefault(s => s.Item == null) != null;

        public bool HasItem(InventoryItem item)
            => slots.Any(s => s.Item.Name == item.Name);

        public int GetSlotNumber(InventoryItem item)
        {
            for (int i = 0; i < slots.Length; i++)
                if (slots[i].Item.Name == item.Name)
                    return i;

            return -1;
        }
        public InventoryItem GetItem(int index)
        {
            if (index < 0 || index >= Capacity)
                throw new ArgumentOutOfRangeException($"Inventory doesn't have {index} slot");

            return slots[index].Item;
        }

        public int GetItemCount(InventoryItem item)
        {
            for (int i = 0; i < slots.Length; i++)
                if (slots[i].Item.Name == item.Name)
                    return slots[i].Count;

            return -1;
        }

        public int GetItemCount(int index)
        {
            if (index < 0 || index >= Capacity)
                throw new ArgumentOutOfRangeException($"Inventory doesn't have {index} slot");

            return slots[index].Count;
        }

        public bool AddToFirstEmptySlot(InventoryItem item, int count)
        {
            int index = FindEmptySlot();
            if (index < 0)
                return false;

            slots[index].Item = item;
            slots[index].Count = count;
            InventoryUpdated?.Invoke();
            return true;
        }

        public bool AddItemToSlot(int index, InventoryItem item, int count)
        {
            index = ClampIndex(index);
            if (slots[index].Item != null && slots[index].Item.Name != item.Name)
                return AddToFirstEmptySlot(item, count);

            slots[index].Item = item;
            slots[index].Count += count;
            InventoryUpdated?.Invoke();
            return true;
        }

        public bool RemoveItemFromSlot(int index, int count)
        {
            index = ClampIndex(index);
            if (slots[index].Item != null)
            {
                slots[index].Count -= count;
                InventoryUpdated?.Invoke();
                return true;
            }

            InventoryUpdated?.Invoke();
            return false;
        }

        public bool RemoveItemFromSlot(InventoryItem item, int count)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].Item != null && slots[i].Item.Name == item.Name)
                {
                    slots[i].Count -= count;
                    InventoryUpdated?.Invoke();
                    return true;
                }
            }
            InventoryUpdated?.Invoke();
            return false;
        }

        private int FindEmptySlot()
        {
            for (int i = 0; i < Capacity; i++)
                if (slots[i].Item == null)
                    return i;

            return -1;
        }

        private int ClampIndex(int index)
                => Mathf.Clamp(index, 0, Capacity);


        public void SaveProgress(PersistentProgress progress)
        {
            var data = progress.InventoriesState.Inventories.FirstOrDefault(i => i.ID == uniqueID.SaveID);
            if (data == null)
            {
                data = new InventoryData();
                progress.InventoriesState.Inventories.Add(data);
            }
            data.ID = uniqueID.SaveID;
            data.Items.Clear();
            for (int i = 0; i < Capacity; i++)
            {
                if (slots[i].Item != null)
                    data.Items.Add(new SlotData(slots[i].Item.Name, slots[i].Count));
                else
                    data.Items.Add(null);
            }
        }

        public void LoadProgress(PersistentProgress progress)
        {
            var data = progress.InventoriesState.Inventories.FirstOrDefault(i => i.ID == uniqueID.SaveID);
            if (data == null) return;
            Capacity = data.Items.Count;
            slots = new InventorySlot[Capacity];
            for (int i = 0; i < data.Items.Count; i++)
            {
                slots[i] = new InventorySlot();
                if (data.Items[i] != null)
                {
                    slots[i].Item = staticData.GetDataForItem(data.Items[i].Name);
                    slots[i].Count = data.Items[i].Count;
                }
            }
            InventoryUpdated?.Invoke();
        }
    }
}