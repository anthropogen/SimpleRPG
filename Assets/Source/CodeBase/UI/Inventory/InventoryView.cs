using SimpleRPG.Infrastructure;
using SimpleRPG.Items;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleRPG.UI
{
    public class InventoryView : GameEntity
    {
        [SerializeField] private InventorySlotView slotTemplate;
        [SerializeField] private Transform container;
        private Inventory inventory;
        private List<InventorySlotView> slotViews = new List<InventorySlotView>();

        public void Construct(Inventory inventory)
        {
            if (this.inventory != null)
                this.inventory.InventoryUpdated -= Redraw;

            this.inventory = inventory;
            this.inventory.InventoryUpdated += Redraw;

            for (int i = 0; i < inventory.Capacity; i++)
            {
                CreteSlotView(inventory, i);
            }
        }

        private void Redraw()
        {
            for (int i = 0; i < inventory.Capacity; i = 0)
            {
                if (i >= slotViews.Count)
                {
                    CreteSlotView(inventory, i);
                    continue;
                }
                slotViews[i].Construct(inventory, i);
            }
        }

        private void CreteSlotView(Inventory inventory, int i)
        {
            var slotView = Instantiate(slotTemplate, container);
            slotView.Construct(inventory, i);
            slotViews.Add(slotView);
        }
    }
}