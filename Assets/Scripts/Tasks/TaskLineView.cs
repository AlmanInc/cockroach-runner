using System;
using UnityEngine.UI;
using UnityEngine;

namespace CockroachRunner
{
    public class TaskLineView : MonoBehaviour
    {
        [Serializable]
        private struct CompletedStateObject
        {
            public GameObject targetObject;
            public bool isActiveWhenComplete;
        }

        [Serializable]
        private struct ColorableObject
        {
            public Graphic targetObject;
            public Color baseColor;
            public Color completedColor;
        }

        [SerializeField] private bool isCompleted;

        [Space]
        [SerializeField] private CompletedStateObject[] completedStateObjects;

        [Space]
        [SerializeField] private ColorableObject[] colorableTaskObjects;
                
        private void OnEnable()
        {
            SetCompleteState(isCompleted);
        }

        public void SetCompleteState(bool completed)
        {
            foreach (CompletedStateObject obj in completedStateObjects) 
            {
                obj.targetObject.SetActive(obj.isActiveWhenComplete ? completed : !completed);
            }

            foreach (ColorableObject obj in colorableTaskObjects)
            {
                obj.targetObject.color = completed ? obj.completedColor : obj.baseColor;
            }
        }
    }
}