using SimpleRPG.Services.PersistentData;

namespace SimpleRPG.Services.SaveLoad
{
    public interface ISaveLoadService : IService
    {
        PersistentProgress Load();
        void Save();
    }
}