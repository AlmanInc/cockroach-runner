using System.Collections;
using System.Linq;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine;
using Zenject;
using System;

namespace CockroachRunner
{
    public class ScreenViewTaskDetails : BaseScreenView
    {
        [Space]
        [SerializeField] private MenuGroupSwitcher menuGroupSwitcher;
        [SerializeField] private Text labelCaption;
        [SerializeField] private Text labelDescription;
        [SerializeField] private Text labelReward;
        [SerializeField] private GameObject locker;
        [SerializeField] private Button buttonBack;

        [Header("Bottom Elements")]
        [SerializeField] private RectTransform bottomButtonsPanel;
        [SerializeField] private float fullPanelSize;
        [SerializeField] private float shortPanelSize;
        [SerializeField] private Button buttonDoTask;
        [SerializeField] private Button buttonCheckTask;

        [Inject] private EventsManager eventsManager;
        [Inject] private GameState gameState;
        [Inject] private GameSettings gameSettings;

        private TaskData task;
        private string response;

        public override void Activate()
        {
            task = PlayerData.Tasks.FirstOrDefault((t) => gameState.CurrentTaskId == t.task_id);
            
            if (task != null) 
            {
                labelCaption.text = task.name;
                labelDescription.text = task.description;
                labelReward.text = task.cost.ToString();

                ConfigureBottomButtonsPanel(task);
            }
            else
            {
                Debug.LogError($"Try to open incorrect task with id {gameState.CurrentTaskId}");
            }

            base.Activate();

            buttonBack.onClick.AddListener(delegate
            {
                menuGroupSwitcher.ShowPanel(ScreenViews.Tasks);
            });
        }

        public override void Deactivate()
        {
            buttonBack.onClick.RemoveAllListeners();
            buttonDoTask.onClick.RemoveAllListeners();
            buttonCheckTask.onClick.RemoveAllListeners();

            base.Deactivate();
        }

        private void ConfigureBottomButtonsPanel(TaskData task)
        {
            switch (task.Kind)
            {
                case TaskKinds.daily:
                    {
                        bottomButtonsPanel.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, shortPanelSize);
                        buttonCheckTask.gameObject.SetActive(false);

                        buttonDoTask.onClick.AddListener(CheckTask);
                    }
                    break;

                case TaskKinds.subscribe:
                    {
                        bottomButtonsPanel.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, fullPanelSize);
                        buttonCheckTask.gameObject.SetActive(true);

                        buttonDoTask.onClick.AddListener(delegate
                        {
                            Application.OpenURL(task.param);
                        });

                        buttonCheckTask.onClick.AddListener(CheckTask);
                    }
                    break;
            }
        }

        private void CheckTask()
        {
            StartCoroutine(CheckTaskProcess());
        }

        private IEnumerator CheckTaskProcess()
        {
            locker.SetActive(true);

            yield return SendRequest(gameSettings.RequestCheckTask, task, gameSettings.LogRequests);
            if (!string.IsNullOrEmpty(response))
            {
                StatusResponse statusData = JsonUtility.FromJson<StatusResponse>(response);

                if (statusData.status)
                {
                    // Refresh tasks
                    yield return SendRequest(gameSettings.RequestGetUserTasks, task, gameSettings.LogRequests);
                    GetTasksResponseData getTasksResponseData = JsonUtility.FromJson<GetTasksResponseData>(response);
                    PlayerData.Tasks = getTasksResponseData.tasks;
                    foreach (var item in PlayerData.Tasks)
                    {
                        item.Kind = (TaskKinds)Enum.Parse(typeof(TaskKinds), item.type);
                    }
                }

                eventsManager.InvokeEvent(GameEvents.AddCurrency, 0);


                locker.SetActive(false);
                menuGroupSwitcher.ShowPanel(statusData.status ? ScreenViews.TaskDoneSuccessfully : ScreenViews.TaskDoneUnsuccessfully);
            }

            locker.SetActive(false);
        }

        private string ConfigureRequestString(TaskData taskData, string request)
        {
            string result = request.Replace("{server}", gameSettings.ServerName);
            result = result.Replace("{name}", PlayerData.Name);
            result = result.Replace("{id}", PlayerData.Id);
            result = result.Replace("{referal_id}", PlayerData.OwnerRefId);
            result = result.Replace("{task_id}", taskData.task_id);

            return result;
        }

        private IEnumerator SendRequest(RequestData requestData, TaskData task, bool logRequest)
        {
            response = string.Empty;

            string request = ConfigureRequestString(task, requestData.request);

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
                    if (requestData.showResponse)
                    {
                        Debug.Log(response);
                    }
                }
                else
                {
                    Debug.LogError(webRequest.error);                    
                    //yield return ShowErrorProcess(webRequest.error);
                    yield return null;
                }
            }
        }
    }
}