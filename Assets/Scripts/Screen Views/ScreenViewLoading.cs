using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using Zenject;

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

        [Inject] private GameSettings gameSettings;
        
        private float progress;
        private float Progress
        {
            get => progress;
            set
            {
                progress = value;
                progressBar.SetProgress(progress);
            }
        }

        private void Start()
        {
            //Progress = 0f;

#if UNITY_WEBGL && !UNITY_EDITOR
            //StartCoroutine(LoadWebProcess());
            StartCoroutine(LoadApplicationProcess(false));
#else
            //StartCoroutine(LoadUnityProcess());
            StartCoroutine(LoadApplicationProcess(true));
#endif            
        }

        private IEnumerator LoadApplicationProcess(bool isUnityLoading)
        {
            labelLog.text = isUnityLoading ? "Unity" : "Web App";
            float time = Time.time;
            Progress = 0f;

            yield return new WaitForEndOfFrame();

            yield return GetUserNameProcess(0.1f, 0.1f, isUnityLoading);
            yield return GetUserIdProcess(0.2f, 0.1f, isUnityLoading);
            yield return GetOwnerRefIdForCurrentUser(0.3f, 0.1f, isUnityLoading);

            yield return RestProgressLoadingProcess(time);

            SceneManager.LoadSceneAsync("Menu");
        }

        private IEnumerator GetUserNameProcess(float partOfProgress, float toProgressDuration, bool isUnityLoading) 
        {
            yield return new WaitForEndOfFrame();

            if (isUnityLoading)
            {
                PlayerData.Name = gameSettings.DefaultUserName;
            }
            else
            {
                jsJob.TryGetUserName();
                PlayerData.Name = jsJob.UserName;
            }
            
            labelUserName.text = PlayerData.Name;

            yield return ToProgressAnimationProcess(partOfProgress, toProgressDuration);
        }

        private IEnumerator GetUserIdProcess(float partOfProgress, float toProgressDuration, bool isUnityLoading)
        {
            yield return new WaitForEndOfFrame();

            if (isUnityLoading)
            {
                PlayerData.Id = gameSettings.DefaultUserId;
            }
            else
            {
                jsJob.TryGetUserId();
                PlayerData.Id = jsJob.UserId;
            }
            
            yield return ToProgressAnimationProcess(partOfProgress, toProgressDuration);
        }

        private IEnumerator GetOwnerRefIdForCurrentUser(float partOfProgress, float toProgressDuration, bool isUnityLoading)
        {
            yield return new WaitForEndOfFrame();

            if (isUnityLoading) 
            {
                PlayerData.OwnerRefId = string.Empty;
            }
            else
            {
                jsJob.TryGetUserRefId();
                PlayerData.OwnerRefId = jsJob.UserRefId;
            }

            yield return ToProgressAnimationProcess(partOfProgress, toProgressDuration);
        }




        private IEnumerator LoadUnityProcess()
        {
            labelLog.text = "Unity";

            float time = Time.time;

            PlayerData.Name = gameSettings.DefaultUserName;
            PlayerData.Id = gameSettings.DefaultUserId;
            yield return ToProgressAnimationProcess(0.2f, 0.1f);         

            yield return RestProgressLoadingProcess(time);
                        
            SceneManager.LoadSceneAsync("Menu");
        }

        private IEnumerator LoadWebProcess()
        {
            labelLog.text = "Web App";
            
            float time = Time.time;

            // Get User Name
            yield return new WaitForEndOfFrame();
            jsJob.TryGetUserName();
            yield return new WaitForEndOfFrame();
            
            PlayerData.Name = jsJob.UserName;
            labelUserName.text = PlayerData.Name;
            Progress = 0.1f;


            // Get User Id
            yield return new WaitForEndOfFrame();
            jsJob.TryGetUserId();
            yield return new WaitForEndOfFrame();

            PlayerData.Id = jsJob.UserId;                        
            Progress = 0.2f;


            // Check loading application link as referal link
            yield return new WaitForEndOfFrame();
            jsJob.TryGetUserRefId();
            yield return new WaitForEndOfFrame();

            PlayerData.OwnerRefId = jsJob.UserRefId;
            Progress = 0.3f;


            // Rest Loading
            yield return RestProgressLoadingProcess(time);

            SceneManager.LoadSceneAsync("Menu");
        }

        private IEnumerator ToProgressAnimationProcess(float progressTarget, float duration)
        {
            float startProgress = Progress;
            float time = 0f;

            while (time < duration)
            {
                yield return null;

                time += Time.deltaTime;
                Progress = Mathf.Lerp(startProgress, progressTarget, Mathf.Clamp01(time / duration));
            }

            Progress = progressTarget;
        }

        private IEnumerator RestProgressLoadingProcess(float time)
        {
            float deltaTime = Time.time - time;

            if (minimumDuration > deltaTime)
            {
                float leftTime = minimumDuration - deltaTime;
                time = 0f;
                float startProgress = Progress;

                while (time < leftTime)
                {
                    yield return null;

                    time += Time.deltaTime;
                    Progress = Mathf.Lerp(startProgress, 1f, Mathf.Clamp01(time / leftTime));
                }
            }

            Progress = 1f;
            yield return new WaitForEndOfFrame();
        }
    }
}