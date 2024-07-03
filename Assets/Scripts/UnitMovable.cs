using System.Collections;
using UnityEngine;
using Zenject;

namespace CockroachRunner
{
    public class UnitMovable : MonoBehaviour
    {
        [SerializeField] private Transform cachedTransform;
        [SerializeField] private Transform center;
        
        [Inject] private GameSettings gameSettings;
        
        private Coroutine runCoroutine;
        private bool isPlaying;

        public Transform CachedTransform => cachedTransform;

        private Transform cockroach;

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

            isPlaying = false;
        }

        public void AddCockroach(Transform targetCockroach)
        {
            cockroach = targetCockroach;
            cockroach.SetParent(center);
            cockroach.localPosition = Vector3.zero;
        }
                
        private IEnumerator RunProcess()
        {
            while (true)
            {
                cachedTransform.position += Vector3.forward * gameSettings.PlayerBaseSpeed * Time.deltaTime;
                yield return null;
            }
        }
    }
}