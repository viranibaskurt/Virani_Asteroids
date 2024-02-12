using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    public class Ship : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb2d;
        [SerializeField] private ColliderComponent colliderComponent;
        [SerializeField] private HealthComponent healthComponent;
        [SerializeField] private ShipEngine engine;
        //TODO set it to ship settings
        [SerializeField] private float linearDrag = 1f;
        [SerializeField] private float angularDrag = 1f;

        private float horizontalInput;
        private float verticalInput;

        public event Action ShipExploded;

        private void Awake()
        {
            rb2d.drag = linearDrag;
            rb2d.angularDrag = angularDrag;
        }

        void Start()
        {
            colliderComponent.CollisionEnter2D += ColliderComponent_CollisionEnter2D;
        }

        void Update()
        {
            //Todo use new input package
            engine.Torque = Input.GetAxis("Horizontal");
            engine.Force = Input.GetAxis("Vertical");
        }

        private void OnDestroy()
        {
            colliderComponent.CollisionEnter2D -= ColliderComponent_CollisionEnter2D;
        }

        private void ColliderComponent_CollisionEnter2D(Collision2D collision2D)
        {
            //Todo replace it with damage provider
            var damageDealer = collision2D.gameObject.GetComponentInParent<DamageDealerComponent>();
            if (damageDealer)
            {
                if (healthComponent.TakeDamage(damageDealer.Amount) <= 0)
                {
                    Explode();
                }
            }
        }

        private void Explode()
        {
            Debug.Log("Ship Exploded");
            ShipExploded?.Invoke();
        }
    }

}