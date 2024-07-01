using UnityEngine;

namespace Scenario
{
    public class StepSetObjectsActivity : ScenarioStep
    {
        [Space]
        [SerializeField] private VisibilityKinds operation;
        [SerializeField] private GameObject[] targets;
                
        public override void Play()
        {
            base.Play();

            foreach (GameObject item in targets)
            {
                item.SetActive(operation == VisibilityKinds.Show);
            }

            FinishStep();
        }
    }
}