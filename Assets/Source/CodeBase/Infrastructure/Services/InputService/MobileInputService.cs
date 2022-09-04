using UnityEngine;

namespace EpicRPG.Services
{
    public class MobileInputService : InputService
    {
        public override Vector2 Axis
           => GetSimpleInputAxis();
    }
}