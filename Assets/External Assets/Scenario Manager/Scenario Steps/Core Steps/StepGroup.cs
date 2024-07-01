using System.Collections.Generic;
using UnityEngine;

namespace Scenario
{
    public class StepGroup : ScenarioStep
    {
        [Space]
        [SerializeField] private List<ScenarioStep> steps;

        [Space]
        [SerializeField] private bool markFinishByTargets;
        [SerializeField] private List<ScenarioStep> finishTargets;

        private int finishedStepCount;

        public override void Play()
        {            
            base.Play();

            if (steps == null || steps.Count == 0)
            {
                FinishStep();
            }

            if (markFinishByTargets && (finishTargets == null || finishTargets.Count == 0))
            {
                FinishStep();
            }

            finishedStepCount = 0;

            foreach (ScenarioStep step in steps)
            {
                if (!markFinishByTargets || (markFinishByTargets && finishTargets.Contains(step)))
                {
                    step.OnFinish -= OnFinishSubstep;
                    step.OnFinish += OnFinishSubstep;
                }

                step.Play();
            }
        }
                        
        private void OnFinishSubstep()
        {
            finishedStepCount++;

            int targetCount = !markFinishByTargets ? steps.Count : finishTargets.Count;

            if (finishedStepCount >= targetCount)
            {
                foreach (ScenarioStep step in steps)
                {
                    step.OnFinish -= OnFinishSubstep;
                }

                FinishStep();
            }
        }
    }
}