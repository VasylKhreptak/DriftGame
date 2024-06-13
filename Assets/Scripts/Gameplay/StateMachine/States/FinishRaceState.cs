using Data;
using Gameplay.InputService.Core;
using Gameplay.StateMachine.States.Core;
using Gameplay.Vehicles;
using Infrastructure.Services.Log.Core;
using Infrastructure.StateMachine.Main.Core;
using Infrastructure.StateMachine.Main.States.Core;
using UI.Gameplay.Windows.RaceFinished;

namespace Gameplay.StateMachine.States
{
    public class FinishRaceState : IGameplayState, IState
    {
        private readonly IStateMachine<IGameplayState> _stateMachine;
        private readonly IInputService _inputService;
        private readonly ILogService _logService;
        private readonly ReactiveHolder<Car> _carHolder;
        private readonly RaceFinishedWindow _raceFinishedWindow;

        public FinishRaceState(IStateMachine<IGameplayState> stateMachine, IInputService inputService, ILogService logService,
            ReactiveHolder<Car> carHolder, RaceFinishedWindow raceFinishedWindow)
        {
            _stateMachine = stateMachine;
            _inputService = inputService;
            _logService = logService;
            _carHolder = carHolder;
            _raceFinishedWindow = raceFinishedWindow;
        }

        public void Enter()
        {
            _logService.Log("FinishRaceState");

            _carHolder.Property.Value?.RigidbodyDecelerator.Decelerate();
            _inputService.Enabled = false;
            _raceFinishedWindow.Show();

            _inputService.Enabled = false;
        }
    }
}