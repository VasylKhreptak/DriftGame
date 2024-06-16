using Data;
using Gameplay.Cars;
using Gameplay.Data;
using Gameplay.DebuggerOptions;
using Gameplay.InputService;
using Gameplay.InputService.Core;
using Gameplay.SpawnPoints;
using Gameplay.StateMachine;
using Gameplay.StateMachine.States;
using Gameplay.StateMachine.States.Core;
using Gameplay.TimeManagement;
using Infrastructure.Services.Advertisement.Core;
using Infrastructure.StateMachine.Main.Core;
using Multiplayer;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using Zenject;

namespace Gameplay.Installers
{
    public class GameplayInstaller : MonoInstaller, IInitializable
    {
        [Header("References")]
        [SerializeField] private ScreenInputService _screenInputService;
        [SerializeField] private CarSpawnPoints _carSpawnPoints;

        private IAdvertisementService _advertisementService;

        [Inject]
        private void Constructor(IAdvertisementService advertisementService)
        {
            _advertisementService = advertisementService;
        }

        #region MonoBehaviour

        private void OnValidate()
        {
            _screenInputService ??= FindObjectOfType<ScreenInputService>();
            _carSpawnPoints ??= FindObjectOfType<CarSpawnPoints>();
        }

        #endregion

        public override void InstallBindings()
        {
            Container.Bind<LevelTimer>().AsSingle();
            Container.BindInstance(_carSpawnPoints).AsSingle();
            Container.Bind<ReactiveHolder<Car>>().AsSingle();
            Container.Bind<GameplayData>().AsSingle();

            Container.BindInterfacesTo<ScoreCalculator>().AsSingle();

            BindInputService();
            BindStateMachine();
            BindDebugOptionsContainer();
            PreloadAd();
            MakeInitializable();
        }

        public void Initialize() => StartGameplay();

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
            Container.Bind<SpawnCarState>().AsSingle();
            Container.Bind<WarmUpState>().AsSingle();
            Container.Bind<StartRaceState>().AsSingle();
            Container.Bind<FinishRaceState>().AsSingle();
            Container.Bind<FinalizeProgressAndLoadGarageState>().AsSingle();
        }

        private void BindDebugOptionsContainer() => Container.BindInterfacesAndSelfTo<GameplayOptions>().AsSingle();

        private void PreloadAd() => _advertisementService.LoadRewardedVideo();

        private void StartGameplay() => Container.Resolve<IStateMachine<IGameplayState>>().Enter<InitializeState>();

        private void MakeInitializable() => Container.Bind<IInitializable>().FromInstance(this).AsSingle();
    }
}