using UnityEngine.UI;
using UnityEngine;

namespace CockroachRunner
{
    public class ProgressBarView : MonoBehaviour
    {
        [SerializeField] private Image imageProgress;
        [SerializeField] private Text labelProgress;

        public void SetProgress(float progress)
        {
            imageProgress.fillAmount = progress;

            if (labelProgress != null ) 
            { 
                labelProgress.text = $"{Mathf.RoundToInt(progress * 100f)}%";
            }
        }        
    }
}