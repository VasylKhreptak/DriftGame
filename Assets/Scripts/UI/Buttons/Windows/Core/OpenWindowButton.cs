using Infrastructure.Graphics.UI.Windows.Core;
using UI.Buttons.Core;
using Zenject;

namespace UI.Buttons.Windows.Core
{
    public class OpenWindowButton<T> : BaseButton where T : IWindow
    {
        private IWindow _window;

        [Inject]
        private void Constructor(T window) => _window = window;

        protected override void OnClicked() => _window.Show();
    }
}