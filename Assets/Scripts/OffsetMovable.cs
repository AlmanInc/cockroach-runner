using UnityEngine;

namespace CockroachRunner
{
    public class OffsetMovable : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Transform watchOutObject;
        [SerializeField] private bool useZMovable = true;
        [SerializeField] private Vector3 specialOffset;

        private Vector3 offset3D;
        private float offset;

        private void Start()
        {
            if (useZMovable)
            {
                offset = target.position.z - watchOutObject.position.z;
            }
            else
            {
                offset3D = target.position - watchOutObject.position;
                target.position = watchOutObject.position + offset3D + specialOffset;
            }
        }

        private void LateUpdate()
        {
            if (useZMovable)
            {
                Vector3 position = target.position;
                position.z = watchOutObject.position.z + offset;
                target.position = position;
            }
            else
            {
                target.position = watchOutObject.position + offset3D + specialOffset;
            }
        }
    }
}