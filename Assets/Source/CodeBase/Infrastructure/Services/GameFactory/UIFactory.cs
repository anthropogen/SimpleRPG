using SimpleRPG.Items;
using SimpleRPG.Services.AssetManagement;
using SimpleRPG.UI;
using System.Threading.Tasks;
using UnityEngine;

namespace SimpleRPG.Services.GameFactory
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssetProvider assets;
        private Transform uiRoot;
        public UIFactory(IAssetProvider assets)
        {
            this.assets = assets;
        }

        public async void WarmUp()
        {
            await assets.Load<GameObject>(AssetsAddress.HUD);
            await assets.Load<GameObject>(AssetsAddress.Inventory);
        }

        public void CreateUIRoot()
            => uiRoot = new GameObject("UIROOT").transform;

        public async Task<GameObject> CreateInventoryWindow()
        {
            var prefab = await assets.Load<GameObject>(AssetsAddress.Inventory);
            var window = GameObject.Instantiate(prefab);
            window.GetComponentInChildren<InventoryView>().Construct(GameFactory.Player.GetComponentInChildren<Inventory>());
            window.transform.SetParent(uiRoot);
            return window;
        }
    }
}