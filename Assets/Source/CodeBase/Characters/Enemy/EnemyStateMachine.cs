using EpicRPG.EntityFSM;
using UnityEngine;
using UnityEngine.AI;

namespace EpicRPG.Characters.Enemies
{
    public class EnemyStateMachine : EntityStateMachine<EnemyState>
    {
        [SerializeField] private EnemyAnimator enemyAnimator;
        [SerializeField] private NavMeshAgent agent;
        private Player.Player player;

        public void Construct(Player.Player player)
        {
            this.player = player;
            InitStates(transform, player, enemyAnimator, agent);
            ChangeState(firstState);
        }

        protected void InitStates(Transform transform, Player.Player player, EnemyAnimator enemyAnimator, NavMeshAgent agent)
        {
            foreach (var state in allStates)
                state.Init(transform, player, enemyAnimator, agent);

        }
    }
}