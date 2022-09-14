using EpicRPG.Services.PersistentData;

namespace EpicRPG.Characters
{
    public class PlayerHealth : Health, ISavable
    {
        public void LoadProgress(PersistentProgress progress)
        {
            Construct(progress.HeroState.CurrentHP, progress.HeroState.MaxHP);
        }

        public void SaveProgress(PersistentProgress progress)
        {
            progress.HeroState.MaxHP = max;
            progress.HeroState.CurrentHP = Current;
        }
    }
}