using UnityEngine;

namespace CarPhysics
{
    public class Car : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private CarController _controller;
        [SerializeField] private DriftObserver _driftObserver;

        public CarController Controller => _controller;
        public DriftObserver DriftObserver => _driftObserver;

        #region MonoBehaviour

        private void OnValidate()
        {
            _controller ??= GetComponentInChildren<CarController>(true);
            _driftObserver ??= GetComponentInChildren<DriftObserver>(true);
        }

        #endregion
    }
}