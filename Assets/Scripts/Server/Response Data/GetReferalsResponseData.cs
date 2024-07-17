using System.Collections.Generic;
using System;
using UnityEngine;

namespace CockroachRunner
{
    [Serializable]
    public class GetReferalsResponseData
    {        
        public bool status;
        public UserData[] referals;
    }
}