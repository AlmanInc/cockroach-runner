using UnityEngine;

namespace Scenario
{
    public abstract class BasePlayable : MonoBehaviour
    {
        protected ScenarioStates state = ScenarioStates.Inactive;

        public abstract void Play();

        public abstract void Pause();

        public abstract void Stop();
    }
}