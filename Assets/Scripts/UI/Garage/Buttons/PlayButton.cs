using Infrastructure.Graphics.UI.Windows.Core;
using UI.Buttons.Core;
using UI.Garage.Windows.Room;
using Zenject;

namespace UI.Garage.Buttons
{
    public class PlayButton : BaseButton
    {
        private IWindow _roomWindows;

        [Inject]
        private void Constructor(RoomWindow roomWindow)
        {
            _roomWindows = roomWindow;
        }

        protected override void OnClicked() => _roomWindows.Show();
    }
}