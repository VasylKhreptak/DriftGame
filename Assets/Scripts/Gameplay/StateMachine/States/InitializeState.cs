using Gameplay.InputService.Core;
using Gameplay.StateMachine.States.Core;
using Infrastructure.Services.Log.Core;
using Infrastructure.StateMachine.Main.Core;
using Infrastructure.StateMachine.Main.States.Core;

namespace Gameplay.StateMachine.States
{
    public class InitializeState : IGameplayState, IState
    {
        private readonly IStateMachine<IGameplayState> _stateMachine;
        private readonly IInputService _inputService;
        private readonly ILogService _logService;

        public InitializeState(IStateMachine<IGameplayState> stateMachine, IInputService inputService, ILogService logService)
        {
            _stateMachine = stateMachine;
            _inputService = inputService;
            _logService = logService;
        }

        public void Enter()
        {
            _logService.Log("InitializeState");
            
            _inputService.Enabled = false;
            _stateMachine.Enter<InitializeConnectionState>();
        }
    }
}