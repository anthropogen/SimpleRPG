using UnityEngine;

namespace EpicRPG.Services.AssetManagement
{
    public interface IAssetProvider : IService
    {
        GameObject Instantiate(string path);
        GameObject InstantiateAt(string path, Vector3 position);
    }
}