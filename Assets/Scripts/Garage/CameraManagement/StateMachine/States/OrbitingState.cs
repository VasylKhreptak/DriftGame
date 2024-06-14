using System;
using Garage.CameraManagement.StateMachine.States.Core;
using Garage.SpawnPoints;
using Infrastructure.StateMachine.Main.States.Core;
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
        }

        private Vector3 _cameraRotation;
        private Touch _touch;
        private float _deltaX;

        public void Enter() { }

        public void Tick()
        {
            if (Input.touchCount != 1)
                return;

            _touch = Input.GetTouch(0);

            _deltaX = _touch.deltaPosition.x;

            _cameraTransform.RotateAround(_center, Vector3.up, _deltaX * _preferences.Sensitivity * Time.deltaTime);
        }

        public void Exit() { }

        [Serializable]
        public class Preferences
        {
            [SerializeField] private float _sensitivity;

            public float Sensitivity => _sensitivity;
        }
    }
}