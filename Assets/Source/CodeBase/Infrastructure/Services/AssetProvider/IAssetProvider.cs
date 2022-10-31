using UnityEngine;

namespace SimpleRPG.Services.AssetManagement
{
    public interface IAssetProvider : IService
    {
        GameObject Instantiate(string path);
        GameObject InstantiateAt(string path, Vector3 position);
    }
}