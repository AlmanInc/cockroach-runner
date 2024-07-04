using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using Zenject;

namespace CockroachRunner
{
    public class GraphView : MonoBehaviour
    {
        [Space]
        [SerializeField] private RectTransform candlesCointainer;
        [SerializeField] private float startMinPrice;
        [SerializeField] private float startMaxPrice;
        [SerializeField] private float timeFrame = 3f;        
        [SerializeField] private float startPrice;
        [SerializeField] private float currentPrice;

        [Header("Price Lines")]
        [SerializeField] private PriceLine[] lines;
        
        [Header("Cells For Bar X Positions")]
        [SerializeField] private Transform[] cells;

        [Inject] DiContainer container;
        [Inject] private GameSettings gameSettings;

        private float minPrice;
        private float maxPrice;
        private int activeCandlesCount;

        private List<CandleView> candles;
        private List<CandleView> freeCandles;
                
        private void Start()
        {            
            candles = new List<CandleView>();
            freeCandles = new List<CandleView>();
            activeCandlesCount = 0;

            minPrice = startMinPrice;
            maxPrice = startMaxPrice;
            SetNewMinMaxPrices(minPrice, maxPrice);    
            
            StartCoroutine(DrawCandlesProcess());
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
        
        private IEnumerator DrawCandlesProcess()
        {
            yield return new WaitForEndOfFrame();

            float time = timeFrame;

            CandleView actualCandle = GenerateNewCandle();
            actualCandle.transform.position = cells[0].transform.position;
            actualCandle.DrawCandle(currentPrice);
            //activeCandlesCount++;

            while (true)
            {
                if (currentPrice < minPrice)
                {
                    minPrice = currentPrice;
                    SetNewMinMaxPrices(minPrice, maxPrice);
                    RedrawCandles();
                    actualCandle.RedrawForNewLimits();
                }

                if (currentPrice > maxPrice)
                {
                    maxPrice = currentPrice;
                    SetNewMinMaxPrices(minPrice, maxPrice);
                    RedrawCandles();
                    actualCandle.RedrawForNewLimits();
                }

                actualCandle.DrawCandle(currentPrice);

                time -= Time.deltaTime;
                if (time <= 0f)
                {
                    // Сдвиг графика
                    candles.Insert(0, actualCandle);
                    int breakIndex = -1;
                    for (int i = 0; i < candles.Count; i++)
                    {
                        if (i < cells.Length - 1)
                        {
                            Vector3 position = candles[i].transform.position;
                            position.x = cells[i + 1].transform.position.x;
                            candles[i].transform.position = position;
                        }
                        else
                        {
                            breakIndex = i;
                            break;
                        }
                    }

                    if (breakIndex > 0)
                    {
                        while (breakIndex < candles.Count) 
                        {
                            var candle = candles[breakIndex];
                            candles.RemoveAt(breakIndex);
                            candle.Clear();
                            freeCandles.Add(candle);
                        }                        
                    }

                    actualCandle = GenerateNewCandle();
                    actualCandle.transform.position = cells[0].transform.position;
                    actualCandle.DrawCandle(currentPrice);

                    time = timeFrame;
                }

                yield return null;
            }
        }

        private CandleView GenerateNewCandle()
        {
            if (freeCandles.Count == 0)
            {
                return container.InstantiatePrefab(gameSettings.CandleViewPrefab, candlesCointainer).GetComponent<CandleView>();
            }
            else
            {
                CandleView candle = freeCandles[0];
                freeCandles.RemoveAt(0);
                return candle;
            }
        }

        private void RedrawCandles()
        {
            foreach (CandleView candle in candles) 
            {
                candle.RedrawForNewLimits();
            }
        }

        public void Clear()
        {
            StopAllCoroutines();

            foreach (var item in candles)
            {
                Destroy(item.gameObject);
            }

            foreach (var item in freeCandles)
            {
                Destroy(item.gameObject);
            }

            candles.Clear();
            freeCandles.Clear();
        }
    }
}