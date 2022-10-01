using UnityEngine;

namespace EpicRPG.Items
{
    public class WeaponModel : GameEntity
    {
        [SerializeField] private Transform attackPoint;
        [field: SerializeField, Range(0.01f, 100)] public float RadiusAttack { get; internal set; }
        public Vector3 AttackPoint => attackPoint.position;

        private void OnDrawGizmos()
        {
            if (attackPoint == null) return;
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, RadiusAttack);
        }
    }
}
