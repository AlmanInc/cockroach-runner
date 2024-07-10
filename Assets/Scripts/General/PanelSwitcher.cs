using UnityEngine;

namespace CockroachRunner
{
    public class PanelSwitcher<TKind> : MonoBehaviour
    {
        [SerializeField] private SerializableItem<TKind, GameObject>[] panels;

        public void ShowPanel(TKind kind)
        {
            foreach (var panel in panels)
            {
                panel.item.SetActive(panel.id.Equals(kind));
            }
        }        
    }
}