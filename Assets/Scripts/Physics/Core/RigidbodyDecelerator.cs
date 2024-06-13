using UnityEngine;

namespace Physics.Core
{
    public class RigidbodyDecelerator : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Rigidbody _rigidbody;

        [Header("Preferences")]
        [SerializeField] private float _defaultDrag = 0f;
        [SerializeField] private float _defaultAngularDrag = 0.05f;
        [SerializeField] private float _targetDrag = 2f;
        [SerializeField] private float _targetAngularDrag = 2f;

        #region MonoBehaviour

        private void OnValidate() => _rigidbody ??= GetComponent<Rigidbody>();

        #endregion

        public void Decelerate()
        {
            _rigidbody.drag = _targetDrag;
            _rigidbody.angularDrag = _targetAngularDrag;
        }

        public void Reset()
        {
            _rigidbody.drag = _defaultDrag;
            _rigidbody.angularDrag = _defaultAngularDrag;
        }
    }
}