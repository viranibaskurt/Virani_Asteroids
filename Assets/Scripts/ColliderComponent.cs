using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Asteroids
{
    public class ColliderComponent : MonoBehaviour
    {
        public event Action<Collision2D> CollisionEnter2D;
        public event Action<Collision2D> CollisionStay2D;
        public event Action<Collision2D> CollisionExit2D;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            CollisionEnter2D?.Invoke(collision);
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            CollisionStay2D?.Invoke(collision);
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            CollisionExit2D?.Invoke(collision);
        }
    }
}
