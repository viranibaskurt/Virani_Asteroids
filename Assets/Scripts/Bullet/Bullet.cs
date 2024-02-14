using System.Collections;
using UnityEngine;

namespace Asteroids
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private TriggerComponent triggerComponent;
        [SerializeField] private Rigidbody2D rb2d;
        [SerializeField] private DamageDealerComponent damageDealerComponent;
        [SerializeField] private float lifeTime = 1f;
        public Rigidbody2D Rb2d => rb2d;
        public float LifeTime => lifeTime;
        public float FireTime { get; private set; }
        public bool IsHit { get; private set; }
        public Collider2D Instigator
        {
            get => damageDealerComponent.InstigatorCollider;
            set => damageDealerComponent.InstigatorCollider = value;
        }
        private void Awake()
        {
            damageDealerComponent.OnDamageDealt += DamageDealerComponent_OnDamageDealt;
        }

        private void OnDestroy()
        {
            damageDealerComponent.OnDamageDealt -= DamageDealerComponent_OnDamageDealt;
        }

        public void Fire(Vector2 position, Vector2 direction, float impulse, Collider2D instigator)
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

        private void DamageDealerComponent_OnDamageDealt(DamageDealerComponent _)
        {
            IsHit = true;
        }
    }
}
