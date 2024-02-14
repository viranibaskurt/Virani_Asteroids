using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    public class Ship : BaseShip, IAutoTurretTarget
    {
        [SerializeField] private Rigidbody2D rb2d;
        [SerializeField] private TriggerComponent triggerComponent;
        [SerializeField] private ShipEngine engine;
        [SerializeField] private Turret turret;

        //TODO set it to ship settings
        [SerializeField] private float linearDrag = 1f;
        [SerializeField] private float angularDrag = 1f;

        private float horizontalInput;
        private float verticalInput;


        private void Awake()
        {
            rb2d.drag = linearDrag;
            rb2d.angularDrag = angularDrag;
        }

        void Update()
        {
            //Todo New input package can be used
            engine.Torque = Input.GetAxis("Horizontal");
            engine.Force = Input.GetAxis("Vertical");
            if (Input.GetKeyDown(KeyCode.Space))
            {
                turret.Fire();
            }
        }

    }

}