using UnityEngine;
using UnityEngine.UI;

namespace CockroachRunner
{
    public class UILayoutRebuilder : MonoBehaviour
    {
        [Space]
        [SerializeField] private RectTransform[] rebuildLayoutTargets;

        private void OnEnable()
        {
            foreach (var item in rebuildLayoutTargets)
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate(item);
            }
        }
    }
}