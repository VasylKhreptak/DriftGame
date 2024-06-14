using System;

namespace Infrastructure.Serialization
{
    [Serializable]
    public class KeyValuePair<TKey, TValue>
    {
        public TKey Key;
        public TValue Value;
    }
}