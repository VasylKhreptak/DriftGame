using System;
using Gameplay.Data;
using Gameplay.StateMachine.States.Core;
using Infrastructure.Services.Log.Core;
using Infrastructure.Services.PersistentData.Core;
using Infrastructure.Services.StaticData.Core;
using Infrastructure.StateMachine.Game.States;
using Infrastructure.StateMachine.Game.States.Core;
using Infrastructure.StateMachine.Main.Core;
using Infrastructure.StateMachine.Main.States.Core;

namespace Gameplay.StateMachine.States
{
    public class FinalizeProgressAndLoadGarageState : IGameplayState, IState
    {
        private readonly IPersistentDataService _persistentDataService;
        private readonly IStateMachine<IGameState> _gameStateMachine;
        private readonly ILogService _logService;
        private readonly GameplayData _gameplayData;
        private readonly IStaticDataService _staticDataService;

        public FinalizeProgressAndLoadGarageState(IPersistentDataService persistentDataService, IStateMachine<IGameState> gameStateMachine,
            ILogService logService, GameplayData gameplayData, IStaticDataService staticDataService)
        {
            _persistentDataService = persistentDataService;
            _gameStateMachine = gameStateMachine;
            _logService = logService;
            _gameplayData = gameplayData;
            _staticDataService = staticDataService;
        }

        public void Enter()
        {
            _logService.Log("FinalizeProgressState");

            RegisterScore();
            LoadGarage();
        }

        private void RegisterScore() => _persistentDataService.Data.PlayerData.Coins.Add(_gameplayData.Score.Amount.Value);

        private void LoadGarage()
        {
            LoadSceneAsyncState.Payload payload = new LoadSceneAsyncState.Payload()
            {
                SceneName = _staticDataService.Config.GarageScene.Name,
                OnComplete = SaveGame,
            };

            _gameStateMachine.Enter<LoadSceneWithTransitionAsyncState, LoadSceneAsyncState.Payload>(payload);
        }

        private void SaveGame() => _gameStateMachine.Enter<SaveDataState, Action>(null);
    }
}