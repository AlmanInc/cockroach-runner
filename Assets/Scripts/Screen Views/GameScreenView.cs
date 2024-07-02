using System;
using UnityEngine;
using Scenario;

namespace CockroachRunner
{
    public class GameScreenView : MonoBehaviour
    {
        [Serializable]
        private struct PanelItem
        {
            public InGameViews kind;
            public GameObject panel;
        }

        [SerializeField] private ScenarioManager runRaceScenario;
        [SerializeField] private PanelItem[] items;

        private void Start() => runRaceScenario.Play();

        public void OpenActualPanel(InGameViews kind)
        {
            foreach (PanelItem item in items) 
            { 
                item.panel.SetActive(item.kind == kind);
            }
        }
    }
}