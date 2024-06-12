using Gameplay.InputService.Core;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.InputService
{
    public class ScreenInputService : MonoBehaviour, IInputService
    {
        [Header("References")]
        [SerializeField] private Button _leftButton;
        [SerializeField] private Button _rightButton;
        [SerializeField] private Button _upButton;
        [SerializeField] private Button _downButton;

        private bool _leftPressed;
        private bool _rightPressed;
        private bool _upPressed;
        private bool _downPressed;

        private readonly CompositeDisposable _subscriptions = new CompositeDisposable();

        public float Horizontal => Enabled ? (_rightPressed ? 1 : 0) - (_leftPressed ? 1 : 0) : 0;
        public float Vertical => Enabled ? (_upPressed ? 1 : 0) - (_downPressed ? 1 : 0) : 0;
        public bool Enabled { get; set; }

        #region MonoBehaviour

        private void OnEnable()
        {
            _leftButton.OnPointerDownAsObservable().Subscribe(_ => _leftPressed = true).AddTo(_subscriptions);
            _leftButton.OnPointerUpAsObservable().Subscribe(_ => _leftPressed = false).AddTo(_subscriptions);

            _rightButton.OnPointerDownAsObservable().Subscribe(_ => _rightPressed = true).AddTo(_subscriptions);
            _rightButton.OnPointerUpAsObservable().Subscribe(_ => _rightPressed = false).AddTo(_subscriptions);

            _upButton.OnPointerDownAsObservable().Subscribe(_ => _upPressed = true).AddTo(_subscriptions);
            _upButton.OnPointerUpAsObservable().Subscribe(_ => _upPressed = false).AddTo(_subscriptions);

            _downButton.OnPointerDownAsObservable().Subscribe(_ => _downPressed = true).AddTo(_subscriptions);
            _downButton.OnPointerUpAsObservable().Subscribe(_ => _downPressed = false).AddTo(_subscriptions);
        }

        private void OnDisable()
        {
            _subscriptions?.Clear();

            _leftPressed = false;
            _rightPressed = false;
            _upPressed = false;
            _downPressed = false;
        }

        #endregion
    }
}