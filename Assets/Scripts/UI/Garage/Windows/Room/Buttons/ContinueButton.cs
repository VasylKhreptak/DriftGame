using Infrastructure.Services.StaticData.Core;
using Infrastructure.StateMachine.Game.States;
using Infrastructure.StateMachine.Game.States.Core;
using Infrastructure.StateMachine.Main.Core;
using Multiplayer;
using Photon.Pun;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Garage.Windows.Room.Buttons
{
    public class ContinueButton : MonoBehaviourPunCallbacks
    {
        [Header("References")]
        [SerializeField] private Button _button;

        private RoomPlayersCountObserver _playersCountObserver;
        private IStateMachine<IGameState> _stateMachine;
        private IStaticDataService _staticDataService;

        [Inject]
        private void Constructor(RoomPlayersCountObserver playersCountObserver, IStateMachine<IGameState> stateMachine,
            IStaticDataService staticDataService)
        {
            _playersCountObserver = playersCountObserver;
            _stateMachine = stateMachine;
            _staticDataService = staticDataService;
        }

        #region MonoBehaviour

        private void OnValidate() => _button ??= GetComponent<Button>();

        private void Awake() => _playersCountObserver.Count.Subscribe(OnPlayersCountChanged).AddTo(this);

        public override void OnEnable()
        {
            base.OnEnable();

            _button.onClick.AddListener(StartGameplay);
        }

        public override void OnDisable()
        {
            base.OnDisable();

            _button.onClick.RemoveListener(StartGameplay);
        }

        #endregion

        private void OnPlayersCountChanged(int count)
        {
            if (count is 1 or 0)
                gameObject.SetActive(PhotonNetwork.IsMasterClient);
        }

        private void StartGameplay()
        {
            photonView.RPC(nameof(LoadGameplay), RpcTarget.All);
            _button.interactable = false;
        }

        [PunRPC]
        private void LoadGameplay()
        {
            LoadSceneAsyncState.Payload payload = new LoadSceneAsyncState.Payload
            {
                SceneName = _staticDataService.Config.GameplayScene.Name
            };

            _stateMachine.Enter<LoadSceneWithTransitionAsyncState, LoadSceneAsyncState.Payload>(payload);
        }
    }
}