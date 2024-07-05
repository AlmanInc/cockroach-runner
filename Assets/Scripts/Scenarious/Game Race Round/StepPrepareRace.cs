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

        [Space]
        [SerializeField] private UnitMovable[] bots;
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
            List<int> treadmillIndexList = new List<int>();
            for (int i = 0; i < treadmills.Length; i++) 
            {
                treadmillIndexList.Add(i);
            }

            int listIndex = Random.Range(0, treadmillIndexList.Count - 1);
            int unitIndex = treadmillIndexList[listIndex];
            player.CachedTransform.position = treadmills[unitIndex].position;
            treadmillIndexList.RemoveAt(listIndex);

            Cockroach[] prefabs = gameSettings.CockroachPrefabs;
            Cockroach prefab = prefabs[Random.Range(0, prefabs.Length)];

            player.AddCockroach(Instantiate<Cockroach>(prefab));
            player.ShowName();

            for (int i = 0; i < bots.Length && treadmillIndexList.Count > 0; i++)
            {
                listIndex = Random.Range(0, treadmillIndexList.Count - 1);
                unitIndex = treadmillIndexList[listIndex];
                bots[i].CachedTransform.position = treadmills[unitIndex].position;
                treadmillIndexList.RemoveAt(listIndex);
                                
                prefab = prefabs[Random.Range(0, prefabs.Length)];
                bots[i].AddCockroach(Instantiate<Cockroach>(prefab));
            }
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