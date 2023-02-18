using SimpleRPG.Infrastructure;
using SimpleRPG.Services.GameFactory;
using SimpleRPG.Services.PersistentData;

namespace SimpleRPG.Levels
{
    public class NPCSpawner : GameEntity, ISavable
    {
        private string saveID;
        private string id;
        private IGameFactory gameFactory;

        public void Construct(IGameFactory factory, string id, string saveID)
        {
            this.id = id;
            this.saveID = saveID;
            gameFactory = factory;
        }

        public async void LoadProgress(PersistentProgress progress)
        {
            var npc = await gameFactory.CreateNPC(id, transform);
          
        }

        public void SaveProgress(PersistentProgress progress)
        {
        }
    }
}