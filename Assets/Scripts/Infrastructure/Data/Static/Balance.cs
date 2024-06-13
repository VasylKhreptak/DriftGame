using Gameplay;
using UnityEngine;

namespace Infrastructure.Data.Static
{
    [CreateAssetMenu(fileName = "GameBalance", menuName = "ScriptableObjects/Static/GameBalance", order = 0)]
    public class Balance : ScriptableObject
    {
        [Header("Gameplay")]
        [SerializeField] private int _warmUpDuration = 3;
        [SerializeField] private int _raceDuration = 120;

        [Header("Score")]
        [SerializeField] private ScoreCalculator.Preferences _scorePreferences;

        public int WarmUpDuration => _warmUpDuration;
        public int RaceDuration => _raceDuration;

        public ScoreCalculator.Preferences ScorePreferences => _scorePreferences;
    }
}