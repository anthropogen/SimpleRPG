using EpicRPG.EntityFSM;

namespace EpicRPG.Characters.Enemies
{
    public abstract class EnemyTransition : Transition<IEntityState>
    {
        protected Player.Player player;
        public override void Construct<TArgument>(TArgument transitionArguments)
        {
            if (transitionArguments is Player.Player)
                player = transitionArguments as Player.Player;
        }
    }
}