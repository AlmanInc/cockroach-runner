using UnityEngine.UI;
using UnityEngine;

namespace CockroachRunner
{
    public class ScreenViewTasks : BaseScreenView
    {
        [Space]
        [SerializeField] private MenuGroupSwitcher menuGroupSwitcher;
        [SerializeField] private Button buttonBack;
        [SerializeField] private Button buttonTabReferal;

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
        }

        public override void Deactivate()
        {
            base.Deactivate();

            buttonBack.onClick.RemoveAllListeners();
            buttonTabReferal.onClick.RemoveAllListeners();
        }
    }
}