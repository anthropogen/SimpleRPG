using EpicRPG.EntityFSM;
using EpicRPG.Hero;
using UnityEngine;
using UnityEngine.AI;

namespace EpicRPG.Characters.Enemies
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyStateMachine : EntityStateMachine<EnemyState>
    {
        [SerializeField] private EnemyAnimator enemyAnimator;
        [SerializeField] private NavMeshAgent agent;
        private Player player;

        public void Construct(Hero.Player player)
        {
            this.player = player;
            InitStates(transform, player, enemyAnimator, agent);
            ChangeState(firstState);
        }

        protected void InitStates(Transform transform, Hero.Player player, EnemyAnimator enemyAnimator, NavMeshAgent agent)
        {
            foreach (var state in allStates)
                state.Init(transform, player, enemyAnimator, agent);

        }
    }
}