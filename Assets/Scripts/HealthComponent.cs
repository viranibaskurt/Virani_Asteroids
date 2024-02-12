using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private int initialHealth;

        public int Health { get; set; }

        private void Awake()
        {
            Health = initialHealth;
        }

        public int TakeDamage(int amount)
        {
            Health = Mathf.Max(Health - amount, 0);
            return Health;
        }
    }
}
