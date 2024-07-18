using UnityEngine.UI;
using UnityEngine;
using Zenject;

namespace CockroachRunner
{
    public class ScreenViewTasks : BaseScreenView
    {
        [Space]
        [SerializeField] private MenuGroupSwitcher menuGroupSwitcher;
        [SerializeField] private Button buttonBack;
        [SerializeField] private Button buttonTabReferal;

        [Inject] private EventsManager eventsManager;

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
        }

        public override void Deactivate()
        {
            buttonBack.onClick.RemoveAllListeners();
            buttonTabReferal.onClick.RemoveAllListeners();

            eventsManager.RemoveListener(GameEvents.TryOpenTaskDetails, OpenTaskDetails);

            base.Deactivate();
        }

        private void OpenTaskDetails(params object[] args)
        {
            menuGroupSwitcher.ShowPanel(ScreenViews.TaskDetails);
        }
    }
}