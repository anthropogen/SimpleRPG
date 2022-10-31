using SimpleRPG.Infrastructure;
using UnityEngine;

namespace SimpleRPG.Items
{
    public class Projectile : GameEntity
    {
        [SerializeField, Range(0.01f, 100f)] private float speed;
        [SerializeField] private new Rigidbody rigidbody;
        [field: SerializeField] public ProjectileType ProjectileType { get; private set; }
        private float damage;

        public void Init(float damage, Vector3 target)
        {
            transform.rotation = Quaternion.LookRotation(target - transform.position);
            this.damage = damage;
        }

        protected override void FixedLoop()
        {
            rigidbody.velocity = transform.forward * speed;
        }

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("Collision");
        }
    }
}
