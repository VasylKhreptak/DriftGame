using Garage.CameraManagement.StateMachine;
using Garage.CameraManagement.StateMachine.States;
using Garage.CameraManagement.StateMachine.States.Core;
using Garage.SpawnPoints;
using Infrastructure.StateMachine.Main.Core;
using UnityEngine;
using Zenject;

namespace Garage.Installers
{
    public class GarageInstaller : MonoInstaller
    {
        [Header("References")]
        [SerializeField] private CarSpawnPoint _carSpawnPoint;
        [SerializeField] private Camera _camera;

        [Header("Camera Preferences")]
        [SerializeField] private OrbitingState.Preferences _orbitingPreferences;
        [SerializeField] private LookAtState.Preferences _lookAtPreferences;

        #region MonoBehaviour

        private void OnValidate()
        {
            _carSpawnPoint ??= FindObjectOfType<CarSpawnPoint>(true);
            _camera ??= FindObjectOfType<Camera>(true);
        }

        #endregion

        public override void InstallBindings()
        {
            Container.BindInstance(_carSpawnPoint).AsSingle();
            Container.Bind<CarSelector>().AsSingle();
            Container.BindInstance(_camera).AsSingle();

            BindCameraStateMachine();
            EnterCameraDefaultState();
        }

        private void BindCameraStateMachine()
        {
            BindCameraStates();
            Container.Bind<CameraStateFactory>().AsSingle();
            Container.BindInterfacesTo<CameraStateMachine>().AsSingle();
        }

        private void BindCameraStates()
        {
            Container.Bind<OrbitingState>().AsSingle().WithArguments(_orbitingPreferences);
            Container.Bind<LookAtState>().AsSingle().WithArguments(_lookAtPreferences);
        }

        private void EnterCameraDefaultState() => Container.Resolve<IStateMachine<ICameraState>>().Enter<OrbitingState>();
    }
}