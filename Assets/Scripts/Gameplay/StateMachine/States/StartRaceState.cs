using System;
using Gameplay.InputService.Core;
using Gameplay.StateMachine.States.Core;
using Gameplay.TimeManagement;
using Infrastructure.Data.Static;
using Infrastructure.Services.Log.Core;
using Infrastructure.Services.StaticData.Core;
using Infrastructure.StateMachine.Main.Core;
using Infrastructure.StateMachine.Main.States.Core;
using UniRx;

namespace Gameplay.StateMachine.States
{
    public class StartRaceState : IGameplayState, IState, IExitable
    {
        private readonly IStateMachine<IGameplayState> _stateMachine;
        private readonly ILogService _logService;
        private readonly Balance _balance;
        private readonly IInputService _inputService;
        private readonly LevelTimer _levelTimer;

        public StartRaceState(IStateMachine<IGameplayState> stateMachine, ILogService logService, IStaticDataService staticDataService,
            IInputService inputService, LevelTimer levelTimer)
        {
            _stateMachine = stateMachine;
            _logService = logService;
            _inputService = inputService;
            _levelTimer = levelTimer;
            _balance = staticDataService.Balance;
        }

        private IDisposable _timerSubscription;

        public void Enter()
        {
            _logService.Log("StartRaceState");

            _inputService.Enabled = true;
            
            _levelTimer.Start(_balance.RaceDuration);
            _timerSubscription = _levelTimer.OnCompleted.Subscribe(_ => OnTimerElapsed());
        }

        public void Exit()
        {
            _timerSubscription?.Dispose();
            _levelTimer.Reset();
        }

        private void OnTimerElapsed() => _stateMachine.Enter<FinishRaceState>();
    }
}