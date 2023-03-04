using UnityEngine.EventSystems;

namespace SimpleRPG.UI
{
    public class ItemTootlipSpawner : TootlipSpawner
    {
        private IItemHolder itemHolder;
        private ItemTootlip itemTootlip;

        public void Construct(ItemTootlip itemTootlip)
        {
            this.itemTootlip = itemTootlip;
            TootlipTransform = itemTootlip.transform;
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            if (itemTootlip == null) return;
            var item = itemHolder.GetItem();
            if (item == null) return;

            itemTootlip.UpdateTootlip(item.Name, item.Description);
            UpdateTootlipPosition();
            itemTootlip.gameObject.SetActive(true);
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            if (itemTootlip == null || itemHolder.GetItem() == null) return;

            itemTootlip.gameObject.SetActive(false);
        }

        protected override void Enable()
        {
            itemHolder = GetComponent<IItemHolder>();
            if (itemHolder == null)
                itemHolder = GetComponentInParent<IItemHolder>();
        }
    }
}