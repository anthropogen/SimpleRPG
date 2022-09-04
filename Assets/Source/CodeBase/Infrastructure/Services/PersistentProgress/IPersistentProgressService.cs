
namespace EpicRPG.Services.PersistentData
{
    public interface IPersistentProgressService : IService
    {
        PersistentProgress Progress { get; set; }
    }
}