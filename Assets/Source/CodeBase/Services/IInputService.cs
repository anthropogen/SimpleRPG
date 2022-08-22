using UnityEngine;

namespace EpicRPG.Services
{
    public interface IInputService
    {
        Vector2 Axis { get; }
        bool IsAttackButtonUp();
    }
}