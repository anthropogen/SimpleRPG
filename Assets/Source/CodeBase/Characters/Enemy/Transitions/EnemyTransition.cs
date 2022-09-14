using EpicRPG.EntityFSM;
using UnityEngine;

namespace EpicRPG.Characters.Enemies
{
    public abstract class EnemyTransition : Transition<EntityState>
    {
        protected Hero.Player player;
        public override void Construct<TArgument>(TArgument transitionArguments)
        {
            if (transitionArguments is Hero.Player)
                player = transitionArguments as Hero.Player;
        }
        protected float SqrDistanceToPLayer()
           => Vector3.SqrMagnitude(player.transform.position - transform.position);
    }
}