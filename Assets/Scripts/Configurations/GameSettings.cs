using System.Collections.Generic;
using UnityEngine;

namespace CockroachRunner
{
    [CreateAssetMenu(menuName = "Game Settings", fileName = "Game Settings")]
    public class GameSettings : ScriptableObject
    {
        [Header("Base Settings")]
        [SerializeField] private string defaultUserId;
        [SerializeField] private string defaultUserName;
        [SerializeField] private string referalLink;

        [Header("Server Settings")]
        [SerializeField] private string serverName;
        [SerializeField] private bool logRequests;
        [SerializeField] private RequestData requestCheckUser;
        [SerializeField] private RequestData requestUpdateUser;
        [SerializeField] private RequestData requestAddUser;
        [SerializeField] private RequestData requestAddReferalForUser;
        [SerializeField] private RequestData requestGetAllReferals;
        [SerializeField] private RequestData requestGetPrice;           // bitcoin price
        [SerializeField] private RequestData requestGetCurrency;        // user balance
        [SerializeField] private RequestData requestAddCurrency;
        [SerializeField] private RequestData requestGetUserTasks;

        [Header("Race Settings")]
        [SerializeField] private int raceBet;
        [SerializeField] private float backCountTimeToStartRace = 3f;
        [SerializeField] private int raceTime = 60;
        [SerializeField] private float baseRunningSpeed = 5f;
        [SerializeField] private float baseRunningSpeedLabelValue = 2.5f;
        [SerializeField] private float fastRunningSpeed = 10f;
        [SerializeField] private float fastRunningSpeedLabelValue = 5f;
        [SerializeField] private float changeSpeedRate = 10f;

        [Header("Prefabs")]
        [SerializeField] private TaskLineView taskLineTop;
        [SerializeField] private TaskLineView taskLineBase;
        [SerializeField] private TaskLineView taskLineBottom;
        [SerializeField] private CandleView candleViewPrefab;

        [Space]
        [SerializeField] private Cockroach[] cockroachPrefabs;
        [SerializeField] private CockroachVariants[] cockroaches;


        // Base Settings
        public string DefaultUserId => defaultUserId;

        public string DefaultUserName => defaultUserName;

        public string ReferalLink => referalLink;


        // Server Settings
        public string ServerName => serverName;

        public bool LogRequests => logRequests;

        public RequestData RequestCheckUser => requestCheckUser;

        public RequestData RequestUpdateUser => requestUpdateUser;

        public RequestData RequestAddUser => requestAddUser;

        public RequestData RequestAddReferalForUser => requestAddReferalForUser;

        public RequestData RequestGetAllReferals => requestGetAllReferals;

        public RequestData RequestGetPrice => requestGetPrice;

        public RequestData RequestGetCurrency => requestGetCurrency;

        public RequestData RequestAddCurrency => requestAddCurrency;

        public RequestData RequestGetUserTasks => requestGetUserTasks;


        // Other
        public int RaceBet => raceBet;

        public float BackCountTimeToStartRace => backCountTimeToStartRace;

        public int RaceTime => raceTime;

        public float BaseRunningSpeed => baseRunningSpeed;

        public float BaseRunningSpeedLabelValue => baseRunningSpeedLabelValue;

        public float FastRunningSpeed => fastRunningSpeed;

        public float FastRunningSpeedLabelValue => fastRunningSpeedLabelValue;

        public float ChangeSpeedRate => changeSpeedRate;


        // Prefabs                
        public TaskLineView TaskLineTop => taskLineTop;

        public TaskLineView TaskLineBase => taskLineBase;

        public TaskLineView TaskLineBottom => taskLineBottom;

        public CandleView CandleViewPrefab => candleViewPrefab;
                
        public Cockroach[] CockroachPrefabs => cockroachPrefabs;

        public CockroachVariants[] Cockroaches => cockroaches;
    }
}