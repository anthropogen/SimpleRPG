using UnityEngine;

namespace EpicRPG.Characters
{
    public class Health : MonoBehaviour
    {
        [SerializeField, Range(0, 10000)] private float max;
        private float current;

        public void ApplyDamage(float damage)
        {
            current = Mathf.Min(0, current - Mathf.Abs(damage));
            if (current == 0)
            {
                Debug.Log("Death");
            }
        }

        public void AddHealth(float addedHealth)
            => current = Mathf.Min(max, current + Mathf.Abs(addedHealth));
    }
}