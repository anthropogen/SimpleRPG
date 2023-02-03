using SimpleRPG.Infrastructure;
using UnityEngine;

namespace SimpleRPG.Hero
{
    public class PlayerInitPoint : SpawnMarker
    {
        public Vector3 Point => transform.position;

        public override Color DrawColor => Color.yellow;
    }
}