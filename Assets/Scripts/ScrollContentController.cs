using System.Collections.Generic;
using UnityEngine;

namespace CockroachRunner
{
    public class ScrollContentController : MonoBehaviour
    {
        [SerializeField] private RectTransform content;
        [SerializeField] private int initializeAmount;
        [SerializeField] private ReferalLineView prefab;
        [SerializeField] private float itemHeight;

        private List<ReferalLineView> lines;

        private Vector2 startPosition;
        private bool wasInitialized;
        private float yPointer;
        private int contentIndex;
        private int totalReferals;
                
        public void SetMaxReferalCount(int count)
        {
            if (!wasInitialized)
            {
                lines = new List<ReferalLineView>();

                startPosition = content.anchoredPosition;

                content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, count * itemHeight);
                totalReferals = count;

                GenerateScrollList();
                                
                wasInitialized = true;
            }

            content.anchoredPosition = startPosition;
            yPointer = content.anchoredPosition.y;
            contentIndex = 0;
        }

        public void ChangeScrollBar(Vector2 value)
        {
            yPointer = content.anchoredPosition.y;

            int index = GetIndex(yPointer);

            if (index > contentIndex)
            {
                ReferalLineView item = lines[0];
                lines.RemoveAt(0);
                item.CachedTransform.localPosition = lines[lines.Count - 1].CachedTransform.localPosition + Vector3.down * itemHeight;
                item.SetData(lines[lines.Count - 1].Index + 1);
                lines.Add(item);

                contentIndex++;
            }

            if (contentIndex > index)
            {
                ReferalLineView item = lines[lines.Count - 1];
                lines.RemoveAt(lines.Count - 1);
                item.CachedTransform.localPosition = lines[0].CachedTransform.localPosition + Vector3.up * itemHeight;
                item.SetData(lines[0].Index - 1);
                lines.Insert(0, item);

                contentIndex--;
            }
        }

        private void GenerateScrollList()
        {
            int count = totalReferals <= initializeAmount ? totalReferals : initializeAmount;

            for (int i = 0; i < count; i++)
            {
                ReferalLineView item = Instantiate(prefab, content).GetComponent<ReferalLineView>();
                item.CachedTransform.localPosition = Vector3.down * itemHeight * i;
                item.SetData(i);

                lines.Add(item);
            }
        }

        public int GetIndex(float y)
        {
            return Mathf.FloorToInt(y / itemHeight);
        }
    }
}