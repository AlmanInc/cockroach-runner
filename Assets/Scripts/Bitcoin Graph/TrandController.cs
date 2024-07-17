using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;

namespace CockroachRunner
{
    public class TrandController : MonoBehaviour
    {
        [Header("Artificial Generation")]
        [SerializeField] private float candleBaseValue;
        [SerializeField] private float tickMinFactor = 0.1f;
        [SerializeField] private float tickMaxFactor = 0.2f;
        [SerializeField] private float trandChance = 50;

        [Header("Realtime Generation")]
        [SerializeField] private bool useRealTimeGeneration;
        [SerializeField] private float lockRequestDelay = 1f;

        [Inject] private GameSettings gameSettings;

        private Coroutine coroutine;
        private float price;

        public bool RealTimePrice => useRealTimeGeneration;

        private void OnEnable()
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
            }

            if (useRealTimeGeneration) 
            {
                coroutine = StartCoroutine(RealtimeGenerationProcess());
            }
        }

        private void OnDisable()
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
                StopAllCoroutines();
                coroutine = null;
            }
        }

        public float Next()
        {
            if (useRealTimeGeneration)
            {
                return price;
            }

            if (Random.Range(0f, 100f) <= trandChance)
            {
                return candleBaseValue * Random.Range(tickMinFactor, tickMaxFactor);
            }
            else
            {
                return -candleBaseValue * Random.Range(tickMinFactor, tickMaxFactor);
            }
        }

        private IEnumerator RealtimeGenerationProcess()
        {
            RequestData requestData = gameSettings.GetPriceRequest;
            GetPriceResponseData priceData;

            while (true)
            {
                yield return SendRequest(requestData);

                if (requestDone)
                {
                    priceData = JsonUtility.FromJson<GetPriceResponseData>(response);
                    price = priceData.price;
                }

                yield return new WaitForSeconds(lockRequestDelay);
            }
        }

        private bool requestDone;
        private string response;

        private IEnumerator SendRequest(RequestData requestData)
        {
            requestDone = false;
            response = string.Empty;

            string request = requestData.request;
            
            using (UnityWebRequest webRequest = UnityWebRequest.Get(request))
            {
                yield return webRequest.SendWebRequest();

                if (webRequest.result == UnityWebRequest.Result.Success)
                {
                    requestDone = true;
                    response = webRequest.downloadHandler.text;
                }
                else
                {
                    requestDone = false;
                    Debug.LogError(webRequest.error);
                }
            }
        }
    }
}