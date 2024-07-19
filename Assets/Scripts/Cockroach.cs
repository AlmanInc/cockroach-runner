using UnityEngine;

namespace CockroachRunner
{
    public class Cockroach : MonoBehaviour
    {
        [SerializeField] private Transform cachedTransform;
        [SerializeField] private Animator controller;

        public Transform CachedTransform => cachedTransform;

        public void SetSpeed(float speed)
        {
            if (speed == 0f)
            {
                controller.SetBool("Run", false);
            }
            else
            {
                controller.SetBool("Run", true);
                controller.SetFloat("Speed", speed);
            }
        }        
    }
}