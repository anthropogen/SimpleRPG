using UnityEngine;

namespace CodeBase.UI
{
    public class LookAtCamera : GameEntity
    {
        private Camera mainCamera;

        private void Start()
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
