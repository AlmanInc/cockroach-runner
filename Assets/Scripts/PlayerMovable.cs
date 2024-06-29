using UnityEngine;

namespace CockroachRunner
{
    public class PlayerMovable : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private float speed = 3f;

        private void Update()
        {
            player.position += Vector3.forward * speed * Time.deltaTime;
        }
    }
}