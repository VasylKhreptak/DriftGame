using System.Collections.Generic;
using Infrastructure.Services.Log.Core;
using Infrastructure.Services.StaticData.Core;
using Infrastructure.StateMachine.Game.States.Core;
using Infrastructure.StateMachine.Main.Core;
using Infrastructure.StateMachine.Main.States.Core;
using Photon.Pun;
using UnityEngine;

namespace Infrastructure.StateMachine.Game.States
{
    public class InitializePhotonPrefabsState : IGameState, IState
    {
        private readonly ILogService _logService;
        private readonly IStaticDataService _staticDataService;
        private readonly IStateMachine<IGameState> _stateMachine;

        public InitializePhotonPrefabsState(ILogService logService, IStaticDataService staticDataService, IStateMachine<IGameState> stateMachine)
        {
            _logService = logService;
            _staticDataService = staticDataService;
            _stateMachine = stateMachine;
        }

        private bool _initialized;

        public void Enter()
        {
            _logService.Log("InitializePhotonPrefabsState");

            if (_initialized)
                return;

            Initialize(_staticDataService.Prefabs.General.AsDictionary().Values);
            Initialize(_staticDataService.Prefabs.Cars.AsDictionary().Values);

            _stateMachine.Enter<BootstrapFirebaseState>();
        }

        private void Initialize(IEnumerable<GameObject> prefabs)
        {
            if (PhotonNetwork.PrefabPool is DefaultPool pool)
                foreach (GameObject prefab in prefabs)
                    pool.ResourceCache.Add(prefab.name, prefab);

            _initialized = true;
        }
    }
}