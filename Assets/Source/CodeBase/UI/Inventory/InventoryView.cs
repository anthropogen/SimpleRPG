using SimpleRPG.Infrastructure;
using SimpleRPG.Items;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleRPG.UI
{
    public class InventoryView : GameEntity
    {
        [SerializeField] private InventorySlotView slotTemplate;
        [SerializeField] private ItemTootlip itemTootlip;
        [SerializeField] private Transform slotsContainer;
        [SerializeField] private Transform dragContainer;
        private Inventory inventory;
        private List<InventorySlotView> slotViews = new List<InventorySlotView>();

        public void Construct(Inventory inventory)
        {
            if (this.inventory != null)
                this.inventory.InventoryUpdated -= Redraw;
            this.inventory = inventory;
            this.inventory.InventoryUpdated += Redraw;

            for (int i = 0; i < inventory.Capacity; i++)
                CreteSlotView(inventory, i);
        }

        private void Redraw()
        {
            for (int i = 0; i < inventory.Capacity; i++)
            {
                if (i >= slotViews.Count)
                {
                    CreteSlotView(inventory, i);
                    continue;
                }
                slotViews[i].Construct(inventory, i, dragContainer);
            }
        }

        private void CreteSlotView(Inventory inventory, int i)
        {
            var slotView = Instantiate(slotTemplate, slotsContainer);
            slotView.Construct(inventory, i, dragContainer);
            slotView.GetComponent<ItemTootlipSpawner>().Construct(itemTootlip);
            slotViews.Add(slotView);
        }
    }
}