using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private Ship ship;
        [SerializeField] private AsteroidsManager asteroidsManager;
        [SerializeField] private int numberOfAsteroidsInLevel = 2;
        private void Start()
        {
            ship.ShipExploded += EndGame;
            asteroidsManager.OnAsteroidsCleared += LevelCleared;
            ship.SetState(false);
        }

        private void OnDestroy()
        {
            ship.ShipExploded -= EndGame;
            asteroidsManager.OnAsteroidsCleared -= LevelCleared;
        }

        public void StartGame()
        {
            ship.SetState(true);
            asteroidsManager.CreateAsteroids(numberOfAsteroidsInLevel);
        }

        public void EndGame()
        {
            Debug.Log("End Game");
        }

        public void LevelCleared()
        {
            Debug.Log("level cleared");
        }
    }
}
