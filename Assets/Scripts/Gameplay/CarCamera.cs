using Cinemachine;
using UnityEngine;

namespace Gameplay
{
    public class CarCamera : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera;

        #region MonoBehaivour

        private void OnValidate() => _cinemachineVirtualCamera = GetComponentInChildren<CinemachineVirtualCamera>();

        #endregion

        public void SetTarget(Transform target)
        {
            _cinemachineVirtualCamera.Follow = target;
            _cinemachineVirtualCamera.LookAt = target;
        }
    }
}