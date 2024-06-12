using Infrastructure.Graphics.UI.Windows.Core;
using UI.Buttons.Core;
using Zenject;

namespace UI.Buttons.Windows.Core
{
    public class CloseWindowButton<T> : BaseButton where T : IWindow
    {
        private IWindow _window;

        [Inject]
        private void Constructor(T window)
        {
            _window = window;
        }

        private bool _canClose = true;

        #region MonoBehaivour

        protected override void OnDisable()
        {
            base.OnDisable();

            _canClose = true;
        }

        #endregion

        protected override void OnClicked()
        {
            if (_canClose == false)
                return;

            _window.Hide();
        }
    }
}