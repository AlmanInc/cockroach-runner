using System.Collections;
using UnityEngine;
using Zenject;

namespace CockroachRunner
{
    public class PlayerMovable : MonoBehaviour
    {
        [SerializeField] private Transform player;
        
        [Inject] private GameSettings gameSettings;
        
        private Coroutine runCoroutine;
        private float startZPosition;
        private bool isPlaying;

        private void Start () 
        {
            startZPosition = player.position.z;
            Play();
        }

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
                
        private IEnumerator RunProcess()
        {
            while (true)
            {
                player.position += Vector3.forward * gameSettings.PlayerBaseSpeed * Time.deltaTime;
                yield return null;
            }
        }
    }
}