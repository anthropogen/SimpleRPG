using EpicRPG.Hero;
using UnityEngine;

namespace EpicRPG.Items
{
    [RequireComponent(typeof(BoxCollider))]
    public class PickupItem : GameEntity
    {
        [field: SerializeField] public InventoryItem Item { get; private set; }
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
                Debug.Log("Pick up");
            }
        }
    }
}