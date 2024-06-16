using System;
using Multiplayer;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace UI.Garage.Windows.Room.Texts
{
    public class RoomPlayersCountText : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TMP_Text _tmp;

        [Header("Preferences")]
        [SerializeField] private string _format = "Players {0}/{1}";

        private RoomPlayersCountObserver _playersCountObserver;

        [Inject]
        private void Construct(RoomPlayersCountObserver playersCountObserver)
        {
            _playersCountObserver = playersCountObserver;
        }

        private IDisposable _subscription;

        #region MonoBehaviour

        private void OnEnable() => _subscription = _playersCountObserver.Count.Subscribe(OnPlayersCountChanged);

        private void OnDisable() => _subscription?.Dispose();

        #endregion

        private void OnPlayersCountChanged(int count) => _tmp.text = string.Format(_format, count, _playersCountObserver.MaxCount);
    }
}