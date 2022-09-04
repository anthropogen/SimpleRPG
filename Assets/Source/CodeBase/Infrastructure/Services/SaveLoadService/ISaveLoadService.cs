using EpicRPG.Services.PersistentData;

namespace EpicRPG.Services.SaveLoad
{
    public interface ISaveLoadService : IService
    {
        PersistentProgress Load();
        void Save();
    }
}