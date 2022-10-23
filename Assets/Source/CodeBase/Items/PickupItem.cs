using EpicRPG.Characters;
using EpicRPG.Levels;
using EpicRPG.Services.PersistentData;
using UnityEngine;

namespace EpicRPG.Items
{
    [RequireComponent(typeof(BoxCollider))]
    public class PickupItem : GameEntity
    {
        public InventoryItem Item { get; set; }
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
            if (other.TryGetComponent(out CharacterAttacker attacker))
            {
                if (Item is WeaponItem)
                    attacker.EquipWeapon(Item as WeaponItem);
            }
            progress.WorldData.LootData.Pickup(this);
            isPicked = true;
            gameObject.SetActive(false);
        }
    }
}