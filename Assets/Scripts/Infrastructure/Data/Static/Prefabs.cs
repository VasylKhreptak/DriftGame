using Cars.Customization;
using Infrastructure.Data.Static.Core;
using UnityEngine;

namespace Infrastructure.Data.Static
{
    [CreateAssetMenu(fileName = "GamePrefabs", menuName = "ScriptableObjects/Static/GamePrefabs", order = 0)]
    public class Prefabs : ScriptableObject
    {
        [SerializeField] private PrefabsHolder<Prefab> _general = new PrefabsHolder<Prefab>();
        [SerializeField] private PrefabsHolder<CarModel> _cars = new PrefabsHolder<CarModel>();

        public PrefabsHolder<Prefab> General => _general;
        public PrefabsHolder<CarModel> Cars => _cars;
    }
}