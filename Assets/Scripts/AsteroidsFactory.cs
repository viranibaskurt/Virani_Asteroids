using System;
using UnityEngine;

namespace Asteroids
{
    public class AsteroidsFactory : MonoBehaviour
    {
        //TODO remove this class if you're not gonna use
        [SerializeField] private AsteroidPool asteroidPool;
        [SerializeField] private AsteroidConfiguration[] configs;

    }
}
