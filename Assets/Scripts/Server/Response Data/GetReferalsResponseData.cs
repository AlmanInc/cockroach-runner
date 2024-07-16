using System.Collections.Generic;
using System;
using UnityEngine;

namespace CockroachRunner
{
    // {"status": true, "referals": [{"tgid": "16", "name": "nasasdasdadd", "referal_id": "15"}, {"tgid": "17", "name": "nasasdasdadd", "referal_id": "15"}]}

    [Serializable]
    public class UserData
    {
        public string tgid;
        public string name;
        public string referal_id;
    }

    [Serializable]
    public class GetReferalsResponseData
    {        
        public bool status;
        public UserData[] referals;
    }
}