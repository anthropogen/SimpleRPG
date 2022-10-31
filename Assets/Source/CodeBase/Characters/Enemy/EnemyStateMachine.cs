using SimpleRPG.EntityFSM;
using SimpleRPG.Hero;
using SimpleRPG.StaticData;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace SimpleRPG.Characters.Enemies
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyStateMachine : EntityStateMachine<EnemyState>
    {
        [SerializeField] private EnemyAnimator enemyAnimator;
        [SerializeField] private NavMeshAgent agent;
        private Player player;

        public void Construct(Player player, EnemyStaticData staticData)
        {
            this.player = player;
            InitStates(staticData, transform, player, enemyAnimator, agent);
            ChangeState(firstState);
        }

        protected void InitStates(EnemyStaticData staticData, Transform transform, Player player, EnemyAnimator enemyAnimator, NavMeshAgent agent)
        {
            foreach (var state in allStates)
                state.Init(staticData, transform, player, enemyAnimator, agent);
        }

        public void Death()
        {
            var deathState = allStates.FirstOrDefault(s => s.GetType() == typeof(EnemyDeathState));
            ChangeState(deathState);
        }
    }
}