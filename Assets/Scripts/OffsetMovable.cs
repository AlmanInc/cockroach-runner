using UnityEngine;

namespace CockroachRunner
{
    public class OffsetMovable : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Transform watchOutObject;
        
        private Vector3 offset;

        private void Start () 
        {
            offset = target.position - watchOutObject.position;
        }

        private void LateUpdate () 
        {
            target.position = watchOutObject.position + offset;
        }
    }
}