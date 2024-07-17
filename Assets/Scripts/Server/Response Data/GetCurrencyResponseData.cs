using System;
using UnityEngine;

namespace CockroachRunner
{
    [Serializable]
    public class GetCurrencyResponseData
    {
        public bool status;
        public int balance;
    }
}