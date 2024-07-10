using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

namespace CockroachRunner
{
    public class ScreenViewMainMenu : BaseScreenView
    {
        [Space]
        [SerializeField] private MenuGroupSwitcher menuGroupSwitcher;
        [SerializeField] private Button buttonRace;
        [SerializeField] private Button buttonAboutGame;

        public override void Activate()
        {
            base.Activate();

            buttonRace.onClick.AddListener(delegate
            {
                SceneManager.LoadSceneAsync("Game");
            });

            buttonAboutGame.onClick.AddListener(delegate
            {
                menuGroupSwitcher.ShowPanel(ScreenViews.AboutGame1);
            });
        }

        public override void Deactivate()
        {
            base.Deactivate();

            buttonRace.onClick.RemoveAllListeners();
            buttonAboutGame.onClick.RemoveAllListeners();
        }
    }
}