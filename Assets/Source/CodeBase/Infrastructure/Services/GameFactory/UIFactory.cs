using SimpleRPG.Dialogue;
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
        private readonly LazyInitializy<IGameFactory> lazyGameFactory;
        private Transform uiRoot;
        public UIFactory(IAssetProvider assets, LazyInitializy<IGameFactory> lazyGameFactory)
        {
            this.assets = assets;
            this.lazyGameFactory = lazyGameFactory;
        }

        public UIFactory(IAssetProvider assets, IGameFactory gameFactory)
        {
            this.assets = assets;
            lazyGameFactory = new LazyInitializy<IGameFactory>();
            lazyGameFactory.Value = gameFactory;
        }

        public async void WarmUp()
        {
            await assets.Load<GameObject>(AssetsAddress.HUD);
            await assets.Load<GameObject>(AssetsAddress.Inventory);
            await assets.Load<GameObject>(AssetsAddress.Dialogue);
        }

        public void CreateUIRoot()
            => uiRoot = new GameObject("UIROOT").transform;

        public async Task<GameObject> CreateInventoryWindow()
        {
            var prefab = await assets.Load<GameObject>(AssetsAddress.Inventory);
            var window = GameObject.Instantiate(prefab,uiRoot);
            window.GetComponentInChildren<InventoryView>().Construct(lazyGameFactory.Value.LazyPlayer.Value.GetComponentInChildren<Inventory>());
            window.GetComponentInChildren<InventoryDropper>().Construct(lazyGameFactory.Value);
            var equipment = lazyGameFactory.Value.LazyPlayer.Value.GetComponentInChildren<Equipment>();
            var actionStore = lazyGameFactory.Value.LazyPlayer.Value.GetComponentInChildren<ActionStore>();

            foreach (var slot in window.GetComponentsInChildren<EquipmentSlotView>())
                slot.Construct(equipment);

            foreach (var action in window.GetComponentsInChildren<ActionItemSlotView>())
                action.Construct(actionStore);

            return window;
        }

        public async Task<GameObject> CreateDialogueWindow()
        {
            var prefab = await assets.Load<GameObject>(AssetsAddress.Dialogue);
            var window = GameObject.Instantiate(prefab,uiRoot);

            var conversant = lazyGameFactory.Value.LazyPlayer.Value.GetComponent<PlayerConversant>();
            window.GetComponent<DialogueWindow>().Construct(conversant);

            return window;
        }
    }
}