using UnityEngine.UI;
using UnityEngine;

namespace CockroachRunner
{
    public class ScreenViewReferals : BaseScreenView
    {
        [Space]
        [SerializeField] private MenuGroupSwitcher menuGroupSwitcher;
        [SerializeField] private Button buttonBack;
        [SerializeField] private Button buttonTabTasks;

        [Space]        
        [SerializeField] private Text laberTotalReferals;
        [SerializeField] private int totalReferals;
        [SerializeField] private ScrollContentController scrollContentController;

        public override void Activate()
        {
            base.Activate();

            buttonBack.onClick.AddListener(delegate
            {
                menuGroupSwitcher.ShowPanel(ScreenViews.MainMenu);
            });

            buttonTabTasks.onClick.AddListener(delegate
            {
                menuGroupSwitcher.ShowPanel(ScreenViews.Tasks);
            });

            laberTotalReferals.text = totalReferals.ToString();
            scrollContentController.SetMaxReferalCount(totalReferals);
        }

        public override void Deactivate()
        {
            base.Deactivate();

            buttonBack.onClick.RemoveAllListeners();
            buttonTabTasks.onClick.RemoveAllListeners();
        }
    }
}