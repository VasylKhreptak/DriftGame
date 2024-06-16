using Cars.Customization;
using Garage.SpawnPoints;
using Infrastructure.Data.Static;
using Infrastructure.Services.PersistentData.Core;
using Infrastructure.Services.StaticData.Core;
using UnityEngine;

namespace Garage
{
    public class CarSelector
    {
        private readonly CarSpawnPoint _carSpawnPoint;
        private readonly Prefabs _prefabs;
        private readonly IPersistentDataService _persistentDataService;

        public CarSelector(CarSpawnPoint carSpawnPoint, IStaticDataService staticDataService, IPersistentDataService persistentDataService)
        {
            _carSpawnPoint = carSpawnPoint;
            _persistentDataService = persistentDataService;
            _prefabs = staticDataService.Prefabs;
        }

        private GameObject _currentCar;

        public void Select(CarModel model)
        {
            if (_currentCar != null)
                Object.Destroy(_currentCar);

            Transform point = _carSpawnPoint.Point;

            _currentCar = Object.Instantiate(_prefabs.Cars[model], point.position, point.rotation, null);

            _persistentDataService.Data.PlayerData.SelectedCarModel = model;
        }
    }
}