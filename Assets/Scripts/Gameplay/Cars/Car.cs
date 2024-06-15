using Cars.Customization;
using Physics.Core;
using UnityEngine;

namespace Gameplay.Cars
{
    public class Car : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private CarController _controller;
        [SerializeField] private DriftObserver _driftObserver;
        [SerializeField] private RigidbodyDecelerator _rigidbodyDecelerator;
        [SerializeField] private CarCustomizationController _customizationController;

        [Header("Preferences")]
        [SerializeField] private CarModel _carModel;

        public CarController Controller => _controller;
        public DriftObserver DriftObserver => _driftObserver;
        public RigidbodyDecelerator RigidbodyDecelerator => _rigidbodyDecelerator;
        public CarCustomizationController CustomizationController => _customizationController;

        public CarModel Model => _carModel;

        #region MonoBehaviour

        private void OnValidate()
        {
            _controller ??= GetComponentInChildren<CarController>(true);
            _driftObserver ??= GetComponentInChildren<DriftObserver>(true);
            _rigidbodyDecelerator ??= GetComponentInChildren<RigidbodyDecelerator>(true);
            _customizationController ??= GetComponentInChildren<CarCustomizationController>(true);
        }

        #endregion
    }
}