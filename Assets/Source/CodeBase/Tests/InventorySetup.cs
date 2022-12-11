using SimpleRPG.Infrastructure;
using SimpleRPG.Items;
using SimpleRPG.UI;
using System.Collections.Generic;
using UnityEngine;

public class InventorySetup : GameEntity
{
    [SerializeField] private List<InventoryItem> items;
    [SerializeField] private Inventory inventory;
    [SerializeField] private InventoryView inventoryView;

    private void Start()
    {
        foreach (var item in items)
            inventory.AddToFirstEmptySlot(item, 1);

        inventoryView.Construct(inventory);
    }
}

