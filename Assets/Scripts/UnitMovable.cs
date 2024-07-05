using System.Collections;
using UnityEngine;
using Zenject;

namespace CockroachRunner
{
    public class UnitMovable : MonoBehaviour
    {
        [SerializeField] private Transform cachedTransform;
        [SerializeField] private Transform center;
        [SerializeField] private GameObject nameObject;
        [SerializeField] private bool isPlayer;
                
        [Inject] private GameSettings gameSettings;
        [Inject] private GraphView graphView;
        
        private Coroutine runCoroutine;
        private Coroutine speedCoroutine;
        private bool isPlaying;

        private Cockroach cockroach;

        private float predictionTime;
        private bool predictUp;
        private float price;
        private float speed;

        public Transform CachedTransform => cachedTransform;
        
        public void ShowName() => nameObject.SetActive(true);

        public void Play()
        {
            if (isPlaying)
            {
                return;
            }
            
            if (runCoroutine != null)
            {
                StopCoroutine(runCoroutine);
            }
                        
            runCoroutine = StartCoroutine(RunProcess());
            speedCoroutine = StartCoroutine(SpeedProcess());
            isPlaying = true;
        }

        public void Stop()
        {
            if (runCoroutine != null)
            {
                StopCoroutine(runCoroutine);
                runCoroutine = null;
            }

            if (speedCoroutine != null)
            {
                StopCoroutine(speedCoroutine);
                speedCoroutine = null;
            }

            predictionTime = 0f;
            cockroach.SetSpeed(0f);
            isPlaying = false;
        }

        public void AddCockroach(Cockroach targetCockroach)
        {
            cockroach = targetCockroach;
            cockroach.CachedTransform.SetParent(center);
            cockroach.CachedTransform.localPosition = Vector3.zero;

            cockroach.SetSpeed(0f);
        }
                
        private IEnumerator RunProcess()
        {
            speed = gameSettings.BaseRunningSpeed;

            while (true)
            {
                //speed = gameSettings.BaseRunningSpeed;
                cockroach.SetSpeed(speed);
                cachedTransform.position += Vector3.forward * speed * Time.deltaTime;
                yield return null;
            }
        }

        public void MakePrediction(bool up)
        {
            predictUp = up;
            predictionTime = 3f;    // заменить потом на настройки в gameSettings
            price = graphView.CurrentPrice;
        }

        private IEnumerator SpeedProcess()
        {
            while (true)
            {
                yield return null;

                if (!isPlayer && predictionTime <= 0f)
                {
                    yield return new WaitForSeconds(Random.Range(0.2f, 0.8f));
                    MakePrediction(Random.Range(1, 101) <= 50);
                }

                if (predictionTime > 0f)
                {
                    yield return null;
                                        
                    predictionTime -= Time.deltaTime;
                                        
                    if ((price <= graphView.CurrentPrice && predictUp) || (price >= graphView.CurrentPrice && !predictUp))
                    {
                        speed = gameSettings.FastRunningSpeed;
                    }
                    else
                    {
                        speed = gameSettings.BaseRunningSpeed;
                    }
                }
                else
                {
                    speed = gameSettings.BaseRunningSpeed;
                }
            }
        }
    }
}