using UnityEngine;
using SimpleRPG.Infrastructure;
using SimpleRPG.Quests;
using TMPro;

namespace SimpleRPG.UI
{
    public class QuestItemUI : GameEntity
    {
        [SerializeField] private TMP_Text title;
        [SerializeField] private TMP_Text progress;

        public void Construct(Quest quest)
        {
            title.text = quest.Title;
            progress.text = $"{0}/{0}";
        }
    }
}