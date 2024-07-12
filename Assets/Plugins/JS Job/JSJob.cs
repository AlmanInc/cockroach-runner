using System.Runtime.InteropServices;
using UnityEngine;

namespace CockroachRunner
{
    public class JSJob : MonoBehaviour
    {
        public string UserName { get; private set; }

        public string UserId { get; private set; }

        [DllImport("__Internal")]
        private static extern void GetUserName();

        [DllImport("__Internal")]
        private static extern void GetUserId();

        public void LoadUserName(string userName)
        {
            UserName = userName;
        }

        public void LoadUserId(string userId)
        {
            UserId = userId;
        }

        public void GetTelegramUserName() => GetUserName();

        public void GetTelegramUserId() => GetUserId();
    }
}