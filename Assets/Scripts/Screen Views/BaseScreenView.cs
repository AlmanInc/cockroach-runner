using UnityEngine;

namespace CockroachRunner
{
    public class BaseScreenView : MonoBehaviour
    {
        [SerializeField] private GameObject screenViewPanel;

        private void OnEnable() => Activate();

        private void OnDisable() => Deactivate();

        public virtual void Activate()
        {
            if (screenViewPanel != null)
            {
                screenViewPanel?.SetActive(true);
            }
        }

        public virtual void Deactivate()
        {
            if (screenViewPanel != null)
            {
                screenViewPanel?.SetActive(false);
            }
        }
    }
}