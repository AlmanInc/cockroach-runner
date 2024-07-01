using UnityEngine;
using Scenario;

namespace CockroachRunner
{
    public class StepPrepareRace : ScenarioStep
    {
        public override void Play()
        {
            base.Play();
            FinishStep();
        }
    }
}