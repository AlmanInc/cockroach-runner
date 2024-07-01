using UnityEngine;

namespace Scenario
{
    public class StepSetScale : ScenarioStep
    {
        [SerializeField] private Transform target;
        [SerializeField] private float scale;
        
        public override void Play()
        {
            base.Play();

            target.transform.localScale = Vector3.one * scale;
            FinishStep();
        }
    }
}