using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Asteroids
{
    [CreateAssetMenu(fileName = "BulletPool", menuName = "ScriptableObjects/BulletPoolScriptableObject", order = 1)]
    public class BulletPool : PoolBase
    {
        [SerializeField] private Bullet bulletPrefab;
        private IObjectPool<Bullet> pool;

        public event Action<Bullet> OnBulletGet;
        public event Action<Bullet> OnBulletRelease;

        public override void Initialize()
        {
            pool = new ObjectPool<Bullet>(OnCreateCallback, OnGetCallback, OnReleaseCallback, OnDestroyCallback);
        }

        public override void Clear()
        {
            pool.Clear();
        }

        public Bullet Get()
        {
            var bullet = pool.Get();
            OnBulletGet?.Invoke(bullet);
            return bullet;
        }

        public void Release(Bullet bullet)
        {
            pool.Release(bullet);
            OnBulletRelease?.Invoke(bullet);
        }

        private Bullet OnCreateCallback()
        {
            Bullet bullet = Instantiate(bulletPrefab);
            bullet.Rb2d.simulated = false;
            return bullet;
        }

        private void OnGetCallback(Bullet bullet)
        {
            bullet.gameObject.SetActive(true);
        }

        private void OnReleaseCallback(Bullet bullet)
        {
            var rb2d = bullet.Rb2d;
            rb2d.velocity = Vector2.zero;
            rb2d.angularVelocity = 0f;
            rb2d.simulated = false;
            bullet.gameObject.SetActive(false);
        }
        private void OnDestroyCallback(Bullet bullet)
        {
            if (bullet)
            {
                Destroy(bullet.gameObject);
            }
        }
    }

}