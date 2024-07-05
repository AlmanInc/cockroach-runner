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
                
        [Inject] private GameSettings gameSettings;
        
        private Coroutine runCoroutine;
        private bool isPlaying;

        public Transform CachedTransform => cachedTransform;

        private Cockroach cockroach;

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
            isPlaying = true;
        }

        public void Stop()
        {
            if (runCoroutine != null)
            {
                StopCoroutine(runCoroutine);
                runCoroutine = null;
            }

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
            while (true)
            {
                float speed = gameSettings.BaseRunningSpeed;
                cockroach.SetSpeed(speed);
                cachedTransform.position += Vector3.forward * speed * Time.deltaTime;
                yield return null;
            }
        }
    }
}