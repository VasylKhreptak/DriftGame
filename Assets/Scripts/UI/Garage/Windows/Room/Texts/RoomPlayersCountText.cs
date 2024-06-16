using Infrastructure.Services.StaticData.Core;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using Zenject;

namespace UI.Garage.Windows.Room.Texts
{
    public class RoomPlayersCountText : MonoBehaviourPunCallbacks
    {
        [Header("References")]
        [SerializeField] private TMP_Text _tmp;

        [Header("Preferences")]
        [SerializeField] private string _format = "Players {0}/{1}";

        private IStaticDataService _staticDataService;

        [Inject]
        private void Constructor(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }

        #region MonoBehaviour

        private void OnValidate() => _tmp ??= GetComponent<TMP_Text>();

        public override void OnEnable()
        {
            base.OnEnable();

            UpdateText();
        }

        #endregion

        public override void OnJoinedRoom() => UpdateText();

        public override void OnPlayerEnteredRoom(Player newPlayer) => UpdateText();

        public override void OnLeftRoom() => UpdateText();
        public override void OnPlayerLeftRoom(Player otherPlayer) => UpdateText();

        private void UpdateText()
        {
            if (PhotonNetwork.CurrentRoom == null)
            {
                _tmp.text = string.Format(_format, 0, _staticDataService.Config.MaxPlayersCount);
                return;
            }

            _tmp.text = string.Format(_format, PhotonNetwork.CurrentRoom.PlayerCount, PhotonNetwork.CurrentRoom.MaxPlayers);
        }
    }
}