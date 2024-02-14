using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    public class Turret : MonoBehaviour
    {
        [SerializeField] private float fireImpulse = 1f;
        [SerializeField] private BulletPool bulletPool;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Fire();
            }
        }
        private void Fire()
        {
            Bullet bullet = bulletPool.Get();
            bullet.Fire(transform.position, transform.up, fireImpulse, transform.parent.gameObject);
        }
    }
}
