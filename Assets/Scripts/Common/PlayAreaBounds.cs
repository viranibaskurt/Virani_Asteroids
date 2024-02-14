using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace Asteroids
{
    public class PlayAreaBounds : MonoBehaviour
    {
        private Camera cam;
        private Random random = new();

        private HashSet<Transform> clampedTr = new();
        private HashSet<Transform> clampedRb2dTr = new();

        private Vector2 BottomLeft { get; set; }
        private Vector2 TopLeft { get; set; }
        private Vector2 BottomRight { get; set; }
        private Vector2 TopRight { get; set; }

        private float MinX => TopLeft.x;
        private float MaxX => TopRight.x;
        private float MinY => BottomLeft.y;
        private float MaxY => TopLeft.y;

        //objects to clamp after Update
        public ISet<Transform> ClampedTr => clampedTr;
        //objects to clamp after FixedUpdate
        public ISet<Transform> ClampedRb2dTr => clampedRb2dTr;

        private void Start()
        {
            cam = Camera.main;
            Initialize();
            StartCoroutine(LateFixedUpdate());
        }


        private void LateUpdate()
        {
            foreach (Transform tr in clampedTr)
            {
                ClampPosition(tr);
            }
        }

        /// <summary>
        /// Keeps physics object in the screen at the end of FixedUpdate
        /// </summary>
        private IEnumerator LateFixedUpdate()
        {
            while (true)
            {
                yield return new WaitForFixedUpdate();
                foreach (Transform rb2d in clampedRb2dTr)
                {
                    ClampPosition(rb2d);
                }
            }
        }

        /// <summary>
        /// Keeps transforms in the screen
        /// </summary>
        public void ClampPosition(Transform tr)
        {
            (bool isClamped, Vector2 clampVal) = Clamp(tr.position);
            if (isClamped)
            {
                tr.position = clampVal;
            }
        }

        public Vector2 GetRandomPtInScreenInWorldSpace(float margin)
        {
            return new Vector2(GetRandom(TopLeft.x + margin, TopRight.x - margin), GetRandom(BottomLeft.y + margin, TopLeft.y - margin));
        }

        private void Initialize()
        {
            float w = Screen.width;
            float h = Screen.height;

            BottomLeft = cam.ScreenToWorldPoint(new Vector3(0, 0, 0));
            BottomRight = cam.ScreenToWorldPoint(new Vector3(w, 0, 0));
            TopLeft = cam.ScreenToWorldPoint(new Vector3(0, h, 0));
            TopRight = cam.ScreenToWorldPoint(new Vector3(w, h, 0));
        }

        private void ClampPosition(Rigidbody2D rb2d)
        {
            //settings position over rb2d doesn't work
            (bool isClamped, Vector2 clampVal) = Clamp(rb2d.position);
            if (isClamped)
            {
                Vector2 vel = rb2d.velocity;
                float angularVel = rb2d.angularVelocity;
                rb2d.simulated = false;
                rb2d.position = clampVal;
                rb2d.simulated = true;
                rb2d.velocity = vel;
                rb2d.angularVelocity = angularVel;
            }
        }

        /// <summary>
        /// Clamps the pos in the game screen so that the position teleports to opposite edge if the it's out of game screen
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        private (bool isClamped, Vector2 clampVal) Clamp(Vector3 pos)
        {
            float clampedValX = Clamp(pos.x, MinX, MaxX);
            float clampedValY = Clamp(pos.y, MinY, MaxY);
            bool isClamped = !Mathf.Approximately(clampedValX, pos.x) || !Mathf.Approximately(clampedValY, pos.y);
            return (isClamped, new Vector2(clampedValX, clampedValY));
        }

        /// <summary>
        /// returns min if the value is greater than max, 
        /// or max if the values is smaller than min
        /// </summary>
        private float Clamp(float val, float min, float max)
        {
            float clampVal = val;
            if (val > max)
                clampVal = min;
            else if (val < min)
                clampVal = max;
            return clampVal;
        }

        private float GetRandom(float min, float max)
        {
            if (min >= max)
            {
                Debug.LogError("max should be bigger than min");
            }
            return (float)random.NextDouble() * (max - min) + min;
        }
    }

}