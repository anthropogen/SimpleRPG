using UnityEngine;

namespace SimpleRPG.Services
{
    public class MobileInputService : InputService
    {
        public override Vector2 Axis
           => GetSimpleInputAxis();
    }
}