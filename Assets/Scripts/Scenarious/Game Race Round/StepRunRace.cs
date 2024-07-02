using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using Scenario;
using Zenject;

namespace CockroachRunner
{
    public class StepRunRace : ScenarioStep
    {
        [Header("Action Steps")]
        [SerializeField] private ScenarioStep prepareRaceStep;

        [Header("Base Settings")]
        [SerializeField] private PlayerMovable player;
        [SerializeField] private Text labelRaceTime;

        [Inject] private GameState gameState;

        public override void Play()
        {
            base.Play();

            SetRaceTimeLabel();
            prepareRaceStep.Play();
            player.Play();

            StartCoroutine(RaceTimeProcess());
        }

        private IEnumerator RaceTimeProcess()
        {
            while (gameState.RaceTime > 0f)
            {
                yield return new WaitForSeconds(1f);
                gameState.RaceTime -= 1;

                SetRaceTimeLabel();
            }

            player.Stop();

            FinishStep();
        }

        private void SetRaceTimeLabel()
        {
            int leftTime = gameState.RaceTime;

            int hours = (leftTime / 60) / 60;
            leftTime -= hours * 60 * 60;
            int minutes = leftTime / 60;
            leftTime -= minutes * 60;
            int seconds = leftTime;

            labelRaceTime.text = $"{GameUtility.NumberToStringWithLeadZero(hours)}:" +
                                 $"{GameUtility.NumberToStringWithLeadZero(minutes)}:" +
                                 $"{GameUtility.NumberToStringWithLeadZero(seconds)}";
        }
    }
}