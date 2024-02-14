using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    public class BaseShip : MonoBehaviour
    {
        [SerializeField] protected HealthComponent healthComponent;
        public HealthComponent HealthComponent => healthComponent;
        public bool IsAlive => healthComponent.Health > 0;
        public Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }
        protected virtual void Start()
        {
            healthComponent.OnHealthChanged += HealthComponent_OnHealthChanged;
        }

        protected virtual void OnDestroy()
        {
            healthComponent.OnHealthChanged -= HealthComponent_OnHealthChanged;
        }

        private void HealthComponent_OnHealthChanged(HealthComponent healthComp)
        {
            if (healthComp.Health <= 0)
            {
                Explode();
            }
        }

        private void Explode()
        {
            SetActive(false);
        }

        public void SetActive(bool state)
        {
            gameObject.SetActive(state);
        }

        public void ResetShip()
        {
            healthComponent.ResetHealth();
        }
    }
}
