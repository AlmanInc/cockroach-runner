using UnityEngine;

namespace CockroachRunner
{
    public class EnvironmentObject : MonoBehaviour
    {
        [SerializeField] private Transform cachedTransform;

        public Transform CachedTransform => cachedTransform;

        public virtual void Activate() { }
    }
}