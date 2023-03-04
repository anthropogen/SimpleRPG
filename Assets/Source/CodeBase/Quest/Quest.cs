using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleRPG.Quests
{
    [CreateAssetMenu(fileName = "newQuest", menuName = "Quest/Create New Quest", order = 51)]
    public class Quest : ScriptableObject
    {
        [field: SerializeField] public string Title { get; private set; }
    }
}