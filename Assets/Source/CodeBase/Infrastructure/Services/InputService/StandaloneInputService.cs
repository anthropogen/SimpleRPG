using UnityEngine;

namespace SimpleRPG.Services
{
    public class StandaloneInputService : InputService
    {
        public override Vector2 Axis
        {
            get
            {
                var axis = GetSimpleInputAxis();
                if (axis == Vector2.zero)
                    axis = new Vector2(Input.GetAxis(Horizontal), Input.GetAxis(Vertical));
                return axis;
            }
        }
    }
}