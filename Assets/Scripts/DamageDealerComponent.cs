using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    public class DamageDealerComponent : MonoBehaviour
    {
        [SerializeField] private int amount = 1;

        public int Amount
        {
            get => amount;
            set => amount = value;
        }
    }
}
