using UnityEngine;

namespace SimpleRPG.Services
{
    public interface IInputService : IService
    {
        Vector2 Axis { get; }
        bool IsAttackButtonUp();

        bool IsForceAttackButtonUp();
    }
}