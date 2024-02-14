using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    public class Turret : MonoBehaviour
    {
        [SerializeField] private float fireImpulse = 1f;
        [SerializeField] private BulletPool bulletPool;
        [SerializeField] private Collider2D collider2d;
        protected bool IsFiring { get; set; } = false;
        private void FixedUpdate()
        {
            if (IsFiring)
            {
                IsFiring = false;
                Bullet bullet = bulletPool.Get();
                bullet.Fire(transform.position, transform.up, fireImpulse, collider2d);
            }
        }

        public void Fire()
        {
            IsFiring = true;
        }
    }
}
