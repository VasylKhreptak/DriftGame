using Gameplay.InputService.Core;
using Gameplay.StateMachine.States.Core;
using Infrastructure.Services.Log.Core;
using Infrastructure.StateMachine.Main.Core;
using Infrastructure.StateMachine.Main.States.Core;

namespace Gameplay.StateMachine.States
{
    public class FinishRaceState : IGameplayState, IState
    {
        private readonly IStateMachine<IGameplayState> _stateMachine;
        private readonly IInputService _inputService;
        private readonly ILogService _logService;

        public FinishRaceState(IStateMachine<IGameplayState> stateMachine, IInputService inputService, ILogService logService)
        {
            _stateMachine = stateMachine;
            _inputService = inputService;
            _logService = logService;
        }

        public void Enter()
        {
            _logService.Log("FinishRaceState");

            _inputService.Enabled = false;
        }
    }
}