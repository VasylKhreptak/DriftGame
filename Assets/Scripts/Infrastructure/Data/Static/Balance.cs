using UnityEngine;

namespace Infrastructure.Data.Static
{
    [CreateAssetMenu(fileName = "GameBalance", menuName = "ScriptableObjects/Static/GameBalance", order = 0)]
    public class Balance : ScriptableObject
    {
        [Header("Gameplay")]
        [SerializeField] private int _warmUpDuration = 3;
        [SerializeField] private int _raceDuration = 120;

        public int WarmUpDuration => _warmUpDuration;
        public int RaceDuration => _raceDuration;
    }
}