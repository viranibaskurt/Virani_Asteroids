using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private PoolBase[] pools;
        private void Awake()
        {
            foreach (var pool in pools)
            {
                pool.Initialize();
            }
        }

        private void OnDestroy()
        {
            foreach (var pool in pools)
            {
                pool.Clear();
            }
        }
    }

}