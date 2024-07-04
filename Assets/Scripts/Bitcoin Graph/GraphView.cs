using UnityEngine;

namespace CockroachRunner
{
    public class GraphView : MonoBehaviour
    {
        [SerializeField] private float minPrice;
        [SerializeField] private float maxPrice;
        [SerializeField] private PriceLine[] lines;

        private void Start()
        {
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
    }
}