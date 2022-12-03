using System.Threading.Tasks;
using UnityEngine;

namespace SimpleRPG.Services.GameFactory
{
    public interface IUIFactory : IService
    {
        Task<GameObject> CreateHUD();
    }
}