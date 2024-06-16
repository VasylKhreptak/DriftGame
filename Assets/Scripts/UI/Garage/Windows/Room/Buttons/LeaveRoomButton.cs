using Multiplayer;
using UI.Buttons.Core;
using Zenject;

namespace UI.Garage.Windows.Room.Buttons
{
    public class LeaveRoomButton : BaseButton
    {
        private RoomManager _roomManager;

        [Inject]
        private void Constructor(RoomManager roomManager)
        {
            _roomManager = roomManager;
        }

        protected override void OnClicked() => _roomManager.Leave();
    }
}