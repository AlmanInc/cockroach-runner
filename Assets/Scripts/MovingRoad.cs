using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace CockroachRunner
{
    public class MovingRoad : MonoBehaviour
    {
        [SerializeField] private Transform anchor;
        [SerializeField] private float speed;
        [SerializeField] private float offset;
        [SerializeField] private List<Transform> road;

        private IEnumerator Start()
        {
            while (true)
            {                
                /*
                foreach (Transform t in road) 
                {
                    t.position += Vector3.back * speed * Time.deltaTime;
                }

                Transform nearestBlock = road[0];
                if (nearestBlock.position.z <= anchor.position.z)
                {                    
                    road.RemoveAt(0);
                    nearestBlock.position = road[road.Count - 1].position + Vector3.forward * offset;
                    road.Add(nearestBlock);
                }
                */

                yield return null;
            }
        }
        
    }
}