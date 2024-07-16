using System;
using UnityEngine;

namespace CockroachRunner
{
    [Serializable]
    public class RequestData
    {
        public string request;
        public RequestTypes kind;
    }
}