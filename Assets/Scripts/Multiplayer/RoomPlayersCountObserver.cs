using Infrastructure.Services.StaticData.Core;
using Photon.Pun;
using Photon.Realtime;
using UniRx;
using Zenject;

namespace Multiplayer
{
    public class RoomPlayersCountObserver : MonoBehaviourPunCallbacks
    {
        private IStaticDataService _staticDataService;

        [Inject]
        private void Constructor(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }

        private readonly IntReactiveProperty _count = new IntReactiveProperty();

        public IReadOnlyReactiveProperty<int> Count => _count;

        public int MaxCount => _staticDataService.Config.MaxPlayersCount;

        public override void OnPlayerEnteredRoom(Player newPlayer) => UpdatePlayersCount();

        public override void OnPlayerLeftRoom(Player otherPlayer) => UpdatePlayersCount();

        public override void OnJoinedRoom() => UpdatePlayersCount();

        public override void OnLeftRoom() => UpdatePlayersCount();

        private void UpdatePlayersCount()
        {
            if (PhotonNetwork.CurrentRoom == null)
            {
                _count.Value = 0;
                return;
            }

            _count.Value = PhotonNetwork.CurrentRoom.PlayerCount;
        }
    }
}