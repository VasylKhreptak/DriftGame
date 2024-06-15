using System.Collections.Generic;
using Gameplay.InputService.Core;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Gameplay.Cars
{
    public class CarController : MonoBehaviour
    {
        private const float MinSpeed = 0.1f;

        [Header("References")]
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Transform _COF;

        [Header("Preferences")]
        [SerializeField] private float _maxSteerAngle = 30f;
        [SerializeField] [Range(0, 1f)] private float _steerSpeed = 0.5f;
        [SerializeField] private float _motorTorque = 1000f;
        [SerializeField] private float _brakeTorque = 3000f;
        [SerializeField] private float _handBrakeTorque = 10000f;

        [Header("Wheels")]
        [SerializeField] private List<Wheel> _wheels;

        private IInputService _inputService;

        [Inject]
        private void Constructor([InjectOptional] IInputService inputService)
        {
            _inputService = inputService;
        }

        private float _currentSteerAngle;

        [ShowInInspector] [ReadOnly] private int _moveSign;

        private Vector3 _targetWheelPosition;
        private Quaternion _targetWheelRotation;

        #region MonoBehaviour

        private void OnValidate() => _rigidbody ??= GetComponent<Rigidbody>();

        private void Awake()
        {
            _rigidbody.centerOfMass = _COF.localPosition;

            if (_inputService == null)
            {
                foreach (Wheel wheel in _wheels)
                {
                    wheel.Collider.brakeTorque = float.MaxValue;
                }
            }
        }

        private void FixedUpdate()
        {
            if (_inputService == null)
            {
                foreach (Wheel wheel in _wheels)
                {
                    UpdateTransform(wheel);
                }

                return;
            }

            UpdateMoveSignProperty();

            foreach (Wheel wheel in _wheels)
            {
                HandleMotor(wheel);
                HandleBrake(wheel);
                HandleSteering(wheel);
                UpdateTransform(wheel);
            }
        }

        #endregion

        private void UpdateMoveSignProperty()
        {
            if (_rigidbody.velocity.magnitude > MinSpeed)
            {
                _moveSign = Vector3.Dot(_rigidbody.velocity, transform.forward) > 0 ? 1 : -1;
                return;
            }

            _moveSign = 0;
        }

        private void HandleMotor(Wheel wheel)
        {
            if (wheel.CanDrive == false)
                return;

            if ((_moveSign == 1 && _inputService.Vertical > 0) ||
                (_moveSign == -1 && _inputService.Vertical < 0) ||
                _moveSign == 0 ||
                _inputService.Vertical == 0)
                wheel.Collider.motorTorque = _motorTorque * _inputService.Vertical;
        }

        private void HandleBrake(Wheel wheel)
        {
            if (wheel.CanBrake == false)
                return;

            if (_inputService.HandBrake && wheel.CanHandBrake)
            {
                wheel.Collider.brakeTorque = _handBrakeTorque;
                return;
            }

            if ((_moveSign == 1 && _inputService.Vertical < 0) || (_moveSign == -1 && _inputService.Vertical > 0))
                wheel.Collider.brakeTorque = _brakeTorque * Mathf.Abs(_inputService.Vertical);
            else if (_moveSign == 0 || _inputService.Vertical == 0)
                wheel.Collider.brakeTorque = 0;
        }

        private void UpdateTransform(Wheel wheel)
        {
            wheel.Collider.GetWorldPose(out _targetWheelPosition, out _targetWheelRotation);
            wheel.Transform.position = _targetWheelPosition;
            wheel.Transform.rotation = wheel.InverseRotation ? _targetWheelRotation * Quaternion.Euler(0, 180, 0) : _targetWheelRotation;
        }

        private void HandleSteering(Wheel wheel)
        {
            if (wheel.CanSteer == false)
                return;

            _currentSteerAngle = Mathf.Lerp(_currentSteerAngle, _maxSteerAngle * _inputService.Horizontal, _steerSpeed);

            wheel.Collider.steerAngle = _currentSteerAngle;
        }
    }
}