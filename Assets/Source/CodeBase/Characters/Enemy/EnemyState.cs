using EpicRPG.EntityFSM;
using UnityEngine;
using UnityEngine.AI;

namespace EpicRPG.Characters.Enemies
{
    public abstract class EnemyState : EntityState
    {
        private Transform parent;
        private Player.Player player;
        private EnemyAnimator animator;
        private NavMeshAgent agent;

        public void Init(Transform transform, Player.Player player, EnemyAnimator animator, NavMeshAgent agent)
        {
            this.agent = agent;
            this.player = player;
            this.animator = animator;
            Init(transform);
            foreach (var transition in Transitions)
                transition.Construct<Player.Player>(player);
        }

        public override void Init(Transform transform)
        => parent = transform;
    }
}