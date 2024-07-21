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
        [SerializeField] private TrandController trand;
        [SerializeField] private float startMinPrice;
        [SerializeField] private float startMaxPrice;
        [SerializeField] private float timeFrame = 3f;
        [SerializeField] private float startPrice;
        [SerializeField] private float realtimeStartPriceOffset = 80f;
        [SerializeField] private PriceLine currentPriceLine;

        [Header("Price Lines")]
        [SerializeField] private PriceLine[] lines;

        [Header("Cells For Bar X Positions")]
        [SerializeField] private Transform[] cells;

        [Inject] DiContainer container;
        [Inject] private GameSettings gameSettings;

        private float minPrice;
        private float maxPrice;
        private float price;

        private List<CandleView> candles;
        private List<CandleView> freeCandles;

        public float CurrentPrice => price;

        private IEnumerator Start()
        {
            candles = new List<CandleView>();
            freeCandles = new List<CandleView>();

            if (!trand.RealTimePrice)
            {
                price = startPrice;
                minPrice = startMinPrice;
                maxPrice = startMaxPrice;
                SetNewMinMaxPrices(minPrice, maxPrice);
            }
            else
            {
                price = 0f;

                while (price == 0f)
                {
                    yield return null;
                    price = trand.Next();
                }
                                
                minPrice = price - realtimeStartPriceOffset;
                maxPrice = price + realtimeStartPriceOffset;
                SetNewMinMaxPrices(minPrice, maxPrice);
            }

            StartCoroutine(DrawCandlesProcess());
        }

        public void SetNewMinMaxPrices(float min, float max)
        {
            float priceStep = Mathf.Abs(max - min) / (lines.Length - 1);

            for (int i = 0; i < lines.Length; i++)
            {
                float price = max - i * priceStep;
                lines[i].Price = price;
                lines[i].LabelPrice.text = string.Format("{0:0.0}", price);
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
            actualCandle.DrawCandle(price);

            DrawPriceLine(currentPriceLine);
            currentPriceLine.gameObject.SetActive(true);

            while (true)
            {

                yield return new WaitForEndOfFrame();
                
                if (trand.RealTimePrice)
                {
                    price = trand.Next();
                }
                else
                {
                    price += trand.Next();
                }

                DrawPriceLine(currentPriceLine);

                if (price < minPrice)
                {
                    minPrice = price;
                    SetNewMinMaxPrices(minPrice, maxPrice);
                    RedrawCandles();
                    actualCandle.RedrawForNewLimits();
                }

                if (price > maxPrice)
                {
                    maxPrice = price;
                    SetNewMinMaxPrices(minPrice, maxPrice);
                    RedrawCandles();
                    actualCandle.RedrawForNewLimits();
                }

                actualCandle.DrawCandle(price);

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
                    actualCandle.DrawCandle(price);

                    time = timeFrame;

                    RefreshMinMaxPrices(actualCandle);
                }

                yield return null;
            }
        }

        private void RefreshMinMaxPrices(CandleView actualCandle)
        {
            print("Check");

            float min = lines[lines.Length - 2].Price;      // Предпоследний элемент
            float max = lines[1].Price;                     // Следующий после первого элемент
            bool needUpdateMin = true;
            bool needUpdateMax = true;

            foreach (var candle in candles)
            {
                if (candle.MaxPrice >= max)
                {
                    needUpdateMax = false;
                }

                if (candle.MinPrice <= min) 
                {
                    needUpdateMin = false;                    
                }
            }

            if (actualCandle.MaxPrice >= max)
            {
                needUpdateMax = false;                
            }

            if (actualCandle.MinPrice <= min)
            {
                needUpdateMin = false;                
            }

            if (needUpdateMin || needUpdateMax)
            {
                float targetMin = needUpdateMin ? min : minPrice;
                float targetMax = needUpdateMax ? max : maxPrice;
                SetNewMinMaxPrices(targetMin, targetMax);
                RedrawCandles();
                actualCandle.RedrawForNewLimits();
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

        private void DrawPriceLine(PriceLine line)
        {
            Vector3 position = line.transform.position;
            position.y = GetPriceYPosition(price);
            line.transform.position = position;

            line.LabelPrice.text = string.Format("{0:0.0}", price);
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