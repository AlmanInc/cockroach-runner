using UnityEngine;

namespace CockroachRunner
{
    [CreateAssetMenu(menuName = "Game Settings", fileName = "Game Settings")]
    public class GameSettings : ScriptableObject
    {
        [Space]
        [SerializeField] private float backCountTimeToStartRace = 3f;
        [SerializeField] private int raceTime = 60;
        [SerializeField] private float playerBaseSpeed = 5f;
        [SerializeField] private float playerJumpBackDistance = 10f;

        [Space]
        [SerializeField] private Cockroach[] cockroachPrefabs;

        public float BackCountTimeToStartRace => backCountTimeToStartRace;

        public int RaceTime => raceTime;

        public float PlayerBaseSpeed => playerBaseSpeed;

        public float PlayerJumpBackDistance => playerJumpBackDistance;

        public Cockroach[] CockroachPrefabs => cockroachPrefabs;
    }
}