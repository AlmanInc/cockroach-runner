using UnityEngine.UI;
using UnityEngine;

namespace CockroachRunner
{
    public class ScreenViewAboutGame2 : BaseScreenView
    {
        [Space]
        [SerializeField] private MenuGroupSwitcher menuGroupSwitcher;
        [SerializeField] private Button buttonNext;

        public override void Activate()
        {
            base.Activate();

            buttonNext.onClick.AddListener(delegate
            {
                menuGroupSwitcher.ShowPanel(ScreenViews.AboutGame3);
            });
        }

        public override void Deactivate()
        {
            base.Deactivate();

            buttonNext.onClick.RemoveAllListeners();
        }
    }
}