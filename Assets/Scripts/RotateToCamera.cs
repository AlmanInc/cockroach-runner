using System.Collections;
using UnityEngine;

namespace CockroachRunner
{
    public class RotateToCamera : MonoBehaviour
    {
        [SerializeField] private Transform cachedTransform;
        
        private void Start() => StartCoroutine(RotationProcess());

        private IEnumerator RotationProcess()
        {
            Camera cachedCamera = Camera.main;

            while (true)
            {
                transform.rotation = Quaternion.LookRotation(transform.position - cachedCamera.transform.position, Vector3.up);
                yield return null;
            }
        }
    }
}