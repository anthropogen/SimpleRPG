using System;
using UnityEngine;

namespace EpicRPG.Characters
{
    public class Health : MonoBehaviour
    {
        [SerializeField, Range(0, 10000)] protected float max;
        public event Action<float, float> HealthChanged;
        private float current;
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
            this.Current = current;
        }


        public void ApplyDamage(float damage)
        {
            Current = Mathf.Max(0, Current - Mathf.Abs(damage));
            if (Current == 0)
            {
                Debug.Log("Death");
            }
        }

        public void AddHealth(float addedHealth)
            => Current = Mathf.Min(max, Current + Mathf.Abs(addedHealth));

        private bool CheckGreaterZero(float value)
            => value > 0;
    }
}