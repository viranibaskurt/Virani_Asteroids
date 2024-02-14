using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    public class Asteroid : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private MoveComponent moveComponent;
        [SerializeField] private HealthComponent healthComponent;
        public HealthComponent HealthComponent => healthComponent;
        public MoveComponent MoveComponent => moveComponent;

        public AsteroidType Type { get; set; }
        public Sprite Sprite
        {
            get => spriteRenderer.sprite;
            set => spriteRenderer.sprite = value;
        }

        public Vector3 LocalScale
        {
            get => transform.localScale;
            set => transform.localScale = value;
        }

        public Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }
    }
}
