using UnityEngine.UI;
using UnityEngine;

namespace CockroachRunner
{
    public class ScreenViewAboutGame1 : BaseScreenView
    {
        [Space]
        [SerializeField] private MenuGroupSwitcher menuGroupSwitcher;
        [SerializeField] private Button buttonNext;

        public override void Activate()
        {
            base.Activate();
                        
            buttonNext.onClick.AddListener(delegate
            {
                menuGroupSwitcher.ShowPanel(ScreenViews.AboutGame2);
            });
        }

        public override void Deactivate()
        {
            base.Deactivate();

            buttonNext.onClick.RemoveAllListeners();
        }
    }
}