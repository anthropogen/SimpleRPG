using EpicRPG.Hero;
using System.Collections;
using UnityEngine;

namespace EpicRPG.Characters.Enemies
{
    [RequireComponent(typeof(EnemyStateMachine))]
    public class Enemy : GameEntity
    {
        [SerializeField] private EnemyStateMachine fsm;
        private Player player;
        private void Start()
        {
            StartCoroutine(Initialaze());
        }

        private IEnumerator Initialaze()
        {
            while (player == null)
            {
                yield return null;
                player = GameObject.FindObjectOfType<Player>();
            }
            fsm.Construct(player);
        }
    }
}