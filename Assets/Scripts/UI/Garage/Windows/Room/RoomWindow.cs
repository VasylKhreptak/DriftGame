using Multiplayer;
using UI.Windows.Core;
using Zenject;

namespace UI.Garage.Windows.Room
{
    public class RoomWindow : BaseWindow
    {
        private RoomManager _roomManager;

        [Inject]
        private void Constructor(RoomManager roomManager)
        {
            _roomManager = roomManager;
        }

        public override void Hide()
        {
            base.Hide();

            _roomManager.Leave();
        }
    }
}