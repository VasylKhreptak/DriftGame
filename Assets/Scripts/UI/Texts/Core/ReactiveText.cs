using System;
using TMPro;
using UniRx;
using UnityEngine;

namespace UI.Texts.Core
{
    public abstract class ReactiveText : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TMP_Text _tmp;

        private IDisposable _subscription;

        #region MonoBehaviour

        private void OnValidate() => _tmp ??= GetComponent<TMP_Text>();

        private void OnEnable() => _subscription = GetObservable().Subscribe(value => _tmp.text = value);

        private void OnDisable() => _subscription?.Dispose();

        #endregion

        protected abstract IObservable<string> GetObservable();
    }
}