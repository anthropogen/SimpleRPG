using UnityEngine.EventSystems;

namespace SimpleRPG.UI
{
    public class QuestTootlipSpawner : TootlipSpawner
    {
        private QuestItemUI questItem;
        private QuestTootlip questTootlip;

        public void Construct(QuestTootlip questTootlip)
        {
            this.questTootlip = questTootlip;
            TootlipTransform = questTootlip.transform;
        }

        protected override void Enable()
            => questItem = GetComponent<QuestItemUI>();

        public override void OnPointerEnter(PointerEventData eventData)
        {
            if (questTootlip == null) return;
            UpdateTootlipPosition();
            questTootlip.gameObject.SetActive(true);
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            if (questTootlip == null) return;
            questTootlip.gameObject.SetActive(false);
        }
    }
}
