using System.Collections.Generic;
using UnityEngine;
using SimpleRPG.Infrastructure;
using SimpleRPG.Quests;

namespace SimpleRPG.UI
{
    public class QuestListUI : GameEntity
    {
        [SerializeField] private Transform container;
        [SerializeField] private QuestTootlip questTootlip;
        [SerializeField] private QuestItemUI questItemTemplate;
        private List<QuestItemUI> questItems = new();

        public void UpdateList(IEnumerable<Quest> quests)
        {
            int i = 0;
            foreach (var quest in quests)
            {
                QuestItemUI questItem = null;
                if (i < questItems.Count)
                {
                    questItem = questItems[i];
                    questItem.gameObject.SetActive(true);
                }
                else
                {
                    questItem = Instantiate(questItemTemplate, container);
                    questItems.Add(questItem);
                }
                questItem.Construct(quest);
                i++;
            }
        }

        private void OnDisable()
        {
            foreach (var item in questItems)
                item.gameObject.SetActive(false);
        }
    }
}