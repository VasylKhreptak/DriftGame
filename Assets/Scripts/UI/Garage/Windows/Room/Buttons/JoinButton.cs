using Multiplayer;
using UI.Buttons.Core;
using UI.Garage.Windows.Room.InputFields;
using UnityEngine;
using Zenject;

namespace UI.Garage.Windows.Room.Buttons
{
    public class JoinButton : BaseButton
    {
        [Header("References")]
        [SerializeField] private RoomNameInputField _roomNameInputField;

        private RoomManager _roomManager;

        [Inject]
        private void Constructor(RoomManager roomManager)
        {
            _roomManager = roomManager;
        }

        protected override void OnClicked() => _roomManager.JoinOrCreate(_roomNameInputField.Text);
    }
}