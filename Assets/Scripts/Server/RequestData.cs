using System;

namespace CockroachRunner
{
    [Serializable]
    public class RequestData
    {
        public string request;
        public RequestTypes kind;
        public bool showResponse;
    }
}