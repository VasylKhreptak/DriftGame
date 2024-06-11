using Infrastructure.Services.StaticData.Core;
using Infrastructure.StateMachine.Game.States;
using Infrastructure.StateMachine.Game.States.Core;
using Infrastructure.StateMachine.Main.Core;
using UI.Buttons.Core;
using Zenject;

namespace UI.Garage.Buttons
{
    public class PlayButton : BaseButton
    {
        private IStateMachine<IGameState> _stateMachine;
        private IStaticDataService _staticDataService;

        [Inject]
        private void Constructor(IStateMachine<IGameState> stateMachine, IStaticDataService staticDataService)
        {
            _stateMachine = stateMachine;
            _staticDataService = staticDataService;
        }

        protected override void OnClicked()
        {
            LoadSceneAsyncState.Payload payload = new LoadSceneAsyncState.Payload
            {
                SceneName = _staticDataService.Config.GameplayScene.Name,
            };

            _stateMachine.Enter<LoadSceneWithTransitionAsyncState, LoadSceneAsyncState.Payload>(payload);

            Button.interactable = false;
        }
    }
}