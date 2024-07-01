using UnityEngine;
using Scenario;

namespace CockroachRunner
{
    public class StepRunRace : ScenarioStep
    {
        public override void Play()
        {
            base.Play();
            FinishStep();
        }
    }
}