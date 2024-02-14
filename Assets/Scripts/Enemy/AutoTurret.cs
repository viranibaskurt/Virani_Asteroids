using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    public interface IAutoTurretTarget
    {
        Vector3 Position { get; }
        bool IsAlive { get; }
    }

    public class AutoTurret : Turret
    {
        [SerializeField] private float idleTime = 3f;
        [SerializeField] private float fireCoolDownTime = 0.5f;
        [SerializeField] private int numberOfFire = 4;

        private Coroutine fireCoroutine;

        public IAutoTurretTarget Target { get; set; }

        private void OnEnable()
        {
            fireCoroutine = StartCoroutine(FireRoutine());
        }

        private void OnDisable()
        {
            StopCoroutine(fireCoroutine);
            fireCoroutine = null;
        }
        private IEnumerator FireRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(idleTime);
                if (Target != null && Target.IsAlive)
                {
                    for (int i = 0; i < numberOfFire; i++)
                    {
                        transform.up = (Target.Position - transform.position);
                        Fire();
                        yield return new WaitForSeconds(fireCoolDownTime);

                    }
                }
            }
        }
    }
}
