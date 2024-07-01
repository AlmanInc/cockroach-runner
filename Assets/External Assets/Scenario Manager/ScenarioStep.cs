using System;
using UnityEngine;

namespace Scenario
{
    public abstract class ScenarioStep : MonoBehaviour
    {
        public event Action OnFinish;

        public bool IsPlaying { get; protected set; }

        public virtual void Play() => IsPlaying = true;

        public virtual void FinishStep()
        {
            IsPlaying = false;
            OnFinish?.Invoke();
        }
    }
}