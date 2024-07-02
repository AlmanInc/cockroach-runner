using UnityEngine;
using Scenario;

namespace CockroachRunner
{
    public class GameScreenView : MonoBehaviour
    {
        [SerializeField] private ScenarioManager runRaceScenario;

        private void Start() => runRaceScenario.Play();
    }
}