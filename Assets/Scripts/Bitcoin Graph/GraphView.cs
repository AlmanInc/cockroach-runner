using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace CockroachRunner
{
    public class GraphView : MonoBehaviour
    {
        [SerializeField] private float startMinPrice;
        [SerializeField] private float startMaxPrice;
        [SerializeField] private PriceLine[] lines;

        [Space]
        [SerializeField] private VerticalLayoutGroup verticalLayoutGroup;
        [SerializeField] private CandleView candle;
        [SerializeField] private float startPrice;
        [SerializeField] private float currentPrice;

        [Header("Cells For Bar X Positions")]
        [SerializeField] private Transform[] cells;

        [Space]
        [SerializeField] private RectTransform[] rebuildLayoutTargets;

        private float minPrice;
        private float maxPrice;

        private List<CandleView> candles;
                
        private void Start()
        {
            foreach (var item in rebuildLayoutTargets)
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate(item);
            }

            minPrice = startMinPrice;
            maxPrice = startMaxPrice;
            SetNewMinMaxPrices(minPrice, maxPrice);

            candle.DrawCandle(startPrice);
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

        public float GetPriceYPosition(float price)
        {                                    
            float deltaPriceProgress = Mathf.Clamp01((price - minPrice) / (maxPrice - minPrice));
            return Mathf.Lerp(lines[lines.Length - 1].transform.position.y, lines[0].transform.position.y, deltaPriceProgress);
        }
        
        public void Update() 
        {
            if (currentPrice < minPrice)
            {
                minPrice = currentPrice;
                SetNewMinMaxPrices(minPrice, maxPrice);
                candle.RedrawForNewLimits();
            }

            if (currentPrice > maxPrice) 
            {
                maxPrice = currentPrice;
                SetNewMinMaxPrices(minPrice, maxPrice);
                candle.RedrawForNewLimits();
            }

            candle.DrawCandle(currentPrice);
        }
    }
}