using UnityEngine;
using Zenject;

namespace CockroachRunner
{
    public class FinishController : MonoBehaviour
    {
        [Inject] private EventsManager eventsManager;

        private void OnTriggerEnter(Collider other)
        {
            UnitMovable target = other.GetComponent<UnitMovable>();

            if (target != null && target.IsPlaying) 
            {
                target.Stop();
                eventsManager.InvokeEvent(GameEvents.UnitFinishedRace, target);
            }            
        }
    }
}