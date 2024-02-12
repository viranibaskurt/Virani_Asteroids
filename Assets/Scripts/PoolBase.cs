using UnityEngine;

namespace Asteroids
{
    public abstract class PoolBase : ScriptableObject
    {
        public abstract void Initialize();
        public abstract void Clear();
    }
}
