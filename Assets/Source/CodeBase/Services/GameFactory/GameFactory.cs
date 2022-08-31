using EpicRPG.Player;
using UnityEngine;
using EpicRPG.Services.AssetManagement;

namespace EpicRPG.Services.GameFactory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider assets;

        public GameFactory(IAssetProvider assets)
        {
            this.assets = assets;
        }

        public GameObject CreateHero()
            => assets.InstantiateAt(AssetsPath.Player, GameObject.FindObjectOfType<PlayerInitPoint>().Point);

        public GameObject CreateHUD()
            => assets.Instantiate(AssetsPath.HUD);
    }
}
