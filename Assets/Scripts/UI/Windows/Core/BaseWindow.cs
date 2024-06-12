using Infrastructure.Graphics.UI.Windows.Core;
using Plugins.Animations;
using Plugins.Animations.Core;
using UnityEngine;

namespace UI.Windows.Core
{
    public class BaseWindow : MonoBehaviour, IWindow
    {
        [Header("References")]
        [SerializeField] private GameObject _gameObject;

        [Header("Show Animation Preferences")]
        [SerializeField] private ScaleAnimation _scaleAnimation;
        [SerializeField] private FadeAnimation _fadeAnimation;

        private IAnimation _showAnimation;

        #region MonoBehaivour

        #region MonoBehaivour

        private void OnValidate() => _gameObject ??= GetComponent<GameObject>();

        #endregion

        private void Awake()
        {
            _showAnimation = new AnimationGroup(_scaleAnimation, _fadeAnimation);

            _gameObject.SetActive(false);
            _showAnimation.SetStartState();
        }

        #endregion

        public void Show()
        {
            _showAnimation.Stop();

            _gameObject.SetActive(true);
            _showAnimation.PlayForward();
        }

        public void Hide()
        {
            _showAnimation.Stop();

            _showAnimation.PlayBackward(() => _gameObject.SetActive(false));
        }
    }
}