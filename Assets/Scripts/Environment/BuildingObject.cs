using System.Collections;
using UnityEngine;

namespace CockroachRunner
{
    public class BuildingObject : EnvironmentObject
    {
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 activationOffset;
        [SerializeField] private Vector3 activationScale;
        [SerializeField] private float positionDuration = 0.5f;
        [SerializeField] private float scaleDuration = 0.5f;

        private Vector3 startLocalPosition;
        private Vector3 startScale;

        private void Start()
        {
            startLocalPosition = target.localPosition;
            startScale = transform.localScale;            
        }

        public override void Activate()
        {            
            base.Activate();
            
            if (positionDuration > 0)
            {
                target.localPosition = startLocalPosition + activationOffset;
                StartCoroutine(RestorePositionProcess());
            }

            if (scaleDuration > 0) 
            {
                target.localScale = activationScale;
                StartCoroutine(RestoreScaleProcess());
            }
        }   
        
        private IEnumerator RestorePositionProcess()
        {
            float time = 0f;

            Vector3 shiftedLocalPosition = target.localPosition;

            while (time <= positionDuration) 
            {
                float progress = Mathf.Clamp01(time / positionDuration);
                target.localPosition = Vector3.Lerp(shiftedLocalPosition, startLocalPosition, progress);

                time += Time.deltaTime;

                yield return null;
            }

            target.localPosition = startLocalPosition;
        }

        private IEnumerator RestoreScaleProcess()
        {
            float time = 0f;

            Vector3 changedScale = target.localScale;

            while (time <= scaleDuration)
            {
                float progress = Mathf.Clamp01(time / scaleDuration);
                target.localScale = Vector3.Lerp(changedScale, startScale, progress);

                time += Time.deltaTime;

                yield return null;
            }

            target.localPosition = startLocalPosition;
        }
    }
}