using SimpleRPG.Services.AssetManagement;
using System.Threading.Tasks;
using UnityEngine;

namespace SimpleRPG.Services.GameFactory
{
    public class UIFactory : IUIFactory
    {
        private readonly IGameFactory gameFactory;
        private readonly IAssetProvider assets;

        public UIFactory(IGameFactory gameFactory, IAssetProvider assets)
        {
            this.gameFactory = gameFactory;
            this.assets = assets;
        }
        public async void WarmUp()
        {
            await assets.Load<GameObject>(AssetsAddress.HUD);
            await assets.Load<GameObject>(AssetsAddress.Inventory);
        }

        public async Task<GameObject> CreateHUD()
            => await InstantiateRegisteredObject(AssetsAddress.HUD);

        public async Task<GameObject> CreateInventoryWindow()
            => await InstantiateRegisteredObject(AssetsAddress.Inventory);

        private async Task<GameObject> InstantiateRegisteredObject(string address)
        {
            var prefab = await assets.Load<GameObject>(address);
            return gameFactory.InstantiateRegisteredObject(prefab);
        }
    }
}