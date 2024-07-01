using UnityEngine;
using Scenario;

namespace CockroachRunner
{
    public class StepFinishRace : ScenarioStep
    {
        public override void Play()
        {
            base.Play();
            FinishStep();
        }
    }
}