using System;
using UnityEngine.UI;
using UnityEngine;
using Zenject;

namespace CockroachRunner
{
    public class TaskLineView : MonoBehaviour
    {
        public string TaskId { get; set; }

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
                
        [SerializeField] private Button buttonTaskDetails;

        [Space]
        [SerializeField] private CompletedStateObject[] completedStateObjects;

        [Space]
        [SerializeField] private ColorableObject[] colorableTaskObjects;

        [Inject] private EventsManager eventsManager;
                
        private void OnEnable()
        {
            buttonTaskDetails.onClick.AddListener(delegate
            {
                eventsManager.InvokeEvent(GameEvents.TryOpenTaskDetails, TaskId);
            });
        }

        private void OnDisable() => buttonTaskDetails.onClick.RemoveAllListeners();

        public void SetViewState(TaskData data)
        {
            TaskId = data.task_id;
            SetCompleteState(data.done);
        }

        private void SetCompleteState(bool completed)
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