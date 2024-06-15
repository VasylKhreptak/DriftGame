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

        private Vector3 _lastRotation;
        private Vector3 _lastPosition;
        private bool _reachedLastState;

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
        }

        private void ReachLastState()
        {
            _cameraTransform.position = Vector3.Lerp(_cameraTransform.position, _lastPosition, _preferences.RestoreStateSpeed * Time.deltaTime);
            _cameraTransform.eulerAngles = Vector3.Lerp(_cameraTransform.eulerAngles, _lastRotation, _preferences.RestoreStateSpeed * Time.deltaTime);

            if (_cameraTransform.position.IsCloseTo(_lastPosition) && _cameraTransform.eulerAngles.IsCloseTo(_lastRotation))
                _reachedLastState = true;
        }

        private void ProcessOrbiting()
        {
            if (Input.touchCount != 1)
                return;

            _touch = Input.GetTouch(0);

            _deltaX = _touch.deltaPosition.x;

            _cameraTransform.RotateAround(_center, Vector3.up, _deltaX * _preferences.Sensitivity * Time.deltaTime);
        }

        private void UpdateLastStateValues()
        {
            _lastPosition = _cameraTransform.position;
            _lastRotation = _cameraTransform.eulerAngles;
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