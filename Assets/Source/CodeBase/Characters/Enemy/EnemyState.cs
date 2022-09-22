using EpicRPG.EntityFSM;
using EpicRPG.Hero;
using EpicRPG.StaticData;
using UnityEngine;
using UnityEngine.AI;

namespace EpicRPG.Characters.Enemies
{
    public abstract class EnemyState : EntityState
    {
        protected Transform parent;
        protected Player player;
        protected EnemyAnimator animator;
        protected NavMeshAgent agent;
        protected EnemyStaticData staticData;

        public void Init(EnemyStaticData staticData, Transform transform, Player player, EnemyAnimator animator, NavMeshAgent agent)
        {
            this.staticData = staticData;
            this.agent = agent;
            this.player = player;
            this.animator = animator;
            Init(transform);
            foreach (var transition in Transitions)
                transition.Construct<Player>(player);
        }

        public override void Init(Transform transform)
        => parent = transform;
    }
}