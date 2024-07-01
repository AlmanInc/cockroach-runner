using System;

namespace CockroachRunner
{
    [Serializable]
    public struct SerializableItem<TId, TItem>
    {
        public TId id;
        public TItem item;
    }
}