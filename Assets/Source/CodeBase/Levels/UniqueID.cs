using UnityEngine;

namespace EpicRPG.Levels
{
    public class UniqueID : GameEntity
    {
        [field: SerializeField] public string SaveID { get; private set; }
    }
}