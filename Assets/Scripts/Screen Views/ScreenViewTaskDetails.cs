using UnityEngine.UI;
using UnityEngine;
using Zenject;
using System.Linq;

namespace CockroachRunner
{
    public class ScreenViewTaskDetails : BaseScreenView
    {
        [Space]
        [SerializeField] private MenuGroupSwitcher menuGroupSwitcher;
        [SerializeField] private Text labelCaption;
        [SerializeField] private Text labelDescription;
        [SerializeField] private Text labelReward;
        [SerializeField] private Button buttonBack;

        [Header("Bottom Elements")]
        [SerializeField] private RectTransform bottomButtonsPanel;
        [SerializeField] private float fullPanelSize;
        [SerializeField] private float shortPanelSize;
        [SerializeField] private Button buttonDoTask;
        [SerializeField] private Button buttonCheckTask;
        
        [Inject] private GameState gameState;

        public override void Activate()
        {
            TaskData task = PlayerData.Tasks.FirstOrDefault((t) => gameState.CurrentTaskId == t.task_id);
            
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
                    }
                    break;

                case TaskKinds.subscribe:
                    {
                        bottomButtonsPanel.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, fullPanelSize);
                        buttonCheckTask.gameObject.SetActive(true);
                    }
                    break;
            }
        }
    }
}