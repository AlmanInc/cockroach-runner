using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using Scenario;
using Zenject;

namespace CockroachRunner
{
    public class StepPrepareRace : ScenarioStep
    {
        [Header("Action Steps")]
        [SerializeField] private ScenarioStep prepareSceneStep;

        [Header("Base Settings")]
        [SerializeField] private Text labelBackCount;

        [Inject] private GameSettings gameSettings;

        public override void Play()
        {
            base.Play();

            prepareSceneStep.Play();
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