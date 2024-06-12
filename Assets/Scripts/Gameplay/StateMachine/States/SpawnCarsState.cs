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
    public class SpawnCarsState : IGameplayState, IState
    {
        private readonly IStateMachine<IGameplayState> _stateMachine;
        private readonly ILogService _logService;
        private readonly Prefabs _prefabs;
        private readonly IInstantiator _instantiator;
        private readonly CarSpawnPoints _carSpawnPoints;

        public SpawnCarsState(IStateMachine<IGameplayState> stateMachine, ILogService logService, IStaticDataService staticDataService,
            IInstantiator instantiator, CarSpawnPoints carSpawnPoints)
        {
            _stateMachine = stateMachine;
            _logService = logService;
            _instantiator = instantiator;
            _carSpawnPoints = carSpawnPoints;
            _prefabs = staticDataService.Prefabs;
        }

        public void Enter()
        {
            _logService.Log("SpawnCarsState");

            Transform spawnPoint = _carSpawnPoints[0];
            Transform car = _instantiator.InstantiatePrefab(_prefabs[Prefab.BaseCar], spawnPoint.position, spawnPoint.rotation, null).transform;
            CameraWrapper camera = _instantiator.InstantiatePrefabForComponent<CameraWrapper>(_prefabs[Prefab.CarCamera]);
            camera.SetTarget(car);

            _stateMachine.Enter<WarmUpState>();
        }
    }
}