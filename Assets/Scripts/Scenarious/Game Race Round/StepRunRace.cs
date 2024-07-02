using UnityEngine;
using Scenario;

namespace CockroachRunner
{
    public class StepRunRace : ScenarioStep
    {
        [Header("Action Steps")]
        [SerializeField] private ScenarioStep prepareRaceStep;

        [Header("Base Settings")]
        [SerializeField] private PlayerMovable player;

        public override void Play()
        {
            base.Play();

            prepareRaceStep.Play();
            player.Play();
        }
    }
}