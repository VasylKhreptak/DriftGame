using Gameplay.InputService;
using Gameplay.InputService.Core;
using Gameplay.StateMachine;
using Gameplay.StateMachine.States;
using Gameplay.StateMachine.States.Factory;
using Gameplay.TimeManagement;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class GameplayInstaller : MonoInstaller
    {
        [Header("References")]
        [SerializeField] private ScreenInputService _screenInputService;

        public override void InstallBindings()
        {
            Container.Bind<LevelTimer>().AsSingle();
            BindInputService();
            BindStateMachine();
        }

        private void BindInputService()
        {
            if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
                Container.Bind<IInputService>().FromInstance(_screenInputService).AsSingle();
            else
                Container.Bind<IInputService>().To<KeyboardInputService>().AsSingle();
        }

        private void BindStateMachine()
        {
            BindStates();
            Container.Bind<GameplayStateFactory>().AsSingle();
            Container.BindInterfacesTo<GameplayStateMachine>().AsSingle();
        }

        private void BindStates()
        {
            Container.Bind<InitializeState>().AsSingle();
            Container.Bind<InitializeConnectionState>().AsSingle();
            Container.Bind<SpawnCarsState>().AsSingle();
            Container.Bind<WarpUpState>().AsSingle();
            Container.Bind<StartRaceState>().AsSingle();
            Container.Bind<FinishRaceState>().AsSingle();
        }
    }
}