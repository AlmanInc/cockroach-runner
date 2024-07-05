using UnityEngine.UI;
using UnityEngine;
using Zenject;
using Scenario;

namespace CockroachRunner
{
    public class StepFinishRace : ScenarioStep
    {
        [Header("Base Settings")]
        [SerializeField] private GameObject bitcoinPanel;
        [SerializeField] private GraphView graphView;
        [SerializeField] private Text labelPlace;

        [Inject] private GameState gameState;
        [Inject] private GameScreenView gameScreenView;

        public override void Play()
        {
            base.Play();

            bitcoinPanel.SetActive(false);
            graphView.Clear();

            labelPlace.text = gameState.PlayerPlace.ToString();
            gameScreenView.OpenActualPanel(InGameViews.Reward);

            FinishStep();
        }
    }
}