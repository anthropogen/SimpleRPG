using SimpleRPG.Infrastructure;
using SimpleRPG.Items;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace SimpleRPG.UI
{
    public class ActionItemSlotView : GameEntity, IDragContainer<InventoryItem>
    {
        [SerializeField, Range(0, 10)] private int index;
        [SerializeField] private ItemIcon icon;
        [SerializeField] private InventoryDragItem dragItem;
        [SerializeField] private Transform dragContainer;
        [SerializeField] private ItemTootlip itemTootlip;
        [SerializeField] private Button useButton;

        private ActionStore actionStore;

        public void Construct(ActionStore store)
        {
            if (actionStore != null)
                actionStore.StoreChanged -= Redraw;
            actionStore = store;
            actionStore.StoreChanged += Redraw;
            dragItem.Construct(this, dragContainer);
            dragItem.GetComponent<ItemTootlipSpawner>().Construct(itemTootlip);
            Redraw();
            useButton.onClick.RemoveAllListeners();
            useButton.onClick.AddListener(Use);
        }

        public void AddItem(InventoryItem item, int count)
        {
            actionStore.AddAction(item as ActionItem, count, index);
        }

        public int GetCount()
        {
            return actionStore.GetCount(index);
        }

        public InventoryItem GetItem()
        {
            return actionStore.GetItem(index);
        }

        public int MaxAcceptable(InventoryItem item)
            => actionStore.MaxAcceptable(item as ActionItem, index);

        public void RemoveItem(int count)
        {
            actionStore.RemoveItem(index, count);
        }

        private void Redraw()
        {
            icon.SetIcon(GetItem(), GetCount());
        }

        private void Use()
        {
            if (GetItem() == null)
                return;
            Debug.Log("Use");
        }

        private void OnDestroy()
        {
            if (actionStore != null)
                actionStore.StoreChanged -= Redraw;
        }
    }
}