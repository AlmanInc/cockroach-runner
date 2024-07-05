using UnityEngine;

namespace CockroachRunner
{
    [CreateAssetMenu(menuName = "Game Settings", fileName = "Game Settings")]
    public class GameSettings : ScriptableObject
    {
        [Space]
        [SerializeField] private float backCountTimeToStartRace = 3f;
        [SerializeField] private int raceTime = 60;
        [SerializeField] private float baseRunningSpeed = 5f;
        [SerializeField] private float fastRunningSpeed = 10f;

        [Space]
        [SerializeField] private CandleView candleViewPrefab;

        [Space]
        [SerializeField] private Cockroach[] cockroachPrefabs;

        public float BackCountTimeToStartRace => backCountTimeToStartRace;

        public int RaceTime => raceTime;

        public float BaseRunningSpeed => baseRunningSpeed;

        public float FastRunningSpeed => fastRunningSpeed;
                
        public CandleView CandleViewPrefab => candleViewPrefab;

        public Cockroach[] CockroachPrefabs => cockroachPrefabs;
    }
}