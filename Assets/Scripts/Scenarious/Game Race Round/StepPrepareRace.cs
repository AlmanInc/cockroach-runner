using System.Collections.Generic;
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
        [SerializeField] private GameObject bitcoinPanel;

        [Space]
        [SerializeField] private UnitMovable player;
        [SerializeField] private Transform[] treadmills;

        [Inject] private GameSettings gameSettings;
        [Inject] private GameState gameState;
        [Inject] private GameScreenView gameScreenView;

        public override void Play()
        {
            base.Play();

            gameScreenView.OpenActualPanel(InGameViews.ReadySetGo);
            bitcoinPanel.SetActive(true);

            gameState.RaceTime = gameSettings.RaceTime;

            GenerateTreadmealsAndCockroaches();
            
            StartCoroutine(BackCountProcess());
        }

        private void GenerateTreadmealsAndCockroaches()
        {
            int playerTreadmillIndex = Random.Range(0, treadmills.Length - 1);
            player.CachedTransform.position = treadmills[playerTreadmillIndex].position;

            var prefabs = gameSettings.CockroachPrefabs;
            var prefab = prefabs[Random.Range(0, prefabs.Length - 1)];

            player.AddCockroach(Instantiate(prefab).GetComponent<Transform>());
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