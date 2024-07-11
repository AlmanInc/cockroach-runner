using System.Collections;
using UnityEngine;

namespace CockroachRunner
{
    public class ScreenViewLoading : BaseScreenView
    {
        [Space]
        [SerializeField] private MenuGroupSwitcher menuGroupSwitcher;
        [SerializeField] private float minimumDuration = 1f;

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(minimumDuration);
            menuGroupSwitcher.ShowPanel(ScreenViews.MainMenu);
        }
    }
}