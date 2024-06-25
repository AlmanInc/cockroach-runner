using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

namespace CockroachRunner
{
    public class MenuScreenView : MonoBehaviour
    {
        [SerializeField] private Button buttonRace;

        private void OnEnable() => buttonRace.onClick.AddListener(StartRace);

        private void OnDisable() => buttonRace.onClick.RemoveListener(StartRace);

        private void StartRace() => SceneManager.LoadSceneAsync("Game");
    }
}