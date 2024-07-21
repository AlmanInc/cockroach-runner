using UnityEngine.UI;
using UnityEngine;

namespace CockroachRunner
{
    public class PriceLine : MonoBehaviour
    {
        [SerializeField] private RectTransform line;
        [SerializeField] private Text labelPrice;

        public float Price { get; set; }

        public RectTransform Line => line;

        public Text LabelPrice => labelPrice;

        public float Width => line.rect.width;
    }
}