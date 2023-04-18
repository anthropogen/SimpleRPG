using System.Threading.Tasks;
using UnityEngine;

namespace SimpleRPG.Services.GameFactory
{
    public interface IUIFactory : IService
    {
        void WarmUp();
        Task<GameObject> CreateInventoryWindow();
        void CreateUIRoot();
        Task<GameObject> CreateDialogueWindow();
        Task<GameObject> CreateQuestWindow();
    }
}