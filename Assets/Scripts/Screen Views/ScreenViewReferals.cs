using UnityEngine.UI;
using UnityEngine;
using Zenject;

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

        [Space]
        [SerializeField] private Text labelReferalLink;
        [SerializeField] private Button buttonCopyLink;

        [Inject] private GameSettings gameSettings;

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

            buttonCopyLink.onClick.AddListener(delegate
            {
                GUIUtility.systemCopyBuffer = labelReferalLink.text;
            });

            laberTotalReferals.text = totalReferals.ToString();
            scrollContentController.SetMaxReferalCount(totalReferals);

            string referalLink = gameSettings.ReferalLink;
            referalLink = referalLink.Replace("{id}", PlayerData.Id);
            labelReferalLink.text = referalLink;
        }

        public override void Deactivate()
        {
            base.Deactivate();

            buttonBack.onClick.RemoveAllListeners();
            buttonTabTasks.onClick.RemoveAllListeners();
            buttonCopyLink.onClick.RemoveAllListeners();
        }
    }
}