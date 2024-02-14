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
        public float Angle
        {
            get => transform.rotation.z;
            set => transform.rotation = Quaternion.AngleAxis(value, Vector3.forward);
        }

        private void Update()
        {
            transform.Translate(Velocity * Time.deltaTime);
        }
    }

}