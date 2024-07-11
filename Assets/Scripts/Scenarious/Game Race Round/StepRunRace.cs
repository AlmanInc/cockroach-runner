using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using Scenario;
using Zenject;
using System;

namespace CockroachRunner
{
    public class StepRunRace : ScenarioStep
    {
        [Header("Base Settings")]
        [SerializeField] private UnitMovable[] units;
        [SerializeField] private UnitMovable player;
        [SerializeField] private Text labelRaceTime;
        [SerializeField] private GameObject blockPanel;
        [SerializeField] private Button buttonUp;
        [SerializeField] private Button buttonDown;

        [Inject] private EventsManager eventsManager;
        [Inject] private GameState gameState;
        [Inject] private GameScreenView gameScreenView;

        private Coroutine raceCoroutine;

        private void OnEnable()
        {
            buttonUp?.onClick.AddListener(UpClick);
            buttonDown?.onClick.AddListener(DownClick);
        }

        private void OnDisable()
        {
            buttonUp?.onClick.RemoveListener(UpClick);
            buttonDown?.onClick.RemoveListener(DownClick);
        }

        public override void Play()
        {
            base.Play();
                        
            SetRaceTimeLabel();

            gameScreenView.OpenActualPanel(InGameViews.Game);

            foreach (var unit in units) 
            {
                unit.Play();
            }

            eventsManager.AddListener(GameEvents.UnitFinishedRace, UnitFinishRace);

            if (raceCoroutine != null)
            {
                StopCoroutine(raceCoroutine);
            }
            StartCoroutine(RaceTimeProcess());
        }

        public override void FinishStep()
        {
            if (raceCoroutine != null)
            {
                StopCoroutine(raceCoroutine);
                raceCoroutine = null;
            }

            eventsManager.RemoveListener(GameEvents.UnitFinishedRace, UnitFinishRace);
            base.FinishStep();
        }

        private IEnumerator RaceTimeProcess()
        {            
            while (true)
            {
                yield return new WaitForSeconds(1f);
                gameState.RaceTime++;

                SetRaceTimeLabel();
            }
        }

        private void UnitFinishRace(object[] args)
        {
            UnitMovable unit = (UnitMovable)args[0];

            if (unit == null)
            {
                Debug.LogError("Incorrect argument: StepRunRace:UnitFinishRace");
                return;
            }

            unit.Stop();

            if (unit.IsPlayer)
            {
                gameState.PlayerPlace = gameState.FinishedOpponentsCount + 1;
                FinishStep();
            }
            else
            {
                gameState.FinishedOpponentsCount++;                
            }
        }

        private void SetRaceTimeLabel() => labelRaceTime.text = GameUtility.SecondsToFullTimeStringFormat(gameState.RaceTime);            
        
        private void UpClick()
        {
            blockPanel.SetActive(true);
            player.MakePrediction(true);
        }

        private void DownClick()
        {
            blockPanel.SetActive(true);
            player.MakePrediction(false);
        } 
    }
}