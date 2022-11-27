using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SimpleRPG.Services.AssetManagement
{
    public interface IAssetProvider : IService
    {
        void Cleanup();
        void Initialize();
        Task<T> Load<T>(AssetReference reference) where T : class;
        Task<T> Load<T>(string addres) where T : class;
    }
}