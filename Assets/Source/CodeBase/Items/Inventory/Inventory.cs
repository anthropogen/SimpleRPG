using SimpleRPG.Infrastructure;
using System;
using System.Linq;
using UnityEngine;

namespace SimpleRPG.Items
{
    public class Inventory : GameEntity
    {
        [field: SerializeField, Min(1)] public int Capacity { get; private set; } = 16;
        private InventorySlot[] slots;
        public event Action InventoryUpdated;

        private void Awake()
        {
            slots = new InventorySlot[Capacity];
            for (int i = 0; i < slots.Length; i++)
                slots[i] = new InventorySlot();
        }

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
    }
}