using Cinemachine;
using UnityEngine;

namespace EpicRPG.Hero
{
    public class FollowingCamera : GameEntity
    {
        [SerializeField] private CinemachineVirtualCamera virtualCamera;

        public void SetTarget(Transform target)
        {
            virtualCamera.Follow = target;
        }
    }
}