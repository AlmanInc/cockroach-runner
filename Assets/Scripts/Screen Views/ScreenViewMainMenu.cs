using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using Zenject;

namespace CockroachRunner
{
    public class ScreenViewMainMenu : BaseScreenView
    {
        [Space]
        [SerializeField] private JSJob jsJob;
        [SerializeField] private MenuGroupSwitcher menuGroupSwitcher;
        [SerializeField] private Button buttonRace;
        [SerializeField] private Button buttonTasks;
        [SerializeField] private Button buttonAboutGame;
        
        [Inject] private EventsManager eventsManager;
        [Inject] private GameSettings gameSettings;
        [Inject] private GameState gameState;

        public override void Activate()
        {
            base.Activate();

            buttonRace.onClick.AddListener(delegate
            {
                if (gameState.Currency >= gameSettings.RaceBet)
                {
                    eventsManager.InvokeEvent(GameEvents.AddCurrency, -gameSettings.RaceBet);
                    SceneManager.LoadSceneAsync("Game");
                }
                else
                {
#if UNITY_WEBGL && !UNITY_EDITOR
                    jsJob.TrySendMessage("You don't have enough money for the race");
#endif
                }
            });

            buttonTasks.onClick.AddListener(delegate
            {
                menuGroupSwitcher.ShowPanel(ScreenViews.Tasks);
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
            buttonTasks.onClick.RemoveAllListeners();
            buttonAboutGame.onClick.RemoveAllListeners();
        }
    }
}