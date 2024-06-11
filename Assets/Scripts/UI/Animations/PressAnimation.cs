using System;
using DG.Tweening;
using Infrastructure.Services.StaticData.Core;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace UI.Animations
{
    public class PressAnimation : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private Preferences _preferences;

        [Inject]
        private void Constructor(IStaticDataService staticDataService) => _preferences = staticDataService.Config.PressAnimationPreferences;

        private Tween _tween;

        #region MonoBehaviour

        private void OnDisable()
        {
            Stop();
            transform.localScale = _preferences.DefaultScale;
        }

        #endregion

        public void OnPointerDown(PointerEventData eventData) => SetScaleSmooth(_preferences.PressedScale);

        public void OnPointerUp(PointerEventData eventData) => SetScaleSmooth(_preferences.DefaultScale);

        private void SetScaleSmooth(Vector3 scale)
        {
            Stop();
            _tween = transform
                .DOScale(scale, _preferences.Duration)
                .SetEase(_preferences.Curve)
                .Play();
        }

        private void Stop() => _tween?.Kill();

        [Serializable]
        public class Preferences
        {
            [SerializeField] private Vector3 _defaultScale = Vector3.one;
            [SerializeField] private Vector3 _pressedScale = new Vector3(0.9f, 0.9f, 0.9f);
            [SerializeField] private float _duration = 0.1f;
            [SerializeField] private AnimationCurve _curve;

            public Vector3 DefaultScale => _defaultScale;
            public Vector3 PressedScale => _pressedScale;
            public float Duration => _duration;
            public AnimationCurve Curve => _curve;
        }
    }
}