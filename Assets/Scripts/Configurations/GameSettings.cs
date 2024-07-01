using UnityEngine;

namespace CockroachRunner
{
    [CreateAssetMenu(menuName = "Game Settings", fileName = "Game Settings")]
    public class GameSettings : ScriptableObject
    {
        [SerializeField] private float playerBaseSpeed = 5f;
        [SerializeField] private float playerJumpBackDistance = 10f;

        public float PlayerBaseSpeed => playerBaseSpeed;

        public float PlayerJumpBackDistance => playerJumpBackDistance;
    }
}