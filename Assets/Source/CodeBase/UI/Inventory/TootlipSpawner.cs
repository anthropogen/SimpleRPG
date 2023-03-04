using SimpleRPG.Infrastructure;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SimpleRPG.UI
{
    public abstract class TootlipSpawner : GameEntity, IPointerEnterHandler, IPointerExitHandler
    {
        protected Transform TootlipTransform;
        public abstract void OnPointerEnter(PointerEventData eventData);

        public abstract void OnPointerExit(PointerEventData eventData);


        protected int GetCornerIndex(bool below, bool right)
        {
            if (below && !right) return 0;
            else if (!below && !right) return 1;
            else if (!below && right) return 2;
            else return 3;
        }

        protected void UpdateTootlipPosition()
        {
            var tooltipCorners = new Vector3[4];
            TootlipTransform.GetComponent<RectTransform>().GetWorldCorners(tooltipCorners);
            var slotCorners = new Vector3[4];
            GetComponent<RectTransform>().GetWorldCorners(slotCorners);

            bool below = transform.position.y > Screen.height / 2;
            bool right = transform.position.x < Screen.width / 2;

            int slotCorner = GetCornerIndex(below, right);
            int tooltipCorner = GetCornerIndex(!below, !right);

            TootlipTransform.position += slotCorners[slotCorner] - tooltipCorners[tooltipCorner];
        }
    }
}