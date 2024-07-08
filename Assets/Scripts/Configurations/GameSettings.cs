using UnityEngine;

namespace CockroachRunner
{
    [CreateAssetMenu(menuName = "Game Settings", fileName = "Game Settings")]
    public class GameSettings : ScriptableObject
    {
        [Header("Base Settings")]
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