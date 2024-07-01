using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace CockroachRunner
{
    public class DescreteObjectsMovable : MonoBehaviour
    {
        [SerializeField] private List<EnvironmentObject> movingObjects;
        [SerializeField] private Transform player;
        [SerializeField] private float replaceDistance = 20f;
        [SerializeField] private float elementsPadding = 10f;

        [Inject] private GameSettings gameSettings;
        
        private void Update()
        {                                    
            EnvironmentObject nearestBlock = movingObjects[0];
            if (Mathf.Abs(player.position.z - nearestBlock.CachedTransform.position.z) >= replaceDistance)
            {
                movingObjects.RemoveAt(0);
                nearestBlock.Activate();
                nearestBlock.CachedTransform.position = movingObjects[movingObjects.Count - 1].CachedTransform.position + Vector3.forward * elementsPadding;
                movingObjects.Add(nearestBlock);
            }
        }
    }
}