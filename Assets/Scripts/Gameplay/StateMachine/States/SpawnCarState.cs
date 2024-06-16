using Cars.Customization;
using Data;
using Gameplay.Cars;
using Gameplay.SpawnPoints;
using Gameplay.StateMachine.States.Core;
using Infrastructure.Data.Static;
using Infrastructure.Data.Static.Core;
using Infrastructure.Services.Log.Core;
using Infrastructure.Services.StaticData.Core;
using Infrastructure.StateMachine.Main.Core;
using Infrastructure.StateMachine.Main.States.Core;
using UnityEngine;
using Zenject;

namespace Gameplay.StateMachine.States
{
    public class SpawnCarState : IGameplayState, IState
    {
        private readonly IStateMachine<IGameplayState> _stateMachine;
        private readonly ILogService _logService;
        private readonly Prefabs _prefabs;
        private readonly IInstantiator _instantiator;
        private readonly CarSpawnPoints _carSpawnPoints;
        private readonly ReactiveHolder<Car> _carReactiveHolder;

        public SpawnCarState(IStateMachine<IGameplayState> stateMachine, ILogService logService, IStaticDataService staticDataService,
            IInstantiator instantiator, CarSpawnPoints carSpawnPoints, ReactiveHolder<Car> carReactiveHolder)
        {
            _stateMachine = stateMachine;
            _logService = logService;
            _instantiator = instantiator;
            _carSpawnPoints = carSpawnPoints;
            _carReactiveHolder = carReactiveHolder;
            _prefabs = staticDataService.Prefabs;
        }

        public void Enter()
        {
            _logService.Log("SpawnCarsState");

            SpawnCar();

            _stateMachine.Enter<WarmUpState>();
        }

        private void SpawnCar()
        {
            Transform spawnPoint = _carSpawnPoints[0];
            Car car = _instantiator.InstantiatePrefabForComponent<Car>(_prefabs.Cars[CarModel.Base], spawnPoint.position, spawnPoint.rotation, null);
            CameraWrapper camera = _instantiator.InstantiatePrefabForComponent<CameraWrapper>(_prefabs.General[Prefab.CarCamera]);
            camera.SetTarget(car.transform);
            _carReactiveHolder.Property.Value = car;
        }
    }
}