using System.Collections;
using UnityEngine;
using Scenario;

namespace CockroachRunner
{
    public class StepScale : ScenarioStep
    {        
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 targetScale;                
        [SerializeField] private float durarion = 0.5f;

        public override void Play()
        {
            base.Play();

            if (durarion <= 0f)
            {
                target.localScale = targetScale;
                FinishStep();
            }
            else
            {
                StartCoroutine(ScaleProcess());
            }
        }

        private IEnumerator ScaleProcess()
        {
            Vector3 startScale = target.localScale;
            float time = 0f;

            while (time < durarion) 
            {
                float progress = Mathf.Clamp01(time / durarion);
                target.localScale = Vector3.Lerp(startScale, targetScale, progress);

                yield return null;

                time += Time.deltaTime;
            }

            target.localScale = targetScale;
            FinishStep();
        }
    }
}