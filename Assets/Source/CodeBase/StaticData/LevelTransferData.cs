using System;
using UnityEngine;

namespace SimpleRPG.StaticData
{
    [Serializable]
    public class LevelTransferData
    {
        public Vector3 Position;
        public string NextLevel;

        public LevelTransferData(Vector3 position, string nextLevel)
        {
            Position = position;
            NextLevel = nextLevel;
        }
    }
}