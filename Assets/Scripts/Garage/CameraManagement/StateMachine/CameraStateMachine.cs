using Garage.CameraManagement.StateMachine.States.Core;
using Infrastructure.StateMachine.Main;

namespace Garage.CameraManagement.StateMachine
{
    public class CameraStateMachine : StateMachine<ICameraState>
    {
        protected CameraStateMachine(CameraStateFactory stateFactory) : base(stateFactory) { }
    }
}