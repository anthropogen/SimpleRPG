using SimpleRPG.Infrastructure;
using UnityEngine;

namespace SimpleRPG.Hero
{
    public class PlayerInitPoint : GameEntity
    {
        public Vector3 Point => transform.position;
    }
}