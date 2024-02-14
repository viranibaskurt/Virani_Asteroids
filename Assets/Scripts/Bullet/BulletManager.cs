using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    public class BulletManager : MonoBehaviour
    {
        [SerializeField] private BulletPool bulletPool;
        [SerializeField] private PlayAreaBounds playAreaBounds;
        private HashSet<Bullet> activeBullets = new HashSet<Bullet>();
        private HashSet<Bullet> releasingBullets = new HashSet<Bullet>();

        private void Start()
        {
            bulletPool.OnBulletGet += BulletPool_OnBulletGet;
            bulletPool.OnBulletRelease += BulletPool_OnBulletRelease;
        }

        private void Update()
        {
            foreach (Bullet bullet in activeBullets)
            {
                float currentTime = Time.time;
                if (bullet.LifeTime < currentTime - bullet.FireTime || bullet.IsHit)
                {
                    releasingBullets.Add(bullet);
                }
            }

            FlushReleasingBullets();

        }

        private void FlushReleasingBullets()
        {
            foreach (Bullet releasingBullet in releasingBullets)
            {
                bulletPool.Release(releasingBullet);
            }
            releasingBullets.Clear();
        }

        public void ClearAll()
        {
            foreach (Bullet bullet in activeBullets)
            {
                releasingBullets.Add(bullet);
            }
            activeBullets.Clear();
            FlushReleasingBullets();
        }

        private void BulletPool_OnBulletRelease(Bullet bullet)
        {
            playAreaBounds.ClampedRb2dTr.Remove(bullet.transform);
            activeBullets.Remove(bullet);
        }

        private void BulletPool_OnBulletGet(Bullet bullet)
        {
            activeBullets.Add(bullet);
            playAreaBounds.ClampedRb2dTr.Add(bullet.transform);
        }
    }
}
