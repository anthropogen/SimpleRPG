using EpicRPG.Characters;
using UnityEngine;

namespace EpicRPG.Items
{
    [RequireComponent(typeof(BoxCollider))]
    public class PickupItem : GameEntity
    {
        [field: SerializeField] public InventoryItem Item { get; private set; }


        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out CharacterAttacker attacker))
            {
                attacker.EquipWeapon(Item as WeaponItem);
            }
            gameObject.SetActive(false);
        }
    }
}