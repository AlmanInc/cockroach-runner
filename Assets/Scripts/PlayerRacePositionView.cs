using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using Zenject;

namespace CockroachRunner
{
    public class PlayerRacePositionView : MonoBehaviour
    {
        [SerializeField] private Text labelPosition;
        [SerializeField] private float checkDelay;
        [SerializeField] private UnitMovable player;
        [SerializeField] private UnitMovable[] bots;

        [Inject] private GameState gameState;

        private Coroutine checkPlaceCoroutine;

        private void OnEnable()
        {
            if (checkPlaceCoroutine != null)
            {
                StopCoroutine(checkPlaceCoroutine);
            }

            checkPlaceCoroutine = StartCoroutine(SetPositionProcess());
        }

        private void OnDisable()
        {
            if (checkPlaceCoroutine != null)
            {
                StopCoroutine(checkPlaceCoroutine);
                checkPlaceCoroutine = null;
            }
        }

        private IEnumerator SetPositionProcess()
        {
            yield return new WaitForEndOfFrame();

            while (true) 
            {
                int position = gameState.FinishedOpponentsCount + 1;

                foreach (var bot in bots) 
                {
                    if (bot.IsPlaying && bot.CachedTransform.position.z > player.CachedTransform.position.z) 
                    {
                        position++;
                    }

                    yield return null;
                }

                switch (position)
                {
                    case 1:
                        labelPosition.text = "1st";
                        break;

                    case 2:
                        labelPosition.text = "2nd";
                        break;

                    case 3:
                        labelPosition.text = "3rd";
                        break;

                    default:
                        labelPosition.text = $"{position}th";
                        break;
                }
                
                yield return new WaitForSeconds(checkDelay);
            }
        }
    }
}