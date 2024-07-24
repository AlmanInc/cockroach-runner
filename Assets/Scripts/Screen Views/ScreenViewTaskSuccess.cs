using UnityEngine.UI;
using UnityEngine;

namespace CockroachRunner
{
    public class ScreenViewTaskSuccess : BaseScreenView
    {
        [Space]
        [SerializeField] private MenuGroupSwitcher menuGroupSwitcher;
        [SerializeField] private Button buttonFinish;

        public override void Activate()
        {
            base.Activate();

            buttonFinish.onClick.AddListener(delegate
            {
                menuGroupSwitcher.ShowPanel(ScreenViews.Tasks);
            });
        }

        public override void Deactivate()
        {
            base.Deactivate();

            buttonFinish.onClick.RemoveAllListeners();
        }
    }
}