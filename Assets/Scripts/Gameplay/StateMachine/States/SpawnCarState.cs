using Cars.Customization;
using Cysharp.Threading.Tasks;
using Data;
using Gameplay.Cars;
using Gameplay.Multiplayer;
using Gameplay.StateMachine.States.Core;
using Infrastructure.Data.Static;
using Infrastructure.Data.Static.Core;
using Infrastructure.Services.Log.Core;
using Infrastructure.Services.StaticData.Core;
using Infrastructure.StateMachine.Main.Core;
using Infrastructure.StateMachine.Main.States.Core;
using Photon.Pun;
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
        private readonly ReactiveHolder<Car> _carReactiveHolder;
        private readonly CarSpawnPointProvider _spawnPointProvider;

        public SpawnCarState(IStateMachine<IGameplayState> stateMachine, ILogService logService, IStaticDataService staticDataService,
            IInstantiator instantiator, ReactiveHolder<Car> carReactiveHolder, CarSpawnPointProvider spawnPointProvider)
        {
            _stateMachine = stateMachine;
            _logService = logService;
            _instantiator = instantiator;
            _carReactiveHolder = carReactiveHolder;
            _spawnPointProvider = spawnPointProvider;
            _prefabs = staticDataService.Prefabs;
        }

        public async void Enter()
        {
            _logService.Log("SpawnCarsState");

            await SpawnCar();

            _stateMachine.Enter<WarmUpState>();
        }

        private async UniTask SpawnCar()
        {
            Transform spawnPoint = await _spawnPointProvider.Get();
            GameObject carObject = PhotonNetwork.Instantiate(_prefabs.Cars[CarModel.Base].name, spawnPoint.position, spawnPoint.rotation);
            Car car = carObject.GetComponent<Car>();
            CameraWrapper camera = _instantiator.InstantiatePrefabForComponent<CameraWrapper>(_prefabs.General[Prefab.CarCamera]);
            camera.SetTarget(car.transform);
            _carReactiveHolder.Property.Value = car;
        }
    }
}