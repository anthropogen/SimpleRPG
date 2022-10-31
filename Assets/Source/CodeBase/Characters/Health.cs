using System;
using UnityEngine;

namespace SimpleRPG.Characters
{
    public class Health : MonoBehaviour
    {
        [SerializeField, Range(0, 10000)] protected float max;
        private float current;
        public event Action<float, float> HealthChanged;
        public event Action AppliedDamage;
        public event Action AddedHealth;
        public event Action Death;

        private void OnValidate()
        {
            Construct(max, max);
        }
        public float Current
        {
            get { return current; }
            protected set
            {
                HealthChanged?.Invoke(value, max);
                current = value;
            }
        }

        public void Construct(float current, float max)
        {
            if (!CheckGreaterZero(max) || !CheckGreaterZero(current) || current > max)
                throw new ArgumentException();
            this.max = max;
            Current = current;
        }

        public void ApplyDamage(float damage)
        {
            Current = Mathf.Max(0, Current - Mathf.Abs(damage));

            if (Current == 0)
                Death?.Invoke();
            else
                AppliedDamage?.Invoke();
        }

        public void AddHealth(float addedHealth)
        {
            Current = Mathf.Min(max, Current + Mathf.Abs(addedHealth));
            AddedHealth?.Invoke();
        }

        private bool CheckGreaterZero(float value)
            => value > 0;
    }
}