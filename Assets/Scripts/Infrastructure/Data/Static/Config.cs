using Music;
using Udar.SceneManager;
using UI.Animations;
using UnityEngine;
using LogType = Infrastructure.Services.Log.Core.LogType;

namespace Infrastructure.Data.Static
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "ScriptableObjects/Static/GameConfig", order = 0)]
    public class Config : ScriptableObject
    {
        [Header("Scenes")]
        [SerializeField] private SceneField _bootstrapScene;
        [SerializeField] private SceneField _garageScene;
        [SerializeField] private SceneField _gameplayScene;

        [Header("Log Preferences")]
        [SerializeField] private LogType _editorLogType = LogType.Info;
        [SerializeField] private LogType _buildLogType = LogType.Info;

        [Header("Application")]
        [SerializeField] private string _androidAppKey = "1ecb53875";
        [SerializeField] private string _iosAppKey = string.Empty;

        [Header("Animations")]
        [SerializeField] private PressAnimation.Preferences _pressAnimationPreferences;

        [Header("Music")]
        [SerializeField] private BackgroundMusicProvider.Preferences _backgroundMusicPreferences;

        [Header("Multiplayer")]
        [SerializeField] private int _maxPlayersCount = 2;

        public SceneField BootstrapScene => _bootstrapScene;
        public SceneField GarageScene => _garageScene;
        public SceneField GameplayScene => _gameplayScene;

        public LogType LogType => Application.isEditor ? _editorLogType : _buildLogType;

        public PressAnimation.Preferences PressAnimationPreferences => _pressAnimationPreferences;

        public string AppKey =>
            Application.platform == RuntimePlatform.Android ? _androidAppKey :
            Application.platform == RuntimePlatform.IPhonePlayer ? _iosAppKey : "unexpected_platform";

        public BackgroundMusicProvider.Preferences BackgroundMusicPreferences => _backgroundMusicPreferences;

        public int MaxPlayersCount => _maxPlayersCount;
    }
}