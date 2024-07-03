using UnityEngine;

namespace CockroachRunner
{
    public class OffsetMovable : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Transform watchOutObject;
                
        private float offset;

        private void Start () 
        {
            offset = target.position.z - watchOutObject.position.z;
        }

        private void LateUpdate () 
        {
            Vector3 position = target.position;
            position.z = watchOutObject.position.z + offset;
            target.position = position;
        }
    }
}