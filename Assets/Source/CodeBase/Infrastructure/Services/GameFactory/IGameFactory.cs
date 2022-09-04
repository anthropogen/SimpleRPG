using EpicRPG.Services.PersistentData;
using System.Collections.Generic;
using UnityEngine;

namespace EpicRPG.Services.GameFactory
{
    public interface IGameFactory : IService
    {
        List<ISavable> Savables { get; }
        List<IProgressReader> ProgressReaders { get; }

        void CleanUp();
        GameObject CreateHero();
        GameObject CreateHUD();
    }
}