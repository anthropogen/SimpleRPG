using EpicRPG.Characters;
using UnityEngine;

namespace EpicRPG.Hero
{
    public class PlayerDeathState : PlayerState
    {
        [SerializeField] private PlayerHealth health;
        public override void Enter()
        {
            health.enabled = false;
            animator.PlayDeath();
        }

        public override void Exit()
        {

        }

        public override void Run()
        {

        }
    }
}