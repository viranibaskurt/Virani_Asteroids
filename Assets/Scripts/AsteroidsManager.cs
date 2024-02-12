using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Asteroids
{

    public class AsteroidsManager : MonoBehaviour
    {
        [SerializeField] private AsteroidPool asteroidPool;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                var asteroid = asteroidPool.Get();
                asteroid.transform.position = Vector3.zero;
                asteroid.GetComponent<MoveComponent>().Velocity = new Vector2(
                    Random.Range(1, 5) * 0.25f, Random.Range(1, 5) * 0.25f);
                asteroid.gameObject.SetActive(true);
            }
        }

    }
}
