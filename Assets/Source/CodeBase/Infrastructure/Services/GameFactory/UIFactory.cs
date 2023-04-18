using SimpleRPG.Dialogue;
using SimpleRPG.Items;
using SimpleRPG.Services.AssetManagement;
using SimpleRPG.UI;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace SimpleRPG.Services.GameFactory
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssetProvider assets;
        private readonly Lazy<IGameFactory> lazyGameFactory;
        private Transform uiRoot;
        public UIFactory(IAssetProvider assets, Lazy<IGameFactory> lazyGameFactory)
        {
            this.assets = assets;
            this.lazyGameFactory = lazyGameFactory;
        }

        public UIFactory(IAssetProvider assets, IGameFactory gameFactory)
        {
            this.assets = assets;
            lazyGameFactory = new Lazy<IGameFactory>(gameFactory);
        }

        public async void WarmUp()
        {
            await assets.Load<GameObject>(AssetsAddress.HUD);
            await assets.Load<GameObject>(AssetsAddress.InventoryWindow);
            await assets.Load<GameObject>(AssetsAddress.DialogueWindow);
        }

        public void CreateUIRoot()
            => uiRoot = new GameObject("UIROOT").transform;

        public async Task<GameObject> CreateInventoryWindow()
        {
            var prefab = await assets.Load<GameObject>(AssetsAddress.InventoryWindow);
            var window = GameObject.Instantiate(prefab, uiRoot);
            window.GetComponentInChildren<InventoryView>().Construct(lazyGameFactory.Value.Player.GetComponentInChildren<Inventory>());
            window.GetComponentInChildren<InventoryDropper>().Construct(lazyGameFactory.Value);
            var equipment = lazyGameFactory.Value.Player.GetComponentInChildren<Equipment>();
            var actionStore = lazyGameFactory.Value.Player.GetComponentInChildren<ActionStore>();

            foreach (var slot in window.GetComponentsInChildren<EquipmentSlotView>())
                slot.Construct(equipment);

            foreach (var action in window.GetComponentsInChildren<ActionItemSlotView>())
                action.Construct(actionStore);

            return window;
        }

        public async Task<GameObject> CreateDialogueWindow()
        {
            var prefab = await assets.Load<GameObject>(AssetsAddress.DialogueWindow);
            var window = GameObject.Instantiate(prefab, uiRoot);

            var conversant = lazyGameFactory.Value.Player.GetComponent<PlayerConversant>();
            window.GetComponent<DialogueWindow>().Construct(conversant);

            return window;
        }

        public async Task<GameObject> CreateQuestWindow()
        {
            var prefab = await assets.Load<GameObject>(AssetsAddress.QuestWindow);
            var window = GameObject.Instantiate(prefab, uiRoot);
            return window;
        }
    }
}