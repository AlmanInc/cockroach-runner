using UnityEngine;
using Zenject;
using Scenario;

namespace CockroachRunner
{
    public class StepFinishRace : ScenarioStep
    {
        [Inject] private GameScreenView gameScreenView;

        public override void Play()
        {
            base.Play();

            gameScreenView.OpenActualPanel(InGameViews.Reward);

            FinishStep();
        }
    }
}