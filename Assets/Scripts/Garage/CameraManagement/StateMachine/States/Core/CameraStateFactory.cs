using System;
using System.Collections.Generic;
using Infrastructure.StateMachine.Main.States.Core;
using Infrastructure.StateMachine.Main.States.Factory;
using Zenject;

namespace Garage.CameraManagement.StateMachine.States.Core
{
    public class CameraStateFactory : StateFactory
    {
        public CameraStateFactory(DiContainer container) : base(container) { }

        protected override Dictionary<Type, Func<IBaseState>> BuildStatesMap() =>
            new Dictionary<Type, Func<IBaseState>>
            {
                [typeof(OrbitingState)] = _container.Resolve<OrbitingState>,
                [typeof(LookAtState)] = _container.Resolve<LookAtState>,
            };
    }
}