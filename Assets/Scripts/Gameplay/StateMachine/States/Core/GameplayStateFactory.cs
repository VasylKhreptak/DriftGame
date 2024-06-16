using System;
using System.Collections.Generic;
using Infrastructure.StateMachine.Main.States.Core;
using Infrastructure.StateMachine.Main.States.Factory;
using Zenject;

namespace Gameplay.StateMachine.States.Core
{
    public class GameplayStateFactory : StateFactory
    {
        public GameplayStateFactory(DiContainer container) : base(container) { }

        protected override Dictionary<Type, Func<IBaseState>> BuildStatesMap() =>
            new Dictionary<Type, Func<IBaseState>>
            {
                [typeof(InitializeState)] = _container.Resolve<InitializeState>,
                [typeof(SpawnCarsState)] = _container.Resolve<SpawnCarsState>,
                [typeof(WarmUpState)] = _container.Resolve<WarmUpState>,
                [typeof(StartRaceState)] = _container.Resolve<StartRaceState>,
                [typeof(FinishRaceState)] = _container.Resolve<FinishRaceState>,
                [typeof(FinalizeProgressAndLoadGarageState)] = _container.Resolve<FinalizeProgressAndLoadGarageState>
            };
    }
}