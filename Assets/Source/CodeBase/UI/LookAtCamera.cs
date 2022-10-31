using SimpleRPG.Infrastructure;
using UnityEngine;

namespace SimpleRPG.UI
{
    public class LookAtCamera : GameEntity
    {
        private Camera mainCamera;

        private void Awake()
        {
            mainCamera = Camera.main;
        }

        protected override void LateLoop()
        {
            Quaternion rotation = mainCamera.transform.rotation;
            transform.LookAt(transform.position + rotation * Vector3.back, rotation * Vector3.up);
        }
    }
}
