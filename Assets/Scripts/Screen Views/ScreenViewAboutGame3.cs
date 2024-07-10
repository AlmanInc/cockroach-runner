using UnityEngine.UI;
using UnityEngine;

namespace CockroachRunner
{
    public class ScreenViewAboutGame3 : BaseScreenView
    {
        [Space]
        [SerializeField] private MenuGroupSwitcher menuGroupSwitcher;
        [SerializeField] private Button buttonStart;

        public override void Activate()
        {
            base.Activate();

            buttonStart.onClick.AddListener(delegate
            {
                menuGroupSwitcher.ShowPanel(ScreenViews.MainMenu);
            });
        }

        public override void Deactivate()
        {
            base.Deactivate();

            buttonStart.onClick.RemoveAllListeners();
        }
    }
}