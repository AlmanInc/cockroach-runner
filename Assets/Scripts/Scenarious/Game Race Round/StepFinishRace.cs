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
        [SerializeField] private Text labelResultTime;

        [Inject] private GameState gameState;
        [Inject] private GameScreenView gameScreenView;

        public override void Play()
        {
            base.Play();

            bitcoinPanel.SetActive(false);
            graphView.Clear();

            labelPlace.text = gameState.PlayerPlace.ToString();
            labelResultTime.text = GameUtility.SecondsToFullTimeStringFormat(gameState.RaceTime);
            gameScreenView.OpenActualPanel(InGameViews.Reward);

            FinishStep();
        }
    }
}