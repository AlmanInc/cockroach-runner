using System.Runtime.InteropServices;
using UnityEngine;

namespace CockroachRunner
{
    public class JSJob : MonoBehaviour
    {
        [DllImport("__Internal")]
        private static extern void GetUserName();

        [DllImport("__Internal")]
        private static extern void GetUserId();

        [DllImport("__Internal")]
        private static extern void GetUserRef();

        [DllImport("__Internal")]
        private static extern void ShowMessage(string str);

        [DllImport("__Internal")]
        private static extern void CopyReferalLink(string str);


        public string UserName { get; private set; }

        public string UserId { get; private set; }

        public string UserRefId { get; private set; }


        public void LoadUserName(string userName)
        {
            this.UserName = userName;
        }
        
        public void LoadUserId(string userId)
        {
            this.UserId = userId;
        }

        public void LoadUserRefId(string refId)
        {
            this.UserRefId = refId;
        }
        

        public void TryGetUserName() => GetUserName();

        public void TryGetUserId() => GetUserId();

        public void TryGetUserRefId() => GetUserRef();

        public void TrySendMessage(string str) => ShowMessage(str);

        public void TryCopyReferalLink(string str) => CopyReferalLink(str);
    }
}