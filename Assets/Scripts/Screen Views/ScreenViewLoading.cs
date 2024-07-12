using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

namespace CockroachRunner
{
    public class ScreenViewLoading : BaseScreenView
    {
        [Space]
        [SerializeField] private JSJob jsJob;        
        [SerializeField] private ProgressBarView progressBar;
        [SerializeField] private float minimumDuration = 1f;

        [Space]
        [SerializeField] private Text labelUserName;
        [SerializeField] private Text labelLog;

        private void Start()
        {
            progressBar.SetProgress(0f);

            
#if UNITY_WEBGL && !UNITY_EDITOR
            StartCoroutine(LoadWebProcess());
#else
            StartCoroutine(LoadUnityProcess());
#endif            
        }

        public void GetNameClick()
        {
            labelUserName.text = "Try to get name...";
            jsJob.GetTelegramUserName();
            labelUserName.text = jsJob.UserName;
        }

        public void PlayClick()
        {
            StartCoroutine(LoadUnityProcess());
        }

        private IEnumerator LoadUnityProcess()
        {
            labelLog.text = "Unity Loading";

            float time = 0f;

            while (time < minimumDuration) 
            {
                yield return null;

                time += Time.deltaTime;
                progressBar.SetProgress(Mathf.Clamp01(time / minimumDuration));
            }

            progressBar.SetProgress(1f);

            yield return new WaitForEndOfFrame();

            SceneManager.LoadSceneAsync("Menu");
        }

        private IEnumerator LoadWebProcess()
        {
            labelLog.text = "Web Telegram Loading";

            float progress = 0f;
            float time = Time.time;

            yield return new WaitForEndOfFrame();
            jsJob.GetTelegramUserName();
            yield return new WaitForEndOfFrame();
                        
            PlayerData.Name = jsJob.UserName;
            labelUserName.text = PlayerData.Name;

            progress = 0.25f;
            progressBar.SetProgress(progress);

            yield return new WaitForEndOfFrame();
            jsJob.GetTelegramUserId();
            yield return new WaitForEndOfFrame();

            PlayerData.Id = jsJob.UserId;

            progress = 0.5f;
            progressBar.SetProgress(progress);

            float deltaTime = Time.time - time;

            if (minimumDuration > deltaTime)
            {
                float leftTime = minimumDuration - deltaTime;
                time = 0f;

                while (time < leftTime)
                {
                    yield return null;

                    time += Time.deltaTime;
                    progressBar.SetProgress(Mathf.Lerp(progress, 1f, Mathf.Clamp01(time / leftTime)));
                }
            }

            progressBar.SetProgress(1f);
            yield return new WaitForEndOfFrame();

            SceneManager.LoadSceneAsync("Menu");
        }
    }
}