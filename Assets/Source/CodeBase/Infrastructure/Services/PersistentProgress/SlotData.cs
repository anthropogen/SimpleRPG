using SimpleRPG.Items;
using System;

namespace SimpleRPG.Services.PersistentData
{
    [Serializable]
    public class SlotData
    {
        public string Name;
        public int Count;

        public SlotData(string name, int count)
        {
            Name = name;
            Count = count;
        }
    }
}