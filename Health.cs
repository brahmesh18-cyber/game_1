using System;
using UnityEngine;

namespace EchoBound
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float maxHealth = 100f;

        public float CurrentHealth { get; private set; }
        public float MaxHealth => maxHealth;
        public bool IsDead => CurrentHealth <= 0f;

        public event Action<float> Damaged;
        public event Action Died;

        private void Awake()
        {
            CurrentHealth = maxHealth;
        }

        public void TakeDamage(float amount)
        {
            if (IsDead || amount <= 0f) return;

            CurrentHealth = Mathf.Max(0f, CurrentHealth - amount);
            Damaged?.Invoke(amount);

            if (IsDead)
                Died?.Invoke();
        }

        public void Restore(float amount)
        {
            if (amount <= 0f || IsDead) return;
            CurrentHealth = Mathf.Min(maxHealth, CurrentHealth + amount);
        }
    }
}
