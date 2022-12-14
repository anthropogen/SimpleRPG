using SimpleRPG.Characters;
using SimpleRPG.Hero;
using SimpleRPG.Infrastructure;
using SimpleRPG.Services.PersistentData;
using System;
using UnityEngine;

namespace SimpleRPG.Items
{
    [RequireComponent(typeof(BoxCollider))]
    public class PickupItem : GameEntity
    {
        public InventoryItem Item { get; set; }
        public int Count { get; set; }
        public string SaveID { get; set; }
        private PersistentProgress progress;
        private bool isPicked;

        public void Construct(PersistentProgress progress)
        {
            this.progress = progress;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (isPicked) return;
            if (other.TryGetComponent(out Player player))
            {
                player.GetComponentInChildren<Inventory>().AddToFirstEmptyOrStackSlot(Item, Count);
            }
            progress.WorldData.LootData.Pickup(this);
            isPicked = true;
            gameObject.SetActive(false);
        }
    }
}