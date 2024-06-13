using System;
using Gameplay.Data;
using UI.Texts.Core;
using UniRx;
using UnityEngine;
using Zenject;

namespace UI.Gameplay.Texts
{
    public class ScoreText : ReactiveText
    {
        [Header("Preferences")]
        [SerializeField] private string _format = "Score: {0}";

        private GameplayData _gameplayData;

        [Inject]
        private void Constructor(GameplayData gameplayData)
        {
            _gameplayData = gameplayData;
        }

        protected override IObservable<string> GetObservable() => _gameplayData.Score.Amount.Select(score => string.Format(_format, score));
    }
}