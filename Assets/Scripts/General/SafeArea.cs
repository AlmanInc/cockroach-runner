using UnityEngine;

namespace CockroachRunner
{
    public class SafeArea : MonoBehaviour
    {
        private void Awake()
        {
            RectTransform rectTransform = GetComponent<RectTransform>();

            if (rectTransform == null)
                return;

            Rect safeArea = Screen.safeArea;

            Vector2 anchorMin = safeArea.position;
            Vector2 anchorMax = anchorMin + safeArea.size;

            anchorMin.x /= Screen.width;
            anchorMin.y /= Screen.height;

            anchorMax.x /= Screen.width;
            anchorMax.y /= Screen.height;

            rectTransform.anchorMin = anchorMin;
            rectTransform.anchorMax = anchorMax;
        }
    }
}