using UnityEngine;

namespace EpicRPG.Services.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        public GameObject Instantiate(string path)
        {
            var prefab = Resources.Load<GameObject>(path);
            return GameObject.Instantiate<GameObject>(prefab);
        }

        public GameObject InstantiateAt(string path, Vector3 position)
        {
            var prefab = Resources.Load<GameObject>(path);
            return GameObject.Instantiate<GameObject>(prefab, position, Quaternion.identity);
        }
    }
}
