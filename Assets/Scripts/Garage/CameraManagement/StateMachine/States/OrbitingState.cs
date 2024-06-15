using System;
using Garage.CameraManagement.StateMachine.States.Core;
using Garage.SpawnPoints;
using Infrastructure.StateMachine.Main.States.Core;
using Plugins.Extensions;
using UnityEngine;
using Zenject;

namespace Garage.CameraManagement.StateMachine.States
{
    public class OrbitingState : ICameraState, IState, ITickable, IExitable
    {
        private const float LastStateThreshold = 0.1f;

        private readonly Transform _cameraTransform;
        private readonly Preferences _preferences;
        private readonly Vector3 _center;

        public OrbitingState(Camera camera, Preferences preferences, CarSpawnPoint carSpawnPoint)
        {
            _cameraTransform = camera.transform;
            _preferences = preferences;
            _center = carSpawnPoint.Point.position;

            _cameraTransform.LookAt(_center);
            UpdateLastStateValues();
        }

        private Vector3 _cameraRotation;
        private Touch _touch;
        private float _deltaX;

        private Vector3 _lastPosition;
        private Quaternion _lastRotation;
        private bool _reachedLastState;

        private bool _pressedOnUI;

        public void Enter() { }

        public void Tick()
        {
            if (_reachedLastState == false)
            {
                ReachLastState();
                return;
            }

            ProcessOrbiting();
        }

        public void Exit()
        {
            if (_reachedLastState == false)
                return;

            UpdateLastStateValues();

            _reachedLastState = false;
            _pressedOnUI = false;
        }

        private void ReachLastState()
        {
            _cameraTransform.position = Vector3.Lerp(_cameraTransform.position, _lastPosition, _preferences.RestoreStateSpeed * Time.deltaTime);
            _cameraTransform.rotation = Quaternion.Lerp(_cameraTransform.rotation, _lastRotation, _preferences.RestoreStateSpeed * Time.deltaTime);

            if (_cameraTransform.position.IsCloseTo(_lastPosition, LastStateThreshold))
                _reachedLastState = true;
        }

        private void ProcessOrbiting()
        {
            if (Input.touchCount != 1)
                return;

            _touch = Input.GetTouch(0);

            if (_touch.phase == TouchPhase.Began && PointerExtensions.IsPointerOverUI(_touch.position))
            {
                _pressedOnUI = true;
                return;
            }

            if (_touch.phase == TouchPhase.Ended)
            {
                _pressedOnUI = false;
                return;
            }

            if (_pressedOnUI)
                return;

            _deltaX = _touch.deltaPosition.x;

            _cameraTransform.RotateAround(_center, Vector3.up, _deltaX * _preferences.Sensitivity * Time.deltaTime);
        }

        private void UpdateLastStateValues()
        {
            _lastPosition = _cameraTransform.position;
            _lastRotation = _cameraTransform.rotation;
        }

        [Serializable]
        public class Preferences
        {
            [SerializeField] private float _sensitivity;
            [SerializeField] private float _restoreStateSpeed = 5f;

            public float Sensitivity => _sensitivity;
            public float RestoreStateSpeed => _restoreStateSpeed;
        }
    }
}