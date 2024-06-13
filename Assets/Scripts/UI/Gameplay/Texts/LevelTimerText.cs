using System;
using Gameplay.TimeManagement;
using UI.Texts.Core;
using UniRx;
using UnityEngine;
using Zenject;

namespace UI.Gameplay.Texts
{
    public class LevelTimerText : ReactiveText
    {
        private LevelTimer _levelTimer;

        [Inject]
        private void Constructor(LevelTimer levelTimer) => _levelTimer = levelTimer;

        protected override IObservable<string> GetObservable() => _levelTimer.RemainingTime.Select(ToTimeString);

        private string ToTimeString(float totalSeconds)
        {
            int minutes = Mathf.FloorToInt(totalSeconds / 60f);
            int seconds = Mathf.FloorToInt(totalSeconds % 60f);
            return $"{minutes:00}:{seconds:00}";
        }
    }
}