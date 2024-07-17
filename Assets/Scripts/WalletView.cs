using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using Zenject;
using UnityEngine.Networking;
using System;

namespace CockroachRunner
{
    public class WalletView : MonoBehaviour
    {
        [SerializeField] private Button buttonAddCurrency;
        [SerializeField] private Text labelCurrency;
        [SerializeField] private int addCurrencyAmount;

        [Inject] private EventsManager eventsManager;
        [Inject] private GameState gameState;
                
        private void OnEnable()
        {
            labelCurrency.text = gameState.Currency.ToString();
            eventsManager.AddListener(GameEvents.UpdateCurrencyStatus, OnUpdateWalletBalance);

            buttonAddCurrency.onClick.AddListener(delegate
            {
                eventsManager.InvokeEvent(GameEvents.AddCurrency, addCurrencyAmount);
            });
        }
        
        private void OnDisable()
        {
            eventsManager.RemoveListener(GameEvents.UpdateCurrencyStatus, OnUpdateWalletBalance);
            buttonAddCurrency.onClick.RemoveAllListeners();
        }

        private void OnUpdateWalletBalance(object[] args)
        {
            labelCurrency.text = gameState.Currency.ToString();
        }
    }
}