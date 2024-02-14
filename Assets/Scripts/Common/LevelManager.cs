using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private Ship ship;
        [SerializeField] private EnemyShip enemyShip;
        [SerializeField] private AsteroidsManager asteroidsManager;
        [SerializeField] private PlayAreaBounds playAreaBounds;
        [SerializeField] private BulletManager bulletManager;
        [SerializeField] private UiManager uiManager;
        [SerializeField] private int numberOfAsteroidsInLevel = 2;

        private Coroutine spawnEnemyShipCoroutine;
        private bool IsLevelCleared => !enemyShip.IsAlive && asteroidsManager.IsCleared;
        private void Start()
        {
            ship.HealthComponent.OnHealthChanged += ShipHealthChanged;
            enemyShip.HealthComponent.OnHealthChanged += EnemyShipHealthChanged;
            asteroidsManager.OnAsteroidsCleared += AsteroidsCleared;
            ship.SetActive(false);
        }

        private void OnDestroy()
        {
            ship.HealthComponent.OnHealthChanged -= ShipHealthChanged;
            enemyShip.HealthComponent.OnHealthChanged -= EnemyShipHealthChanged;
            asteroidsManager.OnAsteroidsCleared -= AsteroidsCleared;
        }

        /// <summary>
        /// Clears the current level and generates a new one
        /// </summary>
        public void StartGame()
        {
            asteroidsManager.ClearAsteroids();
            bulletManager.ClearAll();
            ship.ResetShip();

            ship.SetActive(true);
            asteroidsManager.CreateAsteroids(numberOfAsteroidsInLevel);

            if (spawnEnemyShipCoroutine != null)
                StopCoroutine(spawnEnemyShipCoroutine);
            spawnEnemyShipCoroutine = StartCoroutine(SpawnEnemyShipRoutione());
        }

        /// <summary>
        /// Spawns the enemy ship after with a delay
        /// </summary>
        /// <returns></returns>
        private IEnumerator SpawnEnemyShipRoutione()
        {
            enemyShip.SetActive(false);
            yield return new WaitForSeconds(2f);
            enemyShip.Position = playAreaBounds.GetRandomPtInScreenInWorldSpace(1f);
            enemyShip.ResetShip();
            enemyShip.SetActive(true);
            spawnEnemyShipCoroutine = null;
        }

        public void ShipHealthChanged(HealthComponent shipHealth)
        {
            if (shipHealth.Health <= 0)
            {
                uiManager.SetMainScreenActive(true);
                uiManager.SetGameOverScreenActive(true);
            }
        }

        private void EnemyShipHealthChanged(HealthComponent enemyShipHealth)
        {
            if (enemyShipHealth.Health <= 0 && IsLevelCleared)
            {
                LevelCleared();
            }
        }

        public void AsteroidsCleared()
        {
            if (IsLevelCleared)
            {
                LevelCleared();
            }
        }

        private void LevelCleared()
        {
            StartGame();
        }
    }
}
