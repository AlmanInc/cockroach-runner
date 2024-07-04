using UnityEngine;

namespace CockroachRunner
{
    public class GraphView : MonoBehaviour
    {
        [SerializeField] private float startMinPrice;
        [SerializeField] private float startMaxPrice;
        [SerializeField] private PriceLine[] lines;

        [SerializeField] private RectTransform start;
        [SerializeField] private float startPrice;

        private float minPrice;
        private float maxPrice;

        private void Start()
        {
            minPrice = startMinPrice;
            maxPrice = startMaxPrice;
            SetNewMinMaxPrices(minPrice, maxPrice);
        }

        public void SetNewMinMaxPrices(float min, float max)
        {
            float priceStep = Mathf.Abs(max - min) / (lines.Length - 1);

            for (int i = 0; i < lines.Length; i++)
            {
                float price = max - i * priceStep;
                lines[i].LabelPrice.text = string.Format("{0:0.000}", price);
            }
        }

        private float GetPriceYPosition(float price)
        {                                    
            float deltaPriceProgress = Mathf.Clamp01((price - minPrice) / (maxPrice - minPrice));
            print(deltaPriceProgress);
            return Mathf.Lerp(lines[lines.Length - 1].transform.position.y, lines[0].transform.position.y, deltaPriceProgress);
        }

        public void Update() 
        {
            Vector3 position = start.position;
            position.y = GetPriceYPosition(startPrice);
            start.position = position;
        }
    }
}