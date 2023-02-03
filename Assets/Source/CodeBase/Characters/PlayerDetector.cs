using SimpleRPG.Hero;
using SimpleRPG.Infrastructure;
using System;
using UnityEngine;

namespace SimpleRPG.Characters
{
    public class PlayerDetector : GameEntity
    {
        public event Action<Player, bool> InteractPlayer;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player player))
                InteractPlayer?.Invoke(player, true);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Player player))
                InteractPlayer?.Invoke(player, false);
        }
    }
}