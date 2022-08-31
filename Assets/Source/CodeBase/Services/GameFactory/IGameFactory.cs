using UnityEngine;

namespace EpicRPG.Services.GameFactory
{
    public interface IGameFactory : IService
    {
        GameObject CreateHero();
        GameObject CreateHUD();
    }
}