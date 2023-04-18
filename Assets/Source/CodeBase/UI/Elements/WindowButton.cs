using SimpleRPG.Infrastructure;
using SimpleRPG.Services.WindowsService;
using UnityEngine;
using UnityEngine.UI;

namespace SimpleRPG.UI
{
    public class WindowButton : GameEntity
    {
        [SerializeField] private Button openButton;
        [SerializeField] private WindowsID iD;
        private IWindowsService windowsService;
        public void Construct(IWindowsService windowsService)
        {
            this.windowsService = windowsService;
        }

        private void Awake()
        {
            openButton.onClick.AddListener(() => OpenWindow());
        }

        private async void OpenWindow()
        {
            var window = await windowsService.OpenWindow(iD);
            window.Open();
        }
    }
}
