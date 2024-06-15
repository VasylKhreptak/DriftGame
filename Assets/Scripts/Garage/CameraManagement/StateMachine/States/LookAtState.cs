using System;
using Garage.CameraManagement.StateMachine.States.Core;
using Infrastructure.StateMachine.Main.States.Core;
using UnityEngine;
using Zenject;

namespace Garage.CameraManagement.StateMachine.States
{
    public class LookAtState : ICameraState, IPayloadedState<LookAtState.Payload>, ITickable
    {
        private readonly Transform _cameraTransform;
        private readonly Preferences _preferences;

        public LookAtState(Camera camera, Preferences preferences)
        {
            _cameraTransform = camera.transform;
            _preferences = preferences;
        }

        private Payload _payload;

        public void Enter(Payload payload)
        {
            _payload = payload;
        }

        public void Tick()
        {
            _cameraTransform.position = Vector3.Slerp(_cameraTransform.position, GetTargetPosition(), Time.deltaTime * _preferences.LookSpeed);
            _cameraTransform.forward = Vector3.Slerp(_cameraTransform.forward, _payload.Direction, Time.deltaTime * _preferences.LookSpeed);
        }

        private Vector3 GetTargetPosition() => _payload.Point - _payload.Direction * _payload.Distance;

        public class Payload
        {
            public Vector3 Point;
            public Vector3 Direction;
            public float Distance;
        }

        [Serializable]
        public class Preferences
        {
            [SerializeField] private float _lookSpeed = 5f;

            public float LookSpeed => _lookSpeed;
        }
    }
}