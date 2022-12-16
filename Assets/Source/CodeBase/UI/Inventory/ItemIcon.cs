using SimpleRPG.Infrastructure;
using SimpleRPG.Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SimpleRPG.UI
{
    public class ItemIcon : GameEntity
    {
        [SerializeField] private Image icon;
        [SerializeField] private TMP_Text itemCountText;

        public void SetIcon(InventoryItem item, int count)
        {
            if (item == null)
            {
                icon.enabled = false;
                itemCountText.enabled = false;
                return;
            }
            if (count <= 1)
            {
                itemCountText.enabled = false;
            }
            else
            {
                itemCountText.text = count.ToString();
                itemCountText.enabled = true;
            }
            icon.enabled = true;
            icon.sprite = item.Icon;
        }

    }
}