using UnityEngine;

namespace CockroachRunner
{
    public class TrandController : MonoBehaviour
    {
        [SerializeField] private float startMinPrice;
        [SerializeField] private float startMaxPrice;
        [SerializeField] private float tickMinFactor = 0.1f;
        [SerializeField] private float tickMaxFactor = 0.2f;
        [SerializeField] private float trandChance = 50;
        

        private float delta;

        private void Start()
        {
            delta = Mathf.Abs(startMaxPrice - startMinPrice);            
        }

        public float Next()
        {
            if (Random.Range(0f, 100f) <= trandChance)
            {
                return delta * Random.Range(tickMinFactor, tickMaxFactor);
            }
            else
            {
                return -delta * Random.Range(tickMinFactor, tickMaxFactor);
            }
        }
    }
}