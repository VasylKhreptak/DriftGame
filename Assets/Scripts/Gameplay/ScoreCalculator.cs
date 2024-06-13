using System;
using CarPhysics;
using Data;
using Gameplay.Data;
using Infrastructure.Services.StaticData.Core;
using UniRx;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class ScoreCalculator : IInitializable, IDisposable
    {
        private readonly GameplayData _gameplayData;
        private readonly ReactiveHolder<Car> _carHolder;
        private readonly Preferences _preferences;

        public ScoreCalculator(GameplayData gameplayData, ReactiveHolder<Car> carHolder, IStaticDataService staticDataService)
        {
            _gameplayData = gameplayData;
            _carHolder = carHolder;
            _preferences = staticDataService.Balance.ScorePreferences;
        }

        private IDisposable _carSubscription;
        private IDisposable _driftSubscription;
        private IDisposable _scoreSubscription;

        public void Initialize()
        {
            _carSubscription = _carHolder.Property.Subscribe(OnCarChanged);
        }

        public void Dispose()
        {
            _carSubscription.Dispose();
            _driftSubscription?.Dispose();
            _scoreSubscription?.Dispose();
        }

        private void OnCarChanged(Car car)
        {
            _driftSubscription?.Dispose();

            if (car == null)
                return;

            _driftSubscription = car.DriftObserver.IsDrifting.Subscribe(OnIsDriftingChanged);
        }

        private void OnIsDriftingChanged(bool isDrifting)
        {
            _scoreSubscription?.Dispose();

            if (isDrifting == false)
                return;

            _gameplayData.Score.Add(_preferences.StartScore);

            _scoreSubscription = Observable
                .Interval(TimeSpan.FromSeconds(_preferences.Interval))
                .Subscribe(_ => _gameplayData.Score.Add(_preferences.ScorePerInterval));
        }

        [Serializable]
        public class Preferences
        {
            [SerializeField] private int _startScore = 10;
            [SerializeField] private float _interval = 0.1f;
            [SerializeField] private int _scorePerInterval = 5;

            public int StartScore => _startScore;
            public float Interval => _interval;
            public int ScorePerInterval => _scorePerInterval;
        }
    }
}