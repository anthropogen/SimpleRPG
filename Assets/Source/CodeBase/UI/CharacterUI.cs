using EpicRPG.Characters;
using UnityEngine;

namespace EpicRPG.UI
{
    public class CharacterUI : MonoBehaviour
    {
        [SerializeField] private HPBar hpBar;
        private Health health;

        public void Construct(Health health)
        {
            TryUnsubscribe();
            this.health = health;
            health.HealthChanged += OnHealthChanged;
        }

        private void OnDestroy()
        {
            TryUnsubscribe();
        }

        private void TryUnsubscribe()
        {
            if (health != null)
                health.HealthChanged -= OnHealthChanged;
        }

        private void OnHealthChanged(float current, float max)
            => hpBar.SetValue(current / max);
    }
}