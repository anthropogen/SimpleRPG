using SimpleRPG.Infrastructure;
using SimpleRPG.Items;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace SimpleRPG.UI
{
    public class EquipmentSlotView : GameEntity, IDragContainer<InventoryItem>, IItemHolder
    {
        [SerializeField] private EquipLocation location;
        [SerializeField] private InventoryDragItem dragItem;
        [SerializeField] private ItemIcon icon;
        [SerializeField] private Transform dragContainer;
        [SerializeField] private ItemTootlip itemTootlip;
        private Equipment equipment;

        public void Construct(Equipment equipment)
        {
            if (this.equipment != null)
                this.equipment.EquipedItemsChanged -= Redraw;
            this.equipment = equipment;
            equipment.EquipedItemsChanged += Redraw;
            dragItem.Construct(this, dragContainer);
            dragItem.GetComponent<ItemTootlipSpawner>().Construct(itemTootlip);
            Redraw();
        }

        public void AddItem(InventoryItem item, int count)
        {
            equipment.AddItem(location, item as EquipItem);
            SetIcon(item);
        }

        public int GetCount()
        {
            if (GetItem() != null)
                return 1;
            return 0;
        }

        public InventoryItem GetItem()
            => equipment.GetItemFrom(location);

        public int MaxAcceptable(InventoryItem item)
        {
            var equipItem = item as EquipItem;
            if (equipItem == null) return 0;
            if (equipItem.Location != location) return 0;
            if (GetItem() != null) return 0;

            return 1;
        }

        public void RemoveItem(int count)
            => equipment.RemoveItemFrom(location);

        private void SetIcon(InventoryItem item)
            => icon.SetIcon(item, 1);

        private void Redraw()
            => SetIcon(equipment.GetItemFrom(location));

        private void OnDestroy()
        {
            if (equipment != null)
                equipment.EquipedItemsChanged -= Redraw;
        }
    }
}