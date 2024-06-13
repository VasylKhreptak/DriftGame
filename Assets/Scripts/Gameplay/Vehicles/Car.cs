using Physics.Core;
using UnityEngine;

namespace Gameplay.Vehicles
{
    public class Car : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private CarController _controller;
        [SerializeField] private DriftObserver _driftObserver;
        [SerializeField] private RigidbodyDecelerator _rigidbodyDecelerator;

        public CarController Controller => _controller;
        public DriftObserver DriftObserver => _driftObserver;
        public RigidbodyDecelerator RigidbodyDecelerator => _rigidbodyDecelerator;

        #region MonoBehaviour

        private void OnValidate()
        {
            _controller ??= GetComponentInChildren<CarController>(true);
            _driftObserver ??= GetComponentInChildren<DriftObserver>(true);
            _rigidbodyDecelerator ??= GetComponentInChildren<RigidbodyDecelerator>(true);
        }

        #endregion
    }
}