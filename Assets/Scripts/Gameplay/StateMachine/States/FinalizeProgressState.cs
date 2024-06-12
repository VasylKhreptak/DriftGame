using Gameplay.StateMachine.States.Core;
using Infrastructure.Services.Log.Core;
using Infrastructure.Services.PersistentData.Core;
using Infrastructure.StateMachine.Game.States.Core;
using Infrastructure.StateMachine.Main.Core;
using Infrastructure.StateMachine.Main.States.Core;

namespace Gameplay.StateMachine.States
{
    public class FinalizeProgressState : IGameplayState, IState
    {
        private readonly IPersistentDataService _persistentDataService;
        private readonly IStateMachine<IGameState> _gameStateMachine;
        private readonly ILogService _logService;

        public FinalizeProgressState(IPersistentDataService persistentDataService, IStateMachine<IGameState> gameStateMachine, ILogService logService)
        {
            _persistentDataService = persistentDataService;
            _gameStateMachine = gameStateMachine;
            _logService = logService;
        }

        public void Enter()
        {
            _logService.Log("FinalizeProgressState");
        }
    }
}