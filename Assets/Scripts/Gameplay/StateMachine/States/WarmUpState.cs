using System;
using GameAnalyticsSDK;
using Gameplay.StateMachine.States.Core;
using Gameplay.TimeManagement;
using Infrastructure.Data.Static;
using Infrastructure.Services.Log.Core;
using Infrastructure.Services.StaticData.Core;
using Infrastructure.StateMachine.Main.Core;
using Infrastructure.StateMachine.Main.States.Core;
using Infrastructure.Transition.Core;
using UniRx;

namespace Gameplay.StateMachine.States
{
    public class WarmUpState : IGameplayState, IState, IExitable
    {
        private readonly IStateMachine<IGameplayState> _stateMachine;
        private readonly ILogService _logService;
        private readonly LevelTimer _levelTimer;
        private readonly Balance _balance;
        private readonly ITransitionScreen _transitionScreen;

        public WarmUpState(IStateMachine<IGameplayState> stateMachine, ILogService logService, LevelTimer levelTimer,
            IStaticDataService staticDataService, ITransitionScreen transitionScreen)
        {
            _stateMachine = stateMachine;
            _logService = logService;
            _levelTimer = levelTimer;
            _transitionScreen = transitionScreen;
            _balance = staticDataService.Balance;
        }

        private IDisposable _timerSubscription;
        private IDisposable _screenFadeProgressSubscription;

        public void Enter()
        {
            _logService.Log("WarmUpState");

            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "WarmUp");

            InitializeTimer();
            WaitUntilScreenFade();
        }

        public void Exit()
        {
            _timerSubscription?.Dispose();
            _screenFadeProgressSubscription?.Dispose();
            _levelTimer.Reset();
        }

        private void InitializeTimer()
        {
            _levelTimer.Start(_balance.WarmUpDuration);
            _levelTimer.Pause();
        }

        private void WaitUntilScreenFade()
        {
            _screenFadeProgressSubscription = _transitionScreen.FadeProgress
                .Where(progress => progress >= 1)
                .First()
                .Subscribe(_ => ResumeTimer());
        }

        private void ResumeTimer()
        {
            _levelTimer.Resume();
            _timerSubscription = _levelTimer.OnCompleted.Subscribe(_ => OnTimerElapsed());
        }

        private void OnTimerElapsed()
        {
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "WarmUp");
            _stateMachine.Enter<StartRaceState>();
        }
    }
}