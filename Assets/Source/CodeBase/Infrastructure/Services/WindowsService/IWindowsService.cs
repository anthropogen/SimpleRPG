using SimpleRPG.UI;
using System.Threading.Tasks;

namespace SimpleRPG.Services.WindowsService
{
    public interface IWindowsService : IService
    {
        Task<BaseWindow> OpenWindow(WindowsID windowsID);
        void Clear();
    }
}