using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using Zenject;
using Scenario;

namespace CockroachRunner
{
    public class StepPrepareRace : ScenarioStep
    {        
        [Header("Base Settings")]
        [SerializeField] private Text labelBackCount;

        [Inject] private GameSettings gameSettings;
        [Inject] private GameState gameState;
        [Inject] private GameScreenView gameScreenView;

        public override void Play()
        {
            base.Play();

            gameScreenView.OpenActualPanel(InGameViews.ReadySetGo);
            gameState.RaceTime = gameSettings.RaceTime;
            StartCoroutine(BackCountProcess());
        }

        private IEnumerator BackCountProcess()
        {
            float time = gameSettings.BackCountTimeToStartRace;
            
            while (time > 0f) 
            {
                labelBackCount.text = $"{Mathf.RoundToInt(time)}...";
                
                yield return new WaitForSeconds(1f);

                time -= 1f;
            }

            labelBackCount.text = $"{Mathf.RoundToInt(time)}...";
            FinishStep();
        }
    }
}