using UI.Gameplay.Windows;
using UI.Gameplay.Windows.RaceFinished;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class GameplayUIInstaller : MonoInstaller
    {
        [Header("References")]
        [SerializeField] private RaceFinishedWindow _raceFinishedWindow;

        #region MonoBehaviour

        private void OnValidate()
        {
            _raceFinishedWindow ??= FindObjectOfType<RaceFinishedWindow>(true);
        }

        #endregion

        public override void InstallBindings()
        {
            Container.BindInstance(_raceFinishedWindow).AsSingle();
        }
    }
}