using UnityEngine;

namespace EpicRPG.Player
{
    public class PlayerInitPoint : GameEntity
    {
        public Vector3 Point => transform.position;
    }
}