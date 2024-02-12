using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    public class ShipEngine : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb2d;
        [SerializeField] private float torqueMultiplier = 1f;
        [SerializeField] private float forceMultiplier = 1f;

        public float Torque { get; set; }
        public float Force { get; set; }

        private void FixedUpdate()
        {
            if (Torque != 0f)
            {
                rb2d.AddTorque(-Torque * torqueMultiplier);
            }
            if (Force != 0f)
            {
                rb2d.AddForce(transform.up.normalized * Force * forceMultiplier);
            }
            Torque = 0f;
            Force = 0f;
        }
    }

}