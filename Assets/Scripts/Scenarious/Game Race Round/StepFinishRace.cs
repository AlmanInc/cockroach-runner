using UnityEngine;
using Zenject;
using Scenario;

namespace CockroachRunner
{
    public class StepFinishRace : ScenarioStep
    {
        [Header("Base Settings")]
        [SerializeField] private GameObject bitcoinPanel;

        [Inject] private GameScreenView gameScreenView;

        public override void Play()
        {
            base.Play();

            bitcoinPanel.SetActive(false);
            gameScreenView.OpenActualPanel(InGameViews.Reward);

            FinishStep();
        }
    }
}