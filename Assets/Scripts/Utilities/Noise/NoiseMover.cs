using UnityEngine;

namespace Utilities.Noise
{
    public class NoiseMover : MonoBehaviour
    {
        [Header("Preferences")]
        [SerializeField] private float _speed = 1;
        [SerializeField] private Vector3 _strength = Vector3.one;
        [SerializeField] private float _amplitude = 1;

        private Vector3 _initialLocalPosition;

        private float _dx;
        private float _dy;
        private float _dz;

        private Vector3 _position;

        #region MonoBehaivour

        private void Awake() => _initialLocalPosition = transform.localPosition;

        private void Update() => UpdatePosition();

        #endregion

        private void UpdatePosition()
        {
            if (Mathf.Approximately(_amplitude, 0) || _strength == Vector3.zero)
                return;

            _dx = (float)NoiseS3D.Noise(Time.time * _speed, 0f, 0f) * _amplitude * _strength.x;
            _dy = (float)NoiseS3D.Noise(0f, Time.time * _speed, 0f) * _amplitude * _strength.y;
            _dz = (float)NoiseS3D.Noise(0f, 0f, Time.time * _speed) * _amplitude * _strength.z;

            _position = _initialLocalPosition;

            _position.x += _dx;
            _position.y += _dy;
            _position.z += _dz;

            transform.localPosition = _position;
        }
    }
}