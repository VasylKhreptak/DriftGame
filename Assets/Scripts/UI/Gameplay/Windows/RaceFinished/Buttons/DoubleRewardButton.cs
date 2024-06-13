using Gameplay.Data;
using UI.Buttons.Core;
using Zenject;

namespace UI.Gameplay.Windows.RaceFinished.Buttons
{
    public class DoubleRewardButton : BaseButton
    {
        private GameplayData _gameplayData;
        private ContinueButton _continueButton;

        [Inject]
        private void Constructor(GameplayData gameplayData, ContinueButton continueButton)
        {
            _gameplayData = gameplayData;
            _continueButton = continueButton;
        }

        public bool Enabled
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        protected override void OnClicked()
        {
            DoubleScore();
            Enabled = false;
            _continueButton.Enabled = true;
        }

        private void DoubleScore() => _gameplayData.Score.Add(_gameplayData.Score.Amount.Value);
    }
}