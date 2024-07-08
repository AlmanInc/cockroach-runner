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
        [Header("Action Steps")]
        [SerializeField] private ScenarioStep pulseStep;

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
            Cockroach[] prefabs = gameSettings.CockroachPrefabs;

            ArrayUniqueIndexGrabber treadmillsGrabber = new ArrayUniqueIndexGrabber();
            treadmillsGrabber.Activate(treadmills.Length);

            ArrayUniqueIndexGrabber cockroachGrabber = new ArrayUniqueIndexGrabber();
            cockroachGrabber.Activate(prefabs.Length);
            
            int unitIndex = treadmillsGrabber.NexIndex();
            player.CachedTransform.position = treadmills[unitIndex].position;
            
            Cockroach prefab = prefabs[cockroachGrabber.NexIndex()];

            player.AddCockroach(Instantiate<Cockroach>(prefab));
            player.ShowName();
                        
            for (int i = 0; i < bots.Length; i++)
            {
                unitIndex = treadmillsGrabber.NexIndex();

                if (unitIndex < 0)
                {
                    break;
                }

                bots[i].CachedTransform.position = treadmills[unitIndex].position;

                prefab = prefabs[cockroachGrabber.NexIndex()];
                bots[i].AddCockroach(Instantiate<Cockroach>(prefab));
            }

            treadmillsGrabber.Clear();
            cockroachGrabber.Clear();
        }

        private IEnumerator BackCountProcess()
        {
            float time = gameSettings.BackCountTimeToStartRace;
            
            while (time > 0f) 
            {
                labelBackCount.text = $"{Mathf.RoundToInt(time)}";
                pulseStep?.Play();
                
                yield return new WaitForSeconds(1f);

                time -= 1f;
            }

            labelBackCount.text = $"{Mathf.RoundToInt(time)}";
            FinishStep();
        }
    }
}