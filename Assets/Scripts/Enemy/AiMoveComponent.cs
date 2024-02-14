using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    public class AiMoveComponent : MoveComponent
    {
        [SerializeField] private float yVelocity = 2f;
        private Coroutine directionCoroutione;
        float lastYDirection = -1f;

        private void OnEnable()
        {
            directionCoroutione = StartCoroutine(DirectionRoutine());
        }

        private void OnDisable()
        {
            StopCoroutine(directionCoroutione);
            directionCoroutione = null;
        }


        /// <summary>
        /// Moves the ship up and down in random time intervals
        /// </summary>
        private IEnumerator DirectionRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(30, 50) * 0.1f);
                lastYDirection = -lastYDirection;
                velocity.y = lastYDirection * yVelocity;
                yield return new WaitForSeconds(Random.Range(20, 30) * 0.1f);
                velocity.y = 0;
            }
        }

    }
}
