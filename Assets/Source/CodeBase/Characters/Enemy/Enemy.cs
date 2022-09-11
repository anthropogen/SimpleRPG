using UnityEngine;

namespace EpicRPG.Characters.Enemies
{
    public class Enemy : GameEntity
    {
        [SerializeField] private EnemyStateMachine fsm;
    }
}