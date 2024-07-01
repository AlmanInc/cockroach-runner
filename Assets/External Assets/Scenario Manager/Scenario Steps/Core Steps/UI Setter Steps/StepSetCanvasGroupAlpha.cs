using UnityEngine.UI;
using UnityEngine;

namespace Scenario
{
    public class StepSetCanvasGroupAlpha : ScenarioStep
    {
        [SerializeField] private CanvasGroup target;
        [SerializeField][Range(0f, 1f)] private float targetTransparent;

        public override void Play()
        {
            base.Play();

            target.alpha = targetTransparent;
            FinishStep();
        }
    }
}