using UI.Gameplay.Windows.RaceFinished;
using UI.Gameplay.Windows.RaceFinished.Buttons;
using UnityEngine;
using Zenject;

namespace Gameplay.Installers
{
    public class GameplayUIInstaller : MonoInstaller
    {
        [Header("References")]
        [SerializeField] private RaceFinishedWindow _raceFinishedWindow;
        [SerializeField] private DoubleRewardButton _doubleRewardButton;
        [SerializeField] private ContinueButton _continueButton;

        #region MonoBehaviour

        private void OnValidate()
        {
            _raceFinishedWindow ??= FindObjectOfType<RaceFinishedWindow>(true);
            _doubleRewardButton ??= FindObjectOfType<DoubleRewardButton>(true);
            _continueButton ??= FindObjectOfType<ContinueButton>(true);
        }

        #endregion

        public override void InstallBindings()
        {
            Container.BindInstance(_raceFinishedWindow).AsSingle();
            Container.BindInstance(_doubleRewardButton).AsSingle();
            Container.BindInstance(_continueButton).AsSingle();
        }
    }
}