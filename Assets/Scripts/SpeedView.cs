using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using Zenject;

namespace CockroachRunner
{
    public class SpeedView : MonoBehaviour
    {
        [SerializeField] private Image speedLine;
        [SerializeField] private Text speedLabel;
        [SerializeField] private float amountOffset = 0f;
        [SerializeField] private float toFillAmountOffsetDuration = 0.25f;

        [Inject] private GameSettings gameSettings;
        [Inject] private EventsManager eventsManager;

        private Coroutine activateCoroutine;

        public void Activate()
        {
            eventsManager.AddListener(GameEvents.PlayerSetSpeed, SetPlayerSpeed);

            if (activateCoroutine != null)
            {
                StopCoroutine(activateCoroutine);
            }

            activateCoroutine = StartCoroutine(ActivateProcess());
        }

        public void Deactivate()
        {
            eventsManager.RemoveListener(GameEvents.PlayerSetSpeed, SetPlayerSpeed);

            if (activateCoroutine != null)
            {
                StopCoroutine(activateCoroutine);
                activateCoroutine = null;
            }
        }

        private void OnDisable()
        {
            eventsManager.RemoveListener(GameEvents.PlayerSetSpeed, SetPlayerSpeed);
        }

        private void SetPlayerSpeed(params object[] args)
        {
            float speed = (float)args[0];
            float progress = Mathf.Clamp01(speed/gameSettings.FastRunningSpeed);
            float fillAmountOffset = Mathf.Lerp(amountOffset, 0f, progress);
            
            speedLine.fillAmount = Mathf.Clamp01(progress + fillAmountOffset);
            float speedForLabel = Mathf.Lerp(0f, gameSettings.FastRunningSpeedLabelValue, progress);
            speedLabel.text = string.Format("{0:0.0} cm/s", speedForLabel);

        }

        private IEnumerator ActivateProcess()
        {
            float time = 0f;

            while (time < toFillAmountOffsetDuration)
            {
                float progress = Mathf.Clamp01(time / toFillAmountOffsetDuration);
                speedLine.fillAmount = Mathf.Lerp(0f, amountOffset, progress);

                yield return null;

                time += Time.deltaTime;
            }

            speedLine.fillAmount = amountOffset;
        }
    }
}