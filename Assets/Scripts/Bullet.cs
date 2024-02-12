using System.Collections;
using UnityEngine;

namespace Asteroids
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private ColliderComponent colliderComponent;
        [SerializeField] private DamageDealerComponent damageDealerComponent;
        [SerializeField] private Rigidbody2D rb2d;
        [SerializeField] private float lifeTime = 1f;

        public Rigidbody2D Rb2d => rb2d;

        public float LifeTime => lifeTime;
        public float FireTime { get; private set; }

        private void Awake()
        {
            colliderComponent.CollisionEnter2D += ColliderComponent_CollisionEnter2D;
        }

        private void OnDestroy()
        {
            colliderComponent.CollisionEnter2D -= ColliderComponent_CollisionEnter2D;
        }

        public void Fire(Vector2 position, Vector2 direction, float impulse)
        {
            //TODO set kinematic until it's fired
            transform.position = position;
            transform.up = direction;
            rb2d.simulated = true;
            rb2d.AddForce(impulse * transform.up, ForceMode2D.Impulse);
            FireTime = Time.time;
        }

        private void ColliderComponent_CollisionEnter2D(Collision2D collision2D)
        {
            //TODO create active damage dealer and move this part
            var health = collision2D.gameObject.GetComponentInParent<HealthComponent>();
            if (health)
            {
                health.TakeDamage(damageDealerComponent.Amount);
                //return this to pool
            }
        }
    }
}
