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

        [Inject] private EventsManager eventsManager;
        [Inject] private GameSettings gameSettings;
        [Inject] private GraphView graphView;
        
        private Coroutine runCoroutine;
        private Coroutine speedCoroutine;
        private Coroutine predictionCoroutine;
        private bool isPlaying;

        private Cockroach cockroach;

        private float predictionTime;
        private bool predictUp;
        private float price;
        private float speed;

        public Transform CachedTransform => cachedTransform;

        public bool IsPlaying => isPlaying;

        public bool IsPlayer => isPlayer;
        
        public void ShowName() => nameObject.SetActive(true);
                
        public void Play()
        {
            if (isPlaying)
            {
                return;
            }

            SafeStopCoroutine(runCoroutine);
            SafeStopCoroutine(speedCoroutine);
                                    
            runCoroutine = StartCoroutine(RunProcess());            
            speedCoroutine = StartCoroutine(SpeedProcess());
            
            if (!isPlayer)
            {
                SafeStopCoroutine(predictionCoroutine);
                predictionCoroutine = StartCoroutine(MakePredictionProcess());
            }

            isPlaying = true;
        }

        public void Stop()
        {
            SafeStopCoroutine(runCoroutine);
            SafeStopCoroutine(speedCoroutine);
            SafeStopCoroutine(predictionCoroutine);
                        
            predictionTime = 0f;
            cockroach.SetSpeed(0f);
            SetSpeed(0f, false);
            isPlaying = false;
        }

        private void SafeStopCoroutine(Coroutine coroutine)
        {
            if (coroutine != null) 
            {
                StopCoroutine(coroutine);
                coroutine = null;
            }
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
            while (true)
            {                
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
                if (predictionTime > 0f)
                {
                    predictionTime -= Time.deltaTime;
                                        
                    if ((price <= graphView.CurrentPrice && predictUp) || (price >= graphView.CurrentPrice && !predictUp))
                    {                        
                        SetSpeed(gameSettings.FastRunningSpeed);
                    }
                    else
                    {
                        SetSpeed(gameSettings.BaseRunningSpeed);
                    }
                }
                else
                {
                    SetSpeed(gameSettings.BaseRunningSpeed);
                }

                yield return null;
            }
        }

        private IEnumerator MakePredictionProcess()
        {
            while (true)
            {
                if (predictionTime <= 0f)
                {
                    yield return new WaitForSeconds(Random.Range(0.2f, 0.8f));
                    MakePrediction(Random.Range(1, 101) <= 50);
                }

                yield return null;
            }
        }

        private void SetSpeed(float value, bool fadeSpeed = true)
        {
            speed = fadeSpeed ? Mathf.MoveTowards(speed, value, gameSettings.ChangeSpeedRate * Time.deltaTime) : value;
            
            if (isPlayer)
            {                
                try
                {
                    eventsManager.InvokeEvent(GameEvents.PlayerSetSpeed, speed);
                }
                catch (System.Exception exc)
                {
                    Debug.Log(exc);
                }
            }
        }
    }
}