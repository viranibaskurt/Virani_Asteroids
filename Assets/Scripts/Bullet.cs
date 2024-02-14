using System.Collections;
using UnityEngine;

namespace Asteroids
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private TriggerComponent triggerComponent;
        [SerializeField] private DamageDealerComponent damageDealerComponent;
        [SerializeField] private Rigidbody2D rb2d;
        [SerializeField] private float lifeTime = 1f;
        public Rigidbody2D Rb2d => rb2d;

        public float LifeTime => lifeTime;
        public GameObject Instigator { get; private set; }
        public float FireTime { get; private set; }
        public bool IsHit { get; private set; }
        private void Awake()
        {
            triggerComponent.TriggerEnter2D += TriggerComponent_TriggerEnter2D;
        }

        private void OnDestroy()
        {
            triggerComponent.TriggerEnter2D -= TriggerComponent_TriggerEnter2D;
        }

        public void Fire(Vector2 position, Vector2 direction, float impulse, GameObject instigator)
        {
            Instigator = instigator;
            transform.position = position;
            transform.up = direction;
            rb2d.simulated = true;
            rb2d.AddForce(impulse * transform.up, ForceMode2D.Impulse);
            FireTime = Time.time;
        }

        public void ResetState()
        {
            rb2d.velocity = Vector2.zero;
            rb2d.angularVelocity = 0f;
            rb2d.simulated = false;
            IsHit = false;
            Instigator = null;
        }

        private void TriggerComponent_TriggerEnter2D(Collider2D col2d)
        {
            //TODO create active damage dealer and move this part
            var health = col2d.gameObject.GetComponentInParent<HealthComponent>();
            if (health && health != Instigator)
            {
                health.TakeDamage(damageDealerComponent.Amount);
                IsHit = true;
            }
        }
    }
}
