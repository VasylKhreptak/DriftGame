using System;
using Gameplay.InputService.Core;
using UnityEngine;
using Zenject;

namespace CarPhysics
{
    public class CarController : MonoBehaviour
    {
        private IInputService _inputService;

        [Inject]
        private void Constructor(IInputService inputService)
        {
            _inputService = inputService;
        }

        #region MonoBehaviour

        private void FixedUpdate()
        {
            var horizontal = _inputService.Horizontal;
            var vertical = _inputService.Vertical;

            Debug.Log($"Horizontal: {horizontal}, Vertical: {vertical}");
        }

        #endregion
    }
}