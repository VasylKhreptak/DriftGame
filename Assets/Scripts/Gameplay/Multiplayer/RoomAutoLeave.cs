using System;
using Photon.Pun;

namespace Gameplay.Multiplayer
{
    public class RoomAutoLeave : IDisposable
    {
        public void Dispose()
        {
            if (PhotonNetwork.InRoom)
                PhotonNetwork.LeaveRoom();
        }
    }
}