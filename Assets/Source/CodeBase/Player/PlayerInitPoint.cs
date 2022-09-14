using UnityEngine;

namespace EpicRPG.Hero
{
    public class PlayerInitPoint : GameEntity
    {
        public Vector3 Point => transform.position;
    }
}