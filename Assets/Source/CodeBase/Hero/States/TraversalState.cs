using SimpleRPG.Characters;
using UnityEngine;

namespace SimpleRPG.Hero
{
    public class TraversalState : PlayerState
    {
        [SerializeField] private CharacterAttacker attacker;
        public override void Enter()
        {
            attacker.ShowWeaponModel(false);
            animator.Traversal(true);
        }

        public override void Exit()
        {
            animator.Traversal(false);
        }

        public override void Run()
        {
            mover.Run();
        }
    }
}