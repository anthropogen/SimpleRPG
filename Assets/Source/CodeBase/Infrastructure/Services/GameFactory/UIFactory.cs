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

        public async Task<GameObject> CreateHUD()
        {
            var prefab = await assets.Load<GameObject>(AssetsAddress.HUD);
            return gameFactory.InstantiateRegisteredObject(prefab);
        }
    }
}