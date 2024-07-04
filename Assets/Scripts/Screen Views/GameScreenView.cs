using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
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
        [SerializeField] private Button buttonMenu;
        [SerializeField] private PanelItem[] items;

        private void OnEnable() => buttonMenu?.onClick.AddListener(OpenMenu);

        private void OnDisable() => buttonMenu?.onClick.RemoveListener(OpenMenu);
                
        private void Start() => runRaceScenario.Play();

        public void OpenActualPanel(InGameViews kind)
        {
            foreach (PanelItem item in items) 
            { 
                item.panel.SetActive(item.kind == kind);
            }
        }

        private void OpenMenu()
        {
            SceneManager.LoadSceneAsync("Menu");
        }
    }
}