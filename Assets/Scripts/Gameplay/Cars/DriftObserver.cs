using System;
using Photon.Pun;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;

namespace Gameplay.Cars
{
    public class DriftObserver : MonoBehaviourPunCallbacks
    {
        [Header("References")]
        [SerializeField] private WheelCollider[] _wheelColliders;

        [Header("Preferences")]
        [SerializeField] private float _checkInterval = 0.2f;
        [SerializeField] private float _sidewaysSlipThreshold = 0.5f;

        [ShowInInspector] [ReadOnly] private readonly BoolReactiveProperty _isDrifting = new BoolReactiveProperty(false);

        private IDisposable _subscription;

        private WheelHit _wheelHit;

        public IReadOnlyReactiveProperty<bool> IsDrifting => _isDrifting;

        #region MonoBehaviour

        private void Start() => enabled = photonView.IsMine;

        public override void OnEnable()
        {
            base.OnEnable();

            _subscription = Observable
                .Interval(TimeSpan.FromSeconds(_checkInterval))
                .DoOnSubscribe(UpdateIsDriftingProperty)
                .Subscribe(_ => UpdateIsDriftingProperty());
        }

        public override void OnDisable()
        {
            base.OnDisable();

            _subscription?.Dispose();
            _isDrifting.Value = false;
        }

        #endregion

        private void UpdateIsDriftingProperty() => _isDrifting.Value = CheckDrifting();

        private bool CheckDrifting()
        {
            foreach (WheelCollider wheelCollider in _wheelColliders)
            {
                if (wheelCollider.GetGroundHit(out _wheelHit))
                {
                    if (Mathf.Abs(_wheelHit.sidewaysSlip) > _sidewaysSlipThreshold)
                        return true;
                }
            }

            return false;
        }
    }
}