using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Asteroids
{
    [CreateAssetMenu(fileName = "AsteroidPool", menuName = "ScriptableObjects/AsteroidPoolScriptableObject", order = 1)]

    public class AsteroidPool : PoolBase
    {
        [SerializeField] private Asteroid asteroidPrefab;
        private IObjectPool<Asteroid> pool;

        public override void Initialize()
        {
            pool = new ObjectPool<Asteroid>(OnCreateCallback, OnGetCallback, OnReleaseCallback, OnDestroyCallback);
        }

        public override void Clear()
        {
            pool.Clear();
        }

        public Asteroid Get()
        {
            return pool.Get();
        }

        public void Release(Asteroid asteroid)
        {
            pool.Release(asteroid);
        }

        private Asteroid OnCreateCallback()
        {
            return Instantiate(asteroidPrefab);
        }

        private void OnGetCallback(Asteroid asteroid)
        {
            asteroid.ResetAsteroid();
            asteroid.gameObject.SetActive(true);
        }

        private void OnReleaseCallback(Asteroid asteroid)
        {
            asteroid.gameObject.SetActive(false);
        }
        private void OnDestroyCallback(Asteroid asteroid)
        {
            if (asteroid)
            {
                Destroy(asteroid.gameObject);
            }
        }
    }
}
