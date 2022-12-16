using UnityEngine;

namespace SimpleRPG.UI
{
    public class InventoryWindow : BaseWindow
    {
        [SerializeField] private GameObject inventoryContainer;

        public override void Close()
            => inventoryContainer.SetActive(false);

        public override void Open()
            => inventoryContainer.SetActive(true);
    }
}