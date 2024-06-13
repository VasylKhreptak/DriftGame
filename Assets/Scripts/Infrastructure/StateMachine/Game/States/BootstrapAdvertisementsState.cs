using Infrastructure.Services.Advertisement.Core;
using Infrastructure.Services.Log.Core;
using Infrastructure.StateMachine.Game.States.Core;
using Infrastructure.StateMachine.Main.Core;
using Infrastructure.StateMachine.Main.States.Core;

namespace Infrastructure.StateMachine.Game.States
{
    public class BootstrapAdvertisementsState : IGameState, IState, IExitable
    {
        private const float Timeout = 2f;

        private readonly IStateMachine<IGameState> _stateMachine;
        private readonly ILogService _logService;
        private readonly IAdvertisementService _advertisementService;

        public BootstrapAdvertisementsState(IStateMachine<IGameState> stateMachine, ILogService logService,
            IAdvertisementService advertisementService)
        {
            _stateMachine = stateMachine;
            _logService = logService;
            _advertisementService = advertisementService;
        }

        public void Enter()
        {
            _logService.Log("BootstrapAdvertisementsState");

            _advertisementService.OnInitialized += OnInitialized;

            _advertisementService.Initialize();
        }

        public void Exit() => _advertisementService.OnInitialized -= OnInitialized;

        private void OnInitialized(bool success)
        {
            _advertisementService.OnInitialized -= OnInitialized;

            _logService.Log(success ? "Advertisement service initialized successfully" : "Advertisement service failed to initialize");

            _stateMachine.Enter<FinalizeBootstrapState>();
        }
    }
}