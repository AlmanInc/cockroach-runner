using UnityEngine.UI;
using UnityEngine;

namespace CockroachRunner
{
    public class ReferalLineView : MonoBehaviour
    {
        [SerializeField] private RectTransform cachedTransform;
        [SerializeField] private Text labelUserName;

        public RectTransform CachedTransform => cachedTransform;

        public int Index { get; private set; }

        public void SetData(int index)
        {
            Index = index;

            UserData data = PlayerData.Referals[index];
            labelUserName.text = data.name;
        }
    }
}