using Cysharp.Threading.Tasks;
using Gameplay.SpawnPoints;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Zenject;

namespace Gameplay.Multiplayer
{
    public class CarSpawnPointProvider : MonoBehaviourPunCallbacks
    {
        private CarSpawnPoints _spawnPoints;

        [Inject]
        private void Constructor(CarSpawnPoints spawnPoints)
        {
            _spawnPoints = spawnPoints;
        }

        private readonly AutoResetUniTaskCompletionSource<Transform> _completionSource = AutoResetUniTaskCompletionSource<Transform>.Create();

        private int _currentSpawnPointIndex = -1;

        public async UniTask<Transform> Get()
        {
            photonView.RPC(nameof(UpdateSpawnPointOnServer), RpcTarget.MasterClient, PhotonNetwork.LocalPlayer);

            return await _completionSource.Task;
        }

        [PunRPC]
        private void UpdateSpawnPointOnServer(Player requestingPlayer)
        {
            _currentSpawnPointIndex = (_currentSpawnPointIndex + 1) % _spawnPoints.Count;

            photonView.RPC(nameof(ProvideSpawnPoint), requestingPlayer, _currentSpawnPointIndex);
        }

        [PunRPC]
        private void ProvideSpawnPoint(int index) => _completionSource?.TrySetResult(_spawnPoints[index]);
    }
}