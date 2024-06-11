using UnityEngine;

namespace Utilities.Noise
{
    public class NoiseRotator : MonoBehaviour
    {
        [Header("Preferences")]
        [SerializeField] private float _speed = 1;
        [SerializeField] private Vector3 _strength = Vector3.one;
        [SerializeField] private float _amplitude = 1;

        private Vector3 _initialLocalRotation;

        private float _dx;
        private float _dy;
        private float _dz;

        private Vector3 _rotation;

        #region MonoBehaivour

        private void Awake() => _initialLocalRotation = transform.localRotation.eulerAngles;

        private void Update() => UpdateRotation();

        #endregion

        private void UpdateRotation()
        {
            if (Mathf.Approximately(_amplitude, 0) || _strength == Vector3.zero)
                return;

            _dx = (float)NoiseS3D.Noise(Time.time * _speed, 0f, 0f) * _amplitude * _strength.x;
            _dy = (float)NoiseS3D.Noise(0f, Time.time * _speed, 0f) * _amplitude * _strength.y;
            _dz = (float)NoiseS3D.Noise(0f, 0f, Time.time * _speed) * _amplitude * _strength.z;

            _rotation = _initialLocalRotation;

            _rotation.x += _dx;
            _rotation.y += _dy;
            _rotation.z += _dz;

            transform.localRotation = Quaternion.Euler(_rotation);
        }
    }
}