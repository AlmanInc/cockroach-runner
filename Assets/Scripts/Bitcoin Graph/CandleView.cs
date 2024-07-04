using UnityEngine;
using Zenject;

namespace CockroachRunner
{
    public class CandleView : MonoBehaviour
    {
        [Space]
        [SerializeField] private RectTransform topBar;
        [SerializeField] private RectTransform topShadow;
        [SerializeField] private RectTransform bottomBar;
        [SerializeField] private RectTransform bottomShadow;

        [Space]
        [SerializeField] private Color upColor;
        [SerializeField] private Color downColor;

        [Inject] private GraphView graphView;

        private bool isActive;
        
        public float StartPrice { get; set; }
        public float EndPrice { get; set; }
        public float MinPrice { get; set; }
        public float MaxPrice { get; set; }

        public void Clear()
        {
            isActive = false;

            topBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0f);
            topShadow.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0f);
            bottomBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0f);
            bottomShadow.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0f);
        }

        public void DrawCandle(float price)
        {
            if (!isActive)
            {
                isActive = true;

                Vector3 position = transform.position;
                position.y = graphView.GetPriceYPosition(price);
                transform.position = position;

                StartPrice = price;
                EndPrice = price;
                MinPrice = price;
                MaxPrice = price;
            }
            else
            {
                EndPrice = price;

                topBar.gameObject.SetActive(EndPrice >= StartPrice);
                bottomBar.gameObject.SetActive(EndPrice < StartPrice);
                RectTransform bar = EndPrice >= StartPrice ? topBar : bottomBar;

                float y = graphView.GetPriceYPosition(EndPrice);
                float height = Mathf.Abs(transform.position.y - y);
                if (height < 2f)
                {
                    height = 2f;
                }

                bar.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
            }
        }
    }
}