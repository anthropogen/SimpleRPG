using System.Collections;
using UnityEngine;
namespace SimpleRPG.Characters.Enemies
{
    public class EnemyDeathState : EnemyState
    {
        [SerializeField] private Health health;
        [SerializeField, Range(0, 10)] private float timeUntilDestroy;
        public override void Enter()
        {
            health.enabled = false;
            animator.Die();

            StartCoroutine(DeathRoutine());
        }

        public override void Exit()
        {

        }

        public override void Run()
        {
        }
        private IEnumerator DeathRoutine()
        {
            yield return new WaitForSeconds(timeUntilDestroy);
            transform.parent.gameObject.SetActive(false);
        }
    }
}