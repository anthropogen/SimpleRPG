using EpicRPG.Hero;
using EpicRPG.StaticData;
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
        [SerializeField] private Attacker attacker;
        private LazyInitializy<Player> player;
        public event Action<Enemy> EnemyDeath;
        public EnemyStaticData StaticData { get; private set; }

        public void Construct(EnemyStaticData data, LazyInitializy<Player> player)
        {
            this.player = player;
            StaticData = data;
            attacker.Construct(data);
            health.Death += OnDeath;
            health.Construct(data.HP, data.HP);
            characterUI.Construct(health);
            StartCoroutine(Initialaze());
        }

        protected override void Disable()
        {
            health.Death += OnDeath;
        }

        private void OnDeath()
        {
            EnemyDeath?.Invoke(this);
            fsm.Death();
        }

        private IEnumerator Initialaze()
        {
            while (player.Value == null)
            {
                yield return null;
            }
            fsm.Construct(player.Value, StaticData);
        }

    }
}