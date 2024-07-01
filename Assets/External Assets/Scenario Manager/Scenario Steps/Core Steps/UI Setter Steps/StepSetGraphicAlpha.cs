using UnityEngine.UI;
using UnityEngine;

namespace Scenario
{
    public class StepSetGraphicAlpha : ScenarioStep
    {
        [SerializeField] private MaskableGraphic target;
        [SerializeField][Range(0f, 1f)] private float targetTransparent;

        public override void Play()
        {
            base.Play();

            Color color = target.color;
            color.a = targetTransparent;
            target.color = color;

            FinishStep();
        }
    }
}