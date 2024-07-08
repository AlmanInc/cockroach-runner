using System.Collections.Generic;
using UnityEngine;

namespace CockroachRunner
{
    public class ArrayUniqueIndexGrabber
    {
        private List<int> list;

        public int Length => list.Count;

        public ArrayUniqueIndexGrabber()
        {
            list = new List<int>();
        }

        public void Activate(int capacity)
        {
            Clear();

            for (int i = 0; i < capacity; i++)
            {
                list.Add(i);
            }
        }

        public int NexIndex()
        {
            if (list.Count == 0)
            {
                return -1;
            }

            int index = Random.Range(0, list.Count);
            int result = list[index];
            list.RemoveAt(index);

            return result;
        }

        public void Clear() => list.Clear();
    }
}