using System;
using System.Collections.Generic;

namespace Infrastructure.Serialization
{
    [Serializable]
    public class KeyValuePairs<TKey, TValue>
    {
        public List<KeyValuePair<TKey, TValue>> Pairs = new List<KeyValuePair<TKey, TValue>>();

        public Dictionary<TKey, TValue> ToDictionary()
        {
            Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>();

            foreach (KeyValuePair<TKey, TValue> pair in Pairs)
            {
                dictionary[pair.Key] = pair.Value;
            }

            return dictionary;
        }
    }
}