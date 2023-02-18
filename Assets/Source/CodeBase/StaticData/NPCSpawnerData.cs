using System;
using UnityEngine;

namespace SimpleRPG.StaticData
{
    [Serializable]
    public class NPCSpawnerData
    {
        public Vector3 Position;
        public Quaternion Rotation;
        public string ID;
        public string SaveID;

        public NPCSpawnerData(Vector3 position, Quaternion rotation, string iD, string saveID)
        {
            Position = position;
            Rotation = rotation;
            ID = iD;
            SaveID = saveID;
        }
    }
}