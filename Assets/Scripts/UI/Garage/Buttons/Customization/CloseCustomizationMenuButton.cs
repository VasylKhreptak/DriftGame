using Cars.Customization;
using Garage.CameraManagement.StateMachine.States;
using Garage.CameraManagement.StateMachine.States.Core;
using Infrastructure.StateMachine.Main.Core;
using UI.Buttons.Core;
using UnityEngine;
using Zenject;

namespace UI.Garage.Buttons.Customization
{
    public class CloseCustomizationMenuButton : BaseButton
    {
        [Header("References")]
        [SerializeField] private PartCustomizationMenu _partCustomizationMenu;
        [SerializeField] private OpenCustomizationMenuButtons _openButtons;

        private IStateMachine<ICameraState> _cameraStateMachine;

        [Inject]
        private void Constructor([InjectOptional] IStateMachine<ICameraState> cameraStateMachine)
        {
            _cameraStateMachine = cameraStateMachine;
        }

        #region MonoBehaviour

        protected override void OnValidate()
        {
            base.OnValidate();

            _openButtons = GetComponentInParent<OpenCustomizationMenuButtons>(true);
        }

        #endregion

        protected override void OnClicked()
        {
            _partCustomizationMenu.Enabled = false;

            _openButtons.SetActive(true);

            _cameraStateMachine?.Enter<OrbitingState>();
        }
    }
}