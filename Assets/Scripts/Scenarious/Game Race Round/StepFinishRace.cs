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
        [SerializeField] private Text laberReward;

        [Inject] private EventsManager eventsManager;
        [Inject] private GameSettings gameSettings;
        [Inject] private GameState gameState;
        [Inject] private GameScreenView gameScreenView;

        public override void Play()
        {
            base.Play();

            bitcoinPanel.SetActive(false);
            graphView.Clear();

            labelPlace.text = gameState.PlayerPlace.ToString();
            labelResultTime.text = GameUtility.SecondsToFullTimeStringFormat(gameState.RaceTime);
            laberReward.text = gameState.PlayerPlace == 1 ? gameSettings.RaceBet.ToString() : "0";
            gameScreenView.OpenActualPanel(InGameViews.Reward);

            if (gameState.PlayerPlace == 1)
            {
                eventsManager.InvokeEvent(GameEvents.AddCurrency, gameSettings.RaceBet + gameSettings.RaceBet);
            }

            FinishStep();
        }
    }
}