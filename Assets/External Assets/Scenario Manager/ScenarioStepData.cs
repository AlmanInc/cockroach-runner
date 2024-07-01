using System;

namespace Scenario
{
    [Serializable]
    public struct ScenarioStepData
    {        
        public float delay;
        public ScenarioStep step;
        public NextStepTransitionModes nextStepMode;
    }
}