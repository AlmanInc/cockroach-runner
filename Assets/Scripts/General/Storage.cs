using UnityEngine;

namespace CockroachRunner
{
    public class Storage<TId, TItem> : ScriptableObject
    {
        [SerializeField]
        protected SerializableItem<TId, TItem>[] storage;

        public TItem GetItem(TId id)
        {
            foreach (var item in storage)
            {
                if (item.id.Equals(id))
                {
                    return item.item;
                }
            }

            return default;
        }
    }
}