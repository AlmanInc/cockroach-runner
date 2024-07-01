using System.Collections;
using System;
using UnityEngine;

namespace Scenario
{
    public class ScenarioManager : BasePlayable
    {
        public event Action OnComplete;

        [Space]        
        [SerializeField] private bool logSteps;

        [Space]
        [SerializeField] private ScenarioStepData[] scenario;
                
        private int currentStepIndex;
        private bool isRunCurrentStep;

        public override void Play()
        {
            switch (state)
            {
                case ScenarioStates.Inactive:
                    currentStepIndex = 0;
                    PlayNextStep();
                    break;

                case ScenarioStates.Play:
                    PlayNextStep();
                    break;

                case ScenarioStates.Pause:
                    break;
            }
        }

        public override void Pause()
        {
            state = ScenarioStates.Pause;
        }

        public override void Stop()
        {
            state = ScenarioStates.Inactive;
        }
        
        private void OnDisable() => Stop();
        
        private void PlayNextStep()
        {
            if (0 <= currentStepIndex && currentStepIndex < scenario.Length)
            {
                if (logSteps)
                {
                    Debug.Log($"{gameObject.name}, step {currentStepIndex}");
                }

                StartCoroutine(PlayStepProcess(scenario[currentStepIndex]));
                currentStepIndex++;
            }
            else
            {
                OnComplete?.Invoke();
            }
        }

        private IEnumerator PlayStepProcess(ScenarioStepData data)
        {
            yield return new WaitForSeconds(data.delay);
            
            var step = data.step;

            switch (data.nextStepMode)
            {
                case NextStepTransitionModes.None:
                    step.Play();
                    break;

                case NextStepTransitionModes.PlayNextAfterStart:
                    step.Play();
                    PlayNextStep();
                    break;

                case NextStepTransitionModes.PlayNextWhenFinish:
                    isRunCurrentStep = true;
                    step.OnFinish += OnFinishCurrentStep;

                    step.Play();
                    yield return new WaitUntil(() => !isRunCurrentStep);
                    step.OnFinish -= OnFinishCurrentStep;

                    PlayNextStep();
                    break;
            }
        }
                
        private void OnFinishCurrentStep() => isRunCurrentStep = false;
    }
}