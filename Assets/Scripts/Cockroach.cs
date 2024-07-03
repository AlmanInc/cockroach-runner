using System;
using UnityEngine;

namespace CockroachRunner
{
    public class Cockroach : MonoBehaviour
    {
        [Serializable]
        private struct SpeedModificator
        {
            public float outsideSpeed;
            public float animationSpeed;
        }

        [SerializeField] private Transform cachedTransform;
        [SerializeField] private Animator controller;
        
        [Space]
        [SerializeField] private SpeedModificator minSpeedModificator;
        [SerializeField] private SpeedModificator maxSpeedModificator;        

        public Transform CachedTransform => cachedTransform;

        public void SetSpeed(float speed)
        {
            if (speed == 0f)
            {
                controller.SetBool("Run", false);
            }
            else
            {
                float outSpeed = Mathf.Clamp(speed, minSpeedModificator.outsideSpeed, maxSpeedModificator.outsideSpeed);
                float progress = Mathf.Clamp01(Mathf.Abs(outSpeed - minSpeedModificator.outsideSpeed) / Mathf.Abs(maxSpeedModificator.outsideSpeed - minSpeedModificator.outsideSpeed));
                float targetSpeed = Mathf.Lerp(minSpeedModificator.animationSpeed, maxSpeedModificator.animationSpeed, progress);

                controller.SetBool("Run", true);
                controller.SetFloat("Speed", targetSpeed);
            }
        }        
    }
}