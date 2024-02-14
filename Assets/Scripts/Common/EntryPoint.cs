using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private Ship ship;
        [SerializeField] private EnemyShip enemyShip;
        [SerializeField] private LevelManager levelManager;
        [SerializeField] private PlayAreaBounds bounds;
        [SerializeField] private UiManager uiManager;
        [SerializeField] private PoolBase[] pools;
        private void Awake()
        {
            foreach (var pool in pools)
            {
                pool.Initialize();
            }
        }

        private void Start()
        {
            bounds.ClampedRb2dTr.Add(ship.transform);
            bounds.ClampedTr.Add(enemyShip.transform);
            uiManager.PlayButton.onClick.AddListener(levelManager.StartGame);
            enemyShip.AutoTurret.Target = ship;
        }

        private void OnDestroy()
        {
            foreach (var pool in pools)
            {
                pool.Clear();
            }
            uiManager.PlayButton.onClick.RemoveListener(levelManager.StartGame);
        }

    }
}