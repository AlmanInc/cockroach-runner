using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace CockroachRunner
{
    public class DescreteObjectsMovable : MonoBehaviour
    {
        [SerializeField] private List<Transform> movingObjects;
        [SerializeField] private Transform player;
        [SerializeField] private float replaceDistance = 20f;
        [SerializeField] private float elementsPadding = 10f;

        [Inject] private GameSettings gameSettings;
        
        private void Update()
        {                                    
            Transform nearestBlock = movingObjects[0];
            if (Mathf.Abs(player.position.z - nearestBlock.position.z) >= replaceDistance)
            {
                movingObjects.RemoveAt(0);
                nearestBlock.position = movingObjects[movingObjects.Count - 1].position + Vector3.forward * elementsPadding;
                movingObjects.Add(nearestBlock);
            }
        }
    }
}