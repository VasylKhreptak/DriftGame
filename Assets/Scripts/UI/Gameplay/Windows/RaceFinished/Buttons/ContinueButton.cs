using System;
using Gameplay.StateMachine.States;
using Gameplay.StateMachine.States.Core;
using Infrastructure.StateMachine.Main.Core;
using UI.Buttons.Core;
using Zenject;

namespace UI.Gameplay.Windows.RaceFinished.Buttons
{
    public class ContinueButton : BaseButton
    {
        private IStateMachine<IGameplayState> _gameplayStateMachine;

        [Inject]
        private void Constructor(IStateMachine<IGameplayState> gameplayStateMachine)
        {
            _gameplayStateMachine = gameplayStateMachine;
        }

        public bool Enabled
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        protected override void OnClicked()
        {
            Button.interactable = false;
            _gameplayStateMachine.Enter<FinalizeProgressAndLoadGarageState>();
        }
    }
}