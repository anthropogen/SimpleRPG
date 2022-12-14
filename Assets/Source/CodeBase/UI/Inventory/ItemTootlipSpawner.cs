using SimpleRPG.Infrastructure;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemTootlipSpawner : GameEntity, IPointerEnterHandler, IPointerExitHandler
{
    private IItemHolder itemHolder;
    private ItemTootlip itemTootlip;

    public void Construct(ItemTootlip itemTootlip)
        => this.itemTootlip = itemTootlip;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (itemTootlip == null) return;
        var item = itemHolder.GetItem();
        if (item == null) return;

        itemTootlip.UpdateTootlip(item.Name, item.Description);
        UpdateTootlipPosition();
        itemTootlip.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
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

    private void UpdateTootlipPosition()
    {
        var tooltipCorners = new Vector3[4];
        itemTootlip.GetComponent<RectTransform>().GetWorldCorners(tooltipCorners);
        var slotCorners = new Vector3[4];
        GetComponent<RectTransform>().GetWorldCorners(slotCorners);

        bool below = transform.position.y > Screen.height / 2;
        bool right = transform.position.x < Screen.width / 2;

        int slotCorner = GetCornerIndex(below, right);
        int tooltipCorner = GetCornerIndex(!below, !right);

        itemTootlip.transform.position = slotCorners[slotCorner] - tooltipCorners[tooltipCorner] + itemTootlip.transform.position;
    }

    private int GetCornerIndex(bool below, bool right)
    {
        if (below && !right) return 0;
        else if (!below && !right) return 1;
        else if (!below && right) return 2;
        else return 3;
    }
}

