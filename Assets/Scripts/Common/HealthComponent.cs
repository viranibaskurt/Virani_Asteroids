using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    public class HealthComponent : MonoBehaviour, IDamageReceiver
    {
        [SerializeField] private int initialHealth;
        public event Action<HealthComponent> OnHealthChanged;
        public int Health { get; set; }

        private void Awake()
        {
            Health = initialHealth;
        }

        public int TakeDamage(int amount)
        {
            Health = Mathf.Max(Health - amount, 0);
            OnHealthChanged?.Invoke(this);
            return Health;
        }

        public void ResetHealth()
        {
            Health = initialHealth;
        }
    }
}
