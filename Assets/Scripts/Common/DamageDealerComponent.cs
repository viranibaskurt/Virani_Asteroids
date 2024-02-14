using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    public interface IDamageReceiver
    {
        int TakeDamage(int amount);
    }

    public class DamageDealerComponent : MonoBehaviour
    {
        [SerializeField] private int amount = 1;
        [SerializeField] private TriggerComponent triggerComponent;

        public Collider2D InstigatorCollider { get; set; }
        public event Action<DamageDealerComponent> OnDamageDealt;
        public int Amount
        {
            get => amount;
            set => amount = value;
        }

        private void Awake()
        {
            triggerComponent.TriggerEnter2D += TriggerComponent_TriggerEnter2D;
        }

        private void OnDestroy()
        {
            triggerComponent.TriggerEnter2D -= TriggerComponent_TriggerEnter2D;
        }

        private void TriggerComponent_TriggerEnter2D(Collider2D col2d)
        {
            IDamageReceiver receiver = col2d.GetComponentInParent<IDamageReceiver>();
            if (receiver != null && InstigatorCollider != col2d)
            {
                receiver.TakeDamage(Amount);
                OnDamageDealt?.Invoke(this);
            }
        }
    }
}
