using Infrastructure.Services.Log.Core;
using Infrastructure.Services.StaticData.Core;
using UnityEngine;
using Zenject;
using LogType = Infrastructure.Services.Log.Core.LogType;

namespace Infrastructure.Services.Log
{
    public class LogService : ILogService
    {
        private IStaticDataService _staticDataService;

        [Inject]
        private void Constructor(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }

        public void Log(object message, Object context = null)
        {
            if (_staticDataService.Config.LogType.HasFlag(LogType.Info))
                Debug.Log(message, context);
        }

        public void LogWarning(object message, Object context = null)
        {
            if (_staticDataService.Config.LogType.HasFlag(LogType.Warning))
                Debug.LogWarning(message, context);
        }

        public void LogError(object message, Object context = null)
        {
            if (_staticDataService.Config.LogType.HasFlag(LogType.Error))
                Debug.LogError(message, context);
        }
    }
}