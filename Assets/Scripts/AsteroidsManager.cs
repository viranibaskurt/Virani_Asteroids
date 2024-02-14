using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Asteroids
{
    public class AsteroidsManager : MonoBehaviour
    {
        [SerializeField] private AsteroidConfiguration[] configs;
        [SerializeField] private AsteroidPool asteroidPool;
        [SerializeField] private PlayAreaBounds playAreaBounds;

        private System.Random rnd = new();
        private HashSet<Asteroid> activeAsteroids = new();

        public bool IsCleared => activeAsteroids.Count == 0;
        public event Action OnAsteroidsCleared;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                CreateAsteroids(1);
            }
            if (Input.GetKeyDown(KeyCode.H))
            {
                var ast = FindObjectsOfType<Asteroid>();
                ast[rnd.Next(0, activeAsteroids.Count)].HealthComponent.TakeDamage(1);
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                ClearAsteroids();
            }
        }

        //TODO shouldn't create where the ship is
        public void CreateAsteroids(int numberOfAsteroids)
        {
            for (int i = 0; i < numberOfAsteroids; i++)
            {
                AsteroidType type = rnd.Next(0, 2) == 1 ? AsteroidType.Large : AsteroidType.Medium;
                var asteroid = Create(type, null);
                activeAsteroids.Add(asteroid);
            }
        }

        public void ClearAsteroids()
        {
            foreach (Asteroid asteroid in activeAsteroids)
            {
                asteroid.HealthComponent.OnDamageTaken -= HealthComponent_OnDamageTaken;
                playAreaBounds.ClampedTr.Remove(asteroid.transform);
                asteroidPool.Release(asteroid);
            }
            activeAsteroids.Clear();
        }

        private void Destroy(Asteroid asteroid)
        {
            asteroid.HealthComponent.OnDamageTaken -= HealthComponent_OnDamageTaken;
            activeAsteroids.Remove(asteroid);
            asteroidPool.Release(asteroid);
        }

        private void HealthComponent_OnDamageTaken(HealthComponent asteroidHealthComp)
        {
            if (asteroidHealthComp.Health <= 0 && asteroidHealthComp.TryGetComponent(out Asteroid asteroid))
            {
                AsteroidType type = asteroid.Type;
                Vector3 pos = asteroid.Position;

                Destroy(asteroid);

                if (type > 0)
                {
                    SplitAsteroid(type, pos);
                }
                else if (IsCleared)
                {
                    OnAsteroidsCleared?.Invoke();
                }
            }
        }

        private void SplitAsteroid(AsteroidType type, Vector3 pos)
        {
            for (int i = 0; i < 2; i++)
            {
                Create(type - 1, pos);
            }
        }

        //Factory
        private Asteroid Create(AsteroidType type, Vector2? pos)
        {
            //depending on the configs size, array can be replaced with dictionary.
            AsteroidConfiguration config = Array.Find(configs, (AsteroidConfiguration item) => item.Type == type);
            var asteroid = asteroidPool.Get();
            asteroid.Sprite = config.Sprite;
            asteroid.LocalScale = new Vector3(config.Size, config.Size, 1);
            asteroid.Type = type;

            asteroid.Position = pos ?? playAreaBounds.GetRandomPtInScreenInWorldSpace(0.5f);
            float xV = rnd.Next(1, 10) * 0.1f;
            float yV = rnd.Next(1, 10) * 0.1f;
            float angle = rnd.Next(0, 360);
            var moveComponent = asteroid.MoveComponent;
            moveComponent.Angle = angle;
            moveComponent.Velocity = new Vector2(xV, yV);

            asteroid.HealthComponent.OnDamageTaken += HealthComponent_OnDamageTaken;
            playAreaBounds.ClampedTr.Add(asteroid.transform);

            return asteroid;
        }
    }

    [System.Serializable]
    public struct AsteroidConfiguration
    {
        [SerializeField] private AsteroidType type;
        [SerializeField] private float size;
        [SerializeField] private Sprite sprite;
        public AsteroidType Type => type;
        public float Size => size;
        public Sprite Sprite => sprite;
    }

    public enum AsteroidType
    {
        Small = 0,
        Medium,
        Large
    }
}
