using EpicRPG.Characters.Enemies;
using EpicRPG.Characters.Enemy;
using EpicRPG.Hero;
using EpicRPG.Services.PersistentData;
using System.Collections.Generic;
using UnityEngine;

namespace EpicRPG.Services.GameFactory
{
    public interface IGameFactory : IService
    {
        List<ISavable> Savables { get; }
        List<IProgressReader> ProgressReaders { get; }
        GameObject HeroGameObject { get; }
        LazyInitializy<Player> LazyPlayer { get; }
        void CleanUp();
        Enemy CreateEnemy(EnemyTypeID enemyTypeID, Transform transform);
        GameObject CreateHero();
        GameObject CreateHUD();
        void Register(IProgressReader reader);
    }
}