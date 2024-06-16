using Cars.Customization;
using Garage.CameraManagement.StateMachine.States;
using Garage.CameraManagement.StateMachine.States.Core;
using Infrastructure.Services.Scene.Core;
using Infrastructure.StateMachine.Main.Core;
using Plugins.Extensions;
using UI.Buttons.Core;
using UnityEngine;
using Zenject;

namespace UI.Garage.Buttons.Customization
{
    public class OpenCustomizationMenuButton : BaseButton
    {
        [Header("References")]
        [SerializeField] private PartCustomizationMenu _partCustomizationMenu;
        [SerializeField] private OpenCustomizationMenuButtons _openButtons;

        [Header("Preferences")]
        [SerializeField] private Transform _lookTransform;
        [SerializeField] private float _distance;

        private ISceneService _sceneService;
        private IStateMachine<ICameraState> _cameraStateMachine;

        [Inject]
        private void Constructor(ISceneService sceneService, [InjectOptional] IStateMachine<ICameraState> cameraStateMachine)
        {
            _sceneService = sceneService;
            _cameraStateMachine = cameraStateMachine;
        }

        public bool Enabled
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        #region MonoBehaviour

        protected override void OnValidate()
        {
            base.OnValidate();

            _openButtons = GetComponentInParent<OpenCustomizationMenuButtons>(true);
        }

        private void Awake()
        {
            _partCustomizationMenu.Enabled = false;

            if (_sceneService.IsInGarage() == false)
                Enabled = false;
        }

        #endregion

        protected override void OnClicked()
        {
            _partCustomizationMenu.Enabled = true;

            _openButtons.SetActive(false);

            if (_cameraStateMachine == null)
                return;

            LookAtState.Payload payload = new LookAtState.Payload
            {
                Point = _lookTransform.position,
                Direction = _lookTransform.forward,
                Distance = _distance
            };

            _cameraStateMachine.Enter<LookAtState, LookAtState.Payload>(payload);
        }

        private void OnDrawGizmosSelected()
        {
            if (_lookTransform == null)
                return;

            Gizmos.color = Color.red.WithAlpha(0.3f);

            Gizmos.DrawSphere(_lookTransform.position, _distance);
        }
    }
}