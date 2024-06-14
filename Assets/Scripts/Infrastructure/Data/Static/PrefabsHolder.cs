using System;
using Infrastructure.Serialization;
using UnityEngine;

namespace Infrastructure.Data.Static
{
    [Serializable]
    public class PrefabsHolder<TPrefabType> where TPrefabType : Enum
    {
        [SerializeField] private SerializedDictionary<TPrefabType, GameObject> _prefabs = new SerializedDictionary<TPrefabType, GameObject>();

        public GameObject this[TPrefabType prefab] => _prefabs[prefab];
    }
}