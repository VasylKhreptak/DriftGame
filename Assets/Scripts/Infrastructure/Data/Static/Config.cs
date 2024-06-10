using UnityEngine;
using UnityEngine.Serialization;
using LogType = Infrastructure.Services.Log.Core.LogType;

namespace Infrastructure.Data.Static
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "ScriptableObjects/Static/GameConfig", order = 0)]
    public class Config : ScriptableObject
    {
        [Header("Scenes")]
        [SerializeField] private string _bootstrapScene = "Bootstrap";
        [SerializeField] private string _mainMenuScene = "MainScene";

        [Header("Log Preferences")]
        [SerializeField]
        private LogType _editorLogType = LogType.Info;
        [SerializeField] private LogType _buildLogType = LogType.Info;

        public string BootstrapScene => _bootstrapScene;
        public string MainMenuScene => _mainMenuScene;

        public LogType LogType => Application.isEditor ? _editorLogType : _buildLogType;
    }
}