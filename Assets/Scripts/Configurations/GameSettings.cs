using UnityEngine;

namespace CockroachRunner
{
    [CreateAssetMenu(menuName = "Game Settings", fileName = "Game Settings")]
    public class GameSettings : ScriptableObject
    {
        [Header("Server and Initialization")]
        [SerializeField] private string defaultUserId;
        [SerializeField] private string defaultUserName;
        [SerializeField] private string referalLink;
        [SerializeField] private string serverName;
        [SerializeField] private RequestData requestCheckUser;
        [SerializeField] private RequestData requestUpdateUser;
        [SerializeField] private RequestData requestAddUser;
        [SerializeField] private RequestData requestAddReferalForUser;
        [SerializeField] private RequestData requestGetAllReferals;
        [SerializeField] private RequestData requestGetPrice;
        
        [Header("Race")]
        [SerializeField] private float backCountTimeToStartRace = 3f;
        [SerializeField] private int raceTime = 60;
        [SerializeField] private float baseRunningSpeed = 5f;
        [SerializeField] private float baseRunningSpeedLabelValue = 2.5f;
        [SerializeField] private float fastRunningSpeed = 10f;
        [SerializeField] private float fastRunningSpeedLabelValue = 5f;
        [SerializeField] private float changeSpeedRate = 10f;

        [Header("Prefabs")]
        [SerializeField] private CandleView candleViewPrefab;

        [Space]
        [SerializeField] private Cockroach[] cockroachPrefabs;


        // Loading
        public string ServerName => serverName;

        public string DefaultUserId => defaultUserId;

        public string DefaultUserName => defaultUserName;

        public string ReferalLink => referalLink;

        public RequestData CheckUserRequest => requestCheckUser;

        public RequestData UpdateUserRequest => requestUpdateUser;

        public RequestData AddUserRequest => requestAddUser;

        public RequestData AddReferalForUserRequest => requestAddReferalForUser;

        public RequestData GetAllReferalsRequest => requestGetAllReferals;

        public RequestData GetPriceRequest => requestGetPrice;


        // Other
        public float BackCountTimeToStartRace => backCountTimeToStartRace;

        public int RaceTime => raceTime;

        public float BaseRunningSpeed => baseRunningSpeed;

        public float BaseRunningSpeedLabelValue => baseRunningSpeedLabelValue;

        public float FastRunningSpeed => fastRunningSpeed;

        public float FastRunningSpeedLabelValue => fastRunningSpeedLabelValue;

        public float ChangeSpeedRate => changeSpeedRate;
                
        public CandleView CandleViewPrefab => candleViewPrefab;

        public Cockroach[] CockroachPrefabs => cockroachPrefabs;
    }
}