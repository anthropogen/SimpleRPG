using EpicRPG.Hero;
using EpicRPG.UI;
using System;
using System.Collections;
using UnityEngine;

namespace EpicRPG.Characters.Enemies
{
    [RequireComponent(typeof(EnemyStateMachine))]
    public class Enemy : GameEntity
    {
        [SerializeField] private EnemyStateMachine fsm;
        [SerializeField] private Health health;
        [SerializeField] private CharacterUI characterUI;
        private Player player;
        protected override void Enable()
        {
            health.Death += OnDeath;
            characterUI.Construct(health);
        }
        protected override void Disable()
        {
            health.Death += OnDeath;
        }

        private void OnDeath()
        {
            fsm.Death();
        }

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