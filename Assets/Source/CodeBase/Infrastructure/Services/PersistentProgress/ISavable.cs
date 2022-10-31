
namespace SimpleRPG.Services.PersistentData
{
    public interface ISavable : IProgressReader
    {
        void SaveProgress(PersistentProgress progress);
    }
}
