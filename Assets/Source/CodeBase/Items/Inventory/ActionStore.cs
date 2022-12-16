using SimpleRPG.Infrastructure;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleRPG.Items
{
    public class ActionStore : GameEntity
    {
        private Dictionary<int, ActionItemSlot> actions = new Dictionary<int, ActionItemSlot>();
        public event Action StoreChanged;

        public void AddAction(ActionItem item, int count, int index)
        {
            if (actions.ContainsKey(index))
            {
                if (actions[index].Item.Name == item.Name)
                {
                    actions[index].Count += count;
                }
            }
            else
            {
                actions[index] = new ActionItemSlot(item, count);
            }
            StoreChanged?.Invoke();
        }

        public int MaxAcceptable(ActionItem actionItem, int index)
        {
            if (actionItem == null)
                return 0;
            if (actionItem.IsConsumable)
                return int.MaxValue;

            return 1;
        }

        public int GetCount(int index)
        {
            if (actions.TryGetValue(index, out var slot))
                return slot.Count;
            return -1;
        }

        public InventoryItem GetItem(int index)
        {
            if (actions.TryGetValue(index, out var slot))
                return slot.Item;
            return null;
        }

        public void RemoveItem(int index, int count)
        {
            if (!actions.ContainsKey(index))
                return;
            actions[index].Count -= count;
            StoreChanged?.Invoke();
        }

        public void Use(int index, GameObject user)
        {
            if (!actions.ContainsKey(index))
                return;

            actions[index].Item.Use(user);
            if (actions[index].Item.IsConsumable)
                actions[index].Count--;
            Debug.Log($"used action {actions[index].Item.Name}");

        }
    }
}