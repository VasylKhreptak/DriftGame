using GameAnalyticsSDK;
using Infrastructure.Services.Log.Core;
using Infrastructure.Services.PersistentData.Core;
using Infrastructure.StateMachine.Game.States.Core;
using Infrastructure.StateMachine.Main.Core;
using Infrastructure.StateMachine.Main.States.Core;

namespace Infrastructure.StateMachine.Game.States
{
    public class BootstrapAnalyticsState : IState, IGameState, IExitable
    {
        private readonly IStateMachine<IGameState> _gameStateMachine;
        private readonly IPersistentDataService _persistentDataService;
        private readonly ILogService _logService;

        public BootstrapAnalyticsState(IStateMachine<IGameState> gameStateMachine, IPersistentDataService persistentDataService,
            ILogService logService)
        {
            _gameStateMachine = gameStateMachine;
            _persistentDataService = persistentDataService;
            _logService = logService;
        }

        public void Enter()
        {
            _logService.Log("BootstrapAnalyticsState");
            _persistentDataService.Data.AnalyticsData.SessionsCount++;

            GameAnalytics.onInitialize += OnInitialized;

            GameAnalytics.Initialize();
        }

        public void Exit() => GameAnalytics.onInitialize -= OnInitialized;

        private void OnInitialized(object obj, bool isInitialized)
        {
            GameAnalytics.onInitialize -= OnInitialized;

            _logService.Log(isInitialized ? "GameAnalytics initialized" : "GameAnalytics failed to initialize");

            _gameStateMachine.Enter<BootstrapAdvertisementsState>();
        }
    }
}