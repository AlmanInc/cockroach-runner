using System.Collections;
using UnityEngine.UI;
using UnityEngine;

namespace CockroachRunner
{
    public class BlockPanelController : MonoBehaviour
    {
        [SerializeField] private Text labelBackCount;
        [SerializeField] private int durarion = 3;

        private void OnEnable()
        {
            StartCoroutine(BackCountProcess());
        }

        private void OnDisable()
        {
            labelBackCount.text = $"{durarion}";
        }

        private IEnumerator BackCountProcess()
        {
            float time = durarion;
            labelBackCount.text = $"{time}";

            while (time > 0) 
            {                
                yield return new WaitForSeconds(1f);
                
                time -= 1;
                labelBackCount.text = $"{time}";
            }

            //yield return new WaitForSeconds(0.5f);
            
            gameObject.SetActive(false);
        }
    }
}