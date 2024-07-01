using UnityEngine;

namespace Scenario
{
    public class StepChain : ScenarioStep
    {
        [Space]
        [SerializeField] private ScenarioStep[] steps;

        [Space]
        [SerializeField] private bool markFinishByTarget;
        [SerializeField] private ScenarioStep finishTarget;

        private bool isFinished;
        private int index;

        public override void Play()
        {
            base.Play();

            isFinished = false;
            index = 0;
            
            if (steps == null || steps.Length == 0)
            {
                FinishStep();
            }

            ScenarioStep step = steps[index];
            step.OnFinish -= OnFinishSubstep;
            step.OnFinish += OnFinishSubstep;
            step.Play();
        }

        private void OnFinishSubstep()
        {
            ScenarioStep step = steps[index];

            step.OnFinish -= OnFinishSubstep;

            if (markFinishByTarget && step == finishTarget)
            {
                FinishStep();
            }

            index++;

            if (0 <= index && index < steps.Length) 
            {
                step = steps[index];

                step.OnFinish -= OnFinishSubstep;
                step.OnFinish += OnFinishSubstep;
                step.Play();
            }
            else if (!isFinished)
            {
                FinishStep();
            }
        }

        public override void FinishStep()
        {
            base.FinishStep();
            isFinished = true;
        }
    }
}