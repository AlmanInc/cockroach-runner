using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine;
using Zenject;

namespace CockroachRunner
{
    public class ScreenViewLoading : BaseScreenView
    {
        [Space]
        [SerializeField] private WalletService walletService;
        [SerializeField] private JSJob jsJob;        
        [SerializeField] private ProgressBarView progressBar;
        [SerializeField] private float minimumDuration = 1f;

        [Space]
        [SerializeField] private Text labelUserName;
        [SerializeField] private Text labelLog;
        
        [Inject] private GameSettings gameSettings;

        private bool requestDone;
        private string response;
        
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
#if UNITY_WEBGL && !UNITY_EDITOR
            StartCoroutine(LoadApplicationProcess(false));
#else
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

                        
            yield return SendRequest(gameSettings.RequestCheckUser, gameSettings.LogRequests);            
            CheckUserResponseData checkUserData = JsonUtility.FromJson<CheckUserResponseData>(response);
            yield return ToProgressAnimationProcess(0.4f, 0.1f);

            if (checkUserData.exist)
            {
                yield return SendRequest(gameSettings.RequestUpdateUser, gameSettings.LogRequests);
                yield return ToProgressAnimationProcess(0.6f, 0.2f);
            }
            else
            {
                if (string.IsNullOrEmpty(PlayerData.OwnerRefId))
                {
                    // Не реферальная ссылка
                    yield return SendRequest(gameSettings.RequestAddUser, gameSettings.LogRequests);
                    yield return ToProgressAnimationProcess(0.6f, 0.2f);
                }
                else
                {
                    // Реферальная ссылка
                    yield return SendRequest(gameSettings.RequestAddUser, gameSettings.LogRequests);
                    yield return ToProgressAnimationProcess(0.5f, 0.1f);

                    yield return SendRequest(gameSettings.RequestAddReferalForUser, gameSettings.LogRequests);
                    yield return ToProgressAnimationProcess(0.6f, 0.1f);
                }
            }
            
            yield return SendRequest(gameSettings.RequestGetAllReferals, gameSettings.LogRequests);
            GetReferalsResponseData referalsData = JsonUtility.FromJson<GetReferalsResponseData>(response);
            PlayerData.Referals = referalsData.referals;
            yield return ToProgressAnimationProcess(0.7f, 0.1f);

            walletService.GetCurrency();
            yield return new WaitWhile(() => walletService.IsLocked);
            yield return ToProgressAnimationProcess(0.75f, 0.05f);

            yield return SendRequest(gameSettings.RequestGetUserTasks, gameSettings.LogRequests);
            GetTasksResponseData getTasksResponseData = JsonUtility.FromJson<GetTasksResponseData>(response);
            PlayerData.Tasks = getTasksResponseData.tasks;
            Debug.Log(response);
            yield return ToProgressAnimationProcess(0.8f, 0.05f);

            yield return RestProgressLoadingProcess(time);

            SceneManager.LoadSceneAsync("Menu");
        }

        private IEnumerator ShowErrorProcess(string message)
        {
            labelLog.text = message;
            labelLog.gameObject.SetActive(true);
            labelLog.enabled = true;

            while (true)
            {
                yield return null;
            }
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

            labelLog.text = $"id = ({PlayerData.Id})";
            
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

        private IEnumerator SendRequest(RequestData requestData, bool logRequest)
        {
            response = string.Empty;

            string request = ConfigureRequest(requestData.request);

            if (logRequest)
            {
                Debug.Log(request);
            }

            using (UnityWebRequest webRequest = UnityWebRequest.Get(request)) 
            {
                yield return webRequest.SendWebRequest();

                if (webRequest.result == UnityWebRequest.Result.Success)
                {
                    response = webRequest.downloadHandler.text;
                }
                else
                {
                    Debug.LogError(webRequest.error);
                    yield return ShowErrorProcess(webRequest.error);
                }
            }
        }

        

        private string ConfigureRequest(string request) 
        {
            string result = request.Replace("{server}", gameSettings.ServerName);
            result = result.Replace("{name}", PlayerData.Name);
            result = result.Replace("{id}", PlayerData.Id);
            result = result.Replace("{referal_id}", PlayerData.OwnerRefId);

            return result;
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