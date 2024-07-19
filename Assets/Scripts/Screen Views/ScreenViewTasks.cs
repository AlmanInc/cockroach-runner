using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Zenject;

namespace CockroachRunner
{
    public class ScreenViewTasks : BaseScreenView
    {
        [Space]
        [SerializeField] private MenuGroupSwitcher menuGroupSwitcher;
        [SerializeField] private Transform taskContainer;
        [SerializeField] private Button buttonBack;
        [SerializeField] private Button buttonTabReferal;

        [Inject] DiContainer container;
        [Inject] private GameSettings gameSettings;
        [Inject] private EventsManager eventsManager;

        private List<TaskLineView> tasks;

        public override void Activate()
        {            
            base.Activate();
                        
            buttonBack.onClick.AddListener(delegate
            {
                menuGroupSwitcher.ShowPanel(ScreenViews.MainMenu);
            });
            
            buttonTabReferal.onClick.AddListener(delegate
            {
                menuGroupSwitcher.ShowPanel(ScreenViews.Referals);
            });

            eventsManager.AddListener(GameEvents.TryOpenTaskDetails, OpenTaskDetails);

            GenerateTaskList();            
        }

        public override void Deactivate()
        {
            buttonBack.onClick.RemoveAllListeners();
            buttonTabReferal.onClick.RemoveAllListeners();

            eventsManager.RemoveListener(GameEvents.TryOpenTaskDetails, OpenTaskDetails);

            ClearTaskList();

            base.Deactivate();
        }

        private void OpenTaskDetails(params object[] args)
        {
            menuGroupSwitcher.ShowPanel(ScreenViews.TaskDetails);
        }

        private void GenerateTaskList()
        {
            if (tasks == null)
            {
                tasks = new List<TaskLineView>();
            }

            ClearTaskList();

            TaskLineView taskLine;
            for (int i = 0; i < PlayerData.Tasks.Length; i++)
            {                
                if (i == 0)
                {
                    taskLine = container.InstantiatePrefab(gameSettings.TaskLineTop, taskContainer).GetComponent<TaskLineView>();
                }
                else if (i == PlayerData.Tasks.Length - 1)
                {
                    taskLine = container.InstantiatePrefab(gameSettings.TaskLineBottom, taskContainer).GetComponent<TaskLineView>();
                }
                else
                {
                    taskLine = container.InstantiatePrefab(gameSettings.TaskLineBase, taskContainer).GetComponent<TaskLineView>();
                }

                tasks.Add(taskLine);
                tasks[i].SetViewState(PlayerData.Tasks[i]);
            }
        }

        private void ClearTaskList()
        {
            try
            {
                for (int i = 0; i < tasks.Count; i++)
                {
                    Destroy(tasks[i].gameObject);
                }

                tasks.Clear();
            }
            catch 
            {
                Debug.Log("Couldn't clear task list");
            }
        }
    }
}