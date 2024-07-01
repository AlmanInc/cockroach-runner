using UnityEngine;

namespace Scenario
{
    public class StepRandomAction : ScenarioStep
    {
        [SerializeField] private ScenarioStep[] steps;
        
        private ScenarioStep target;

        public override void Play()
        {
            base.Play();

            if (steps.Length == 0)
            {
                FinishStep();
            }
            else
            {
                target = steps[Random.Range(0, steps.Length)];
                target.OnFinish -= OnFinishSubstep;
                target.OnFinish += OnFinishSubstep;
                target.Play();
            }
        }

        private void OnFinishSubstep()
        {
            target.OnFinish -= OnFinishSubstep;
            FinishStep();
        }
    }
}