using UnityEngine;
using Scenario;

namespace CockroachRunner
{
    public class StepPrepareRace : ScenarioStep
    {
        [SerializeField] private ScenarioStep prepareSceneStep;

        public override void Play()
        {
            base.Play();

            prepareSceneStep.Play();

            //FinishStep();
        }
    }
}