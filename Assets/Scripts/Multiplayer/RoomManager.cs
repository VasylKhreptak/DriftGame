using Infrastructure.Services.StaticData.Core;
using Infrastructure.Services.ToastMessage.Core;
using Photon.Pun;
using Photon.Realtime;
using Zenject;

namespace Multiplayer
{
    public class RoomManager : MonoBehaviourPunCallbacks
    {
        private IToastMessageService _toastMessageService;
        private IStaticDataService _staticDataService;

        [Inject]
        private void Constructor(IToastMessageService toastMessageService, IStaticDataService staticDataService)
        {
            _toastMessageService = toastMessageService;
            _staticDataService = staticDataService;
        }

        private void Start() => EnsureConnection();

        public override void OnConnectedToMaster()
        {
            base.OnConnectedToMaster();

            PhotonNetwork.JoinLobby();
        }

        public void JoinOrCreate(string roomName)
        {
            if (IsRoomNameValid(roomName) == false)
            {
                _toastMessageService.Send("Invalid room name");
                return;
            }

            if (PhotonNetwork.InRoom && PhotonNetwork.CurrentRoom.Name == roomName)
                return;

            if (EnsureConnection() == false)
                return;

            RoomOptions roomOptions = new RoomOptions
            {
                MaxPlayers = _staticDataService.Config.MaxPlayersCount
            };

            PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, null);
        }

        public void Leave()
        {
            if (PhotonNetwork.InRoom == false)
                return;

            PhotonNetwork.LeaveRoom();
        }

        private bool IsRoomNameValid(string roomName) => string.IsNullOrEmpty(roomName) == false;

        private bool EnsureConnection()
        {
            if (PhotonNetwork.InRoom)
                return true;

            if (PhotonNetwork.IsConnected == false)
            {
                PhotonNetwork.ConnectUsingSettings();
                return false;
            }

            if (PhotonNetwork.InLobby == false)
            {
                PhotonNetwork.JoinLobby();
                return false;
            }

            return true;
        }
    }
}