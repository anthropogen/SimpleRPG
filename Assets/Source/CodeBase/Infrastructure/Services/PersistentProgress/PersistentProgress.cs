using System;
using System.Diagnostics;

namespace EpicRPG.Services.PersistentData
{
    [Serializable]
    public class PersistentProgress
    {
        public WorldData WorldData;
        public HeroState HeroState;
        public KillData KillData;
        public PersistentProgress(string sceneName)
        {
            KillData = new KillData();
            WorldData = new WorldData(sceneName);
            HeroState = new HeroState();
            if (KillData == null)
                UnityEngine.Debug.Log("Why null???");
        }
    }
}