using System.Collections.Generic;
using SimpleRPG.Quests;
using UnityEngine;

namespace SimpleRPG.UI
{
    public class QuestWindow : BaseWindow
    {
        [SerializeField] private QuestListUI questList;
        [SerializeField] private List<Quest> templates;
        public override void Open()
        {
            base.Open();
            questList.UpdateList(templates);
        }
    }
}