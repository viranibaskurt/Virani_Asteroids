using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    public class BulletManager : MonoBehaviour
    {
        [SerializeField] private BulletPool bulletPool;
        private HashSet<Bullet> activeBullets = new HashSet<Bullet>();
        private HashSet<Bullet> releasingBullets = new HashSet<Bullet>();

        void Start()
        {
            bulletPool.OnBulletGet += BulletPool_OnBulletGet;
            bulletPool.OnBulletRelease += BulletPool_OnBulletRelease;
        }

        void Update()
        {
            foreach (Bullet bullet in activeBullets)
            {
                float currentTime = Time.time;
                if (bullet.LifeTime < currentTime - bullet.FireTime || bullet.IsHit)
                {
                    releasingBullets.Add(bullet);
                }
            }

            foreach (Bullet releasingBullet in releasingBullets)
            {
                bulletPool.Release(releasingBullet);
            }
            releasingBullets.Clear();

        }

        private void BulletPool_OnBulletRelease(Bullet bullet)
        {
            activeBullets.Remove(bullet);
        }

        private void BulletPool_OnBulletGet(Bullet bullet)
        {
            activeBullets.Add(bullet);
        }
    }
}
