using System.Collections;
using UnityEngine;

namespace Scenario
{
    public class StepDelay : ScenarioStep
    {        
        [SerializeField] float delay;

        public override void Play()
        {
            base.Play();

            if (delay > 0)
            {
                StartCoroutine(PlayingStepProcess());
            }
            else
            {
                FinishStep();
            }
        }

        private IEnumerator PlayingStepProcess()
        {
            yield return new WaitForSeconds(delay);
            FinishStep();
        }
    }
}