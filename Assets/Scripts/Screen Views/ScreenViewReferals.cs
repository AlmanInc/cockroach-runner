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
        }

        public override void Deactivate()
        {
            base.Deactivate();

            buttonBack.onClick.RemoveAllListeners();
            buttonTabTasks.onClick.RemoveAllListeners();
        }
    }
}