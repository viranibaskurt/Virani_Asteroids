using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    public class MoveComponent : MonoBehaviour
    {
        [SerializeField] private Vector2 velocity;
        public Vector2 Velocity
        {
            get => velocity;
            set => velocity = value;
        }

        private void Update()
        {
            transform.Translate(Velocity * Time.deltaTime);
        }
    }

}