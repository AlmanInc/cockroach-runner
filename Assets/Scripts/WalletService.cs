using System.Collections;
using UnityEngine.Networking;
using UnityEngine;
using Zenject;
using System;

namespace CockroachRunner
{
    public class WalletService : MonoBehaviour
    {
        [SerializeField] private GameSettings gameSettings;

        [Inject] private EventsManager eventsManager;
        [Inject] private GameState gameState;

        private string response;

        public bool IsLocked { get; private set; }

        public void Start()
        {
            eventsManager.AddListener(GameEvents.LoadCurrency, OnLoadCurrency);
            eventsManager.AddListener(GameEvents.AddCurrency, OnAddCurrency);            
            DontDestroyOnLoad(this.gameObject);
        }
                
        private void OnDestroy()
        {
            eventsManager.RemoveListener(GameEvents.LoadCurrency, OnLoadCurrency);
            eventsManager.RemoveListener(GameEvents.AddCurrency, OnAddCurrency);
        }

        private void OnLoadCurrency(object[] args)
        {
            if (!IsLocked)
            {
                GetCurrency();
            }
        }

        private void OnAddCurrency(params object[] args)
        {
            if (!IsLocked)
            {
                int amount = (int)args[0];
                AddCurrency(amount);
            }
            else
            {
                Debug.Log("Wallet Service is working. Can't add currency");
            }
        }

        public void AddCurrency(int amount)
        {
            if (!IsLocked)
            {
                StartCoroutine(AddCurrencyProcess(amount));
            }
        }

        public void GetCurrency()
        {
            if (!IsLocked)
            {
                StartCoroutine(GetCurrencyProcess());
            }
        }

        private IEnumerator AddCurrencyProcess(int amount)
        {
            IsLocked = true;
            yield return SendRequest(gameSettings.AddCurrencyRequest, amount);

            if (!string.IsNullOrEmpty(response)) 
            {
                gameState.Currency += amount;

                GetCurrencyResponseData data = JsonUtility.FromJson<GetCurrencyResponseData>(response);
                
                if (gameState.Currency != data.balance)
                {
                    Debug.LogError("Different balances");
                }
                
                gameState.Currency = data.balance;

                eventsManager.InvokeEvent(GameEvents.UpdateCurrencyStatus);                
            }
            
            IsLocked = false;
        }

        private IEnumerator GetCurrencyProcess()
        {
            IsLocked = true;
            yield return SendRequest(gameSettings.GetCurrencyRequest);

            if (!string.IsNullOrEmpty(response))
            {
                GetCurrencyResponseData data = JsonUtility.FromJson<GetCurrencyResponseData>(response);
                gameState.Currency = data.balance;

                eventsManager.InvokeEvent(GameEvents.UpdateCurrencyStatus);
            }

            IsLocked = false;
        }

        private IEnumerator SendRequest(RequestData requestData, int amount = 0)
        {
            response = string.Empty;

            string request = requestData.request;
            request = request.Replace("{server}", gameSettings.ServerName);
            request = request.Replace("{id}", PlayerData.Id);
            request = request.Replace("{currency}", amount.ToString());

            using (UnityWebRequest webRequest = UnityWebRequest.Get(request))
            {
                yield return webRequest.SendWebRequest();

                if (webRequest.result == UnityWebRequest.Result.Success)
                {
                    response = webRequest.downloadHandler.text;
                }
                else
                {
                    Debug.LogError(webRequest.error);
                }
            }
        }
    }
}