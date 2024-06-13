using Gameplay.Data;
using Infrastructure.Services.Advertisement.Core;
using Infrastructure.Services.ToastMessage.Core;
using UI.Buttons.Core;
using Zenject;

namespace UI.Gameplay.Windows.RaceFinished.Buttons
{
    public class DoubleRewardButton : BaseButton
    {
        private GameplayData _gameplayData;
        private ContinueButton _continueButton;
        private IToastMessageService _toastMessageService;
        private IAdvertisementService _advertisementService;

        [Inject]
        private void Constructor(GameplayData gameplayData, ContinueButton continueButton, IToastMessageService toastMessageService,
            IAdvertisementService advertisementService)
        {
            _gameplayData = gameplayData;
            _continueButton = continueButton;
            _toastMessageService = toastMessageService;
            _advertisementService = advertisementService;
        }

        public bool Enabled
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        #region MonoBehaviour

        private void OnDestroy()
        {
            base.OnDisable();

            _advertisementService.OnRewarded -= DoubleScore;
        }

        #endregion

        protected override void OnClicked()
        {
            _advertisementService.OnRewarded -= DoubleScore;

            if (_advertisementService.ShowRewardedVideo())
                _advertisementService.OnRewarded += DoubleScore;
            else
                _toastMessageService.Send("No video available");

            Enabled = false;
            _continueButton.Enabled = true;
        }

        private void DoubleScore() => _gameplayData.Score.Add(_gameplayData.Score.Amount.Value);
    }
}