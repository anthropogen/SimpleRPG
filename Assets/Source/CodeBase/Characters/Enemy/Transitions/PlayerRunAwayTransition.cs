using UnityEngine;

namespace SimpleRPG.Characters.Enemies
{
    public class PlayerRunAwayTransition : EnemyTransition
    {
        [SerializeField, Range(0, 20)] private float attackDistance;
        private float sqrAttackDistance;
        private void OnEnable()
            => sqrAttackDistance = attackDistance * attackDistance;
        public override bool NeedTransit()
            => SqrDistanceToPLayer() >= sqrAttackDistance;
    }
}