using SimpleRPG.Infrastructure;
using UnityEngine;

namespace SimpleRPG.Levels
{
    public class UniqueID : GameEntity
    {
        [field: SerializeField] public string SaveID { get; private set; }
    }
}