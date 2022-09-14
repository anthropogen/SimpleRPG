using UnityEngine;

namespace EpicRPG.Characters.Enemies
{
    public class SawPlayerTransition : EnemyTransition
    {
        [SerializeField, Range(0, 100)] private float distanceView;
        [SerializeField, Range(0, 100)] private float dangerousDistance;
        [SerializeField, Range(0, 360)] private float viewAngle;
        private float sqrDistanceView;
        private float sqrDangerousDistance;

        private void OnEnable()
        {
            sqrDangerousDistance = dangerousDistance * dangerousDistance;
            sqrDistanceView = distanceView * distanceView;
        }

        public override bool NeedTransit()
        {
            var sqrDistanceToPlayer = SqrDistanceToPLayer();
            if (sqrDistanceToPlayer <= sqrDangerousDistance)
                return true;

            float angleToPlayer = Vector3.SignedAngle(player.transform.position - transform.position, transform.forward, Vector3.up);
            if (sqrDistanceToPlayer <= sqrDistanceView && angleToPlayer <= viewAngle)
                return true;

            return false;
        }
    }
}