using UnityEngine.UI;
using UnityEngine;

namespace CockroachRunner
{
    public class ScreenViewTaskDetails : BaseScreenView
    {
        [Space]
        [SerializeField] private MenuGroupSwitcher menuGroupSwitcher;
        [SerializeField] private Button buttonBack;
        [SerializeField] private Button buttonDoTask;
        [SerializeField] private Button buttonCheckTask;

        public override void Activate()
        {
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
    }
}