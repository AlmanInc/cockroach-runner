using System.Runtime.InteropServices;
using UnityEngine;

namespace CockroachRunner
{
    public class JSJob : MonoBehaviour
    {
        public string UserName { get; private set; }

        public string UserId { get; private set; }

        public string UserRefId { get; private set; }

        [DllImport("__Internal")]
        private static extern void GetUserName();

        [DllImport("__Internal")]
        private static extern void GetUserId();

        [DllImport ("__Internal")]
        private static extern void GetUserRef();

        public void LoadUserName(string userName)
        {
            UserName = userName;
        }

        public void LoadUserId(string userId)
        {
            UserId = userId;
        }

        public void LoadUserRefId(string refId)
        {
            UserRefId = refId;
        }
        
        public void TryGetUserName() => GetUserName();

        public void TryGetUserId() => GetUserId();

        public void TryGetUserRefId() => GetUserRef();
    }
}