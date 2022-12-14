using SimpleRPG.Infrastructure;
using SimpleRPG.Items;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SimpleRPG.UI
{
    public class InventorySlotView : GameEntity, IItemHolder, IDragContainer<InventoryItem>
    {
        [SerializeField] private Image icon;
        [SerializeField] private TMP_Text itemCountText;
        [SerializeField] private InventoryDragItem dragItem;
        private int index;
        private Inventory inventory;
        private InventoryItem item;

        public void Construct(Inventory inventory, int index, Transform dragContainer)
        {
            this.index = index;
            this.inventory = inventory;
            item = inventory.GetItem(index);
            int count = inventory.GetItemCount(index);
            dragItem.Construct(this, dragContainer);
            SetIcon(item, count);
        }

        public void AddItem(InventoryItem item, int count)
        {
            inventory.AddItemToSlot(index, item, count);
        }

        public int GetCount()
        {
            return inventory.GetItemCount(index);
        }

        public InventoryItem GetItem()
        {
            return inventory.GetItem(index);
        }

        public int MaxAcceptable(InventoryItem item)
        {
            return int.MaxValue;
        }

        public void RemoveItem(int count)
        {
            inventory.RemoveItemFromSlot(index, count);
        }

        private void SetIcon(InventoryItem item, int count)
        {
            if (item == null)
            {
                icon.enabled = false;
                itemCountText.enabled = false;
                return;
            }
            if (count <= 1)
            {
                itemCountText.enabled = false;
            }
            else
            {
                itemCountText.text = count.ToString();
                itemCountText.enabled = true;
            }
            icon.enabled = true;
            icon.sprite = item.Icon;
        }
    }
}