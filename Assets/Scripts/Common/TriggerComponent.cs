using System;
using UnityEngine;

namespace Asteroids
{
    public class TriggerComponent : MonoBehaviour
    {
        public event Action<Collider2D> TriggerEnter2D;
        public event Action<Collider2D> TriggerStay2D;
        public event Action<Collider2D> TriggerExit2D;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            TriggerEnter2D?.Invoke(collision);
        }
        private void OnTriggerStay2D(Collider2D collision)
        {
            TriggerStay2D?.Invoke(collision);
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            TriggerExit2D?.Invoke(collision);
        }
    }
}
