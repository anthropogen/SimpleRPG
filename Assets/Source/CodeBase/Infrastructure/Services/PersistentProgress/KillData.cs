using System;
using System.Collections.Generic;

namespace EpicRPG.Services.PersistentData
{
    [Serializable]
    public class KillData
    {
        public List<string> ClearedSpawners;

        public KillData()
        {
            ClearedSpawners = new List<string>();
        }
    }
}