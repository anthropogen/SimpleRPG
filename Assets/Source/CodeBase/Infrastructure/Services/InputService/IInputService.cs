using UnityEngine;

namespace EpicRPG.Services
{
    public interface IInputService : IService
    {
        Vector2 Axis { get; }
        bool IsAttackButtonUp();

        bool IsForceAttackButtonUp();
    }
}