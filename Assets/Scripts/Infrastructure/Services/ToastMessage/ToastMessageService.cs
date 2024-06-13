using Infrastructure.Services.Log.Core;
using Infrastructure.Services.ToastMessage.Core;
using UnityEngine;

namespace Infrastructure.Services.ToastMessage
{
    public class ToastMessageService : IToastMessageService
    {
        private readonly ILogService _logService;

        public ToastMessageService(ILogService logService)
        {
            _logService = logService;
        }

        public void Send(string message)
        {
            if (Application.platform != RuntimePlatform.Android)
            {
                _logService.Log("ToastMessage: " + message);
                return;
            }

            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

            if (unityActivity != null)
            {
                AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
                unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
                {
                    AndroidJavaObject toastObject = toastClass.CallStatic<AndroidJavaObject>("makeText", unityActivity, message, 0);
                    toastObject.Call("show");
                }));
            }
        }
    }
}