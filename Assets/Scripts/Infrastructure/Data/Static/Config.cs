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

        [Header("Animations")]
        [SerializeField] private PressAnimation.Preferences _pressAnimationPreferences;

        public SceneField BootstrapScene => _bootstrapScene;
        public SceneField GarageScene => _garageScene;
        public SceneField GameplayScene => _gameplayScene;

        public LogType LogType => Application.isEditor ? _editorLogType : _buildLogType;
        
        public PressAnimation.Preferences PressAnimationPreferences => _pressAnimationPreferences;
    }
}