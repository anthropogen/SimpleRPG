using SimpleRPG.Services.GameFactory;
using SimpleRPG.UI;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace SimpleRPG.Services.WindowsService
{
    class WindowsService : IWindowsService
    {
        private readonly IUIFactory uIFactory;
        private readonly Dictionary<WindowsID, BaseWindow> windows;
        public WindowsService(IUIFactory uIFactory)
        {
            this.uIFactory = uIFactory;
            windows = new Dictionary<WindowsID, BaseWindow>();
        }

        public void Clear()
            => windows.Clear();

        public async Task<BaseWindow> OpenWindow(WindowsID windowsID)
        {
            BaseWindow result = null;
            if (windows.TryGetValue(windowsID, out result) && result != null)
                return result;

            var window = await CreateWindow(windowsID);
            result = window.GetComponent<BaseWindow>();
            windows.Add(windowsID, result);
            return result;
        }

        private async Task<GameObject> CreateWindow(WindowsID windowsID)
        {
            switch (windowsID)
            {
                case WindowsID.Inventory:
                    return await uIFactory.CreateInventoryWindow();
                case WindowsID.Dialogue:
                    return await uIFactory.CreateDialogueWindow();
                default:
                    throw new InvalidOperationException($"I can't create this {windowsID} window");
            }
        }
    }
}
