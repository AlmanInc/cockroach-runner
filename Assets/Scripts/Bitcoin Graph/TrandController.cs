using UnityEngine;

namespace CockroachRunner
{
    public class TrandController : MonoBehaviour
    {
        [SerializeField] private float candleBaseValue;
        [SerializeField] private float tickMinFactor = 0.1f;
        [SerializeField] private float tickMaxFactor = 0.2f;
        [SerializeField] private float trandChance = 50;
        
        public float Next()
        {
            if (Random.Range(0f, 100f) <= trandChance)
            {
                return candleBaseValue * Random.Range(tickMinFactor, tickMaxFactor);
            }
            else
            {
                return -candleBaseValue * Random.Range(tickMinFactor, tickMaxFactor);
            }
        }
    }
}