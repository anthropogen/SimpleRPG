using Cinemachine;
using SimpleRPG.Infrastructure;
using UnityEngine;

namespace SimpleRPG.Hero
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