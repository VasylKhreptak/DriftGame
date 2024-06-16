using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Infrastructure.Services.StaticData.Core;
using Plugins.Extensions;
using UnityEngine;
using UnityEngine.Networking;

namespace Music
{
    public class BackgroundMusicProvider
    {
        private readonly Preferences _preferences;

        public BackgroundMusicProvider(IStaticDataService staticDataService)
        {
            _preferences = staticDataService.Config.BackgroundMusicPreferences;
        }

        public async UniTask<AudioClip> GetAudioClipAsync(CancellationToken cancellationToken)
        {
            string url = _preferences.Urls.Random();

            using UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.MPEG);

            await www.SendWebRequest().ToUniTask(cancellationToken: cancellationToken);

            if (www.result != UnityWebRequest.Result.Success)
                throw new Exception($"Failed to download audio clip: {www.error}");

            return DownloadHandlerAudioClip.GetContent(www);
        }

        [Serializable]
        public class Preferences
        {
            [SerializeField] private string[] _urls;

            public string[] Urls => _urls;
        }
    }
}