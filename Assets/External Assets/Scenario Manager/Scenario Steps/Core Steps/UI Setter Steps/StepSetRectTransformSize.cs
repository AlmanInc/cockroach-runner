using UnityEngine;

namespace Scenario
{
    public class StepSetRectTransformSize : ScenarioStep
    {
        [SerializeField] private RectTransform target;
        [SerializeField] private Vector2 size;
        
        public override void Play()
        {
            base.Play();

            target.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size.x);
            target.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size.y);

            FinishStep();
        }
    }
}