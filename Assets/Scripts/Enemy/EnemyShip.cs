using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    public class EnemyShip : BaseShip
    {
        [SerializeField] private AutoTurret autoTurret;
        public AutoTurret AutoTurret => autoTurret;

    }
}
