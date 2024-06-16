using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Infrastructure.Services.Log.Core;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Music
{
    public class BackgroundMusicPlayer : IDisposable
    {
        private const float RetryTime = 2f;

        private readonly BackgroundMusicProvider _backgroundMusicProvider;
        private readonly AudioSource _audioSource;
        private readonly ILogService _logService;

        public BackgroundMusicPlayer(BackgroundMusicProvider backgroundMusicProvider, ILogService logService)
        {
            _backgroundMusicProvider = backgroundMusicProvider;
            _logService = logService;
            _audioSource = CreateAudioSource();
        }

        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        private bool _startedPlaying;

        public void Dispose() => _cancellationTokenSource.Cancel();

        private AudioSource CreateAudioSource()
        {
            GameObject backgroundMusicGameObject = new GameObject("Background Music");

            Object.DontDestroyOnLoad(backgroundMusicGameObject);

            AudioSource audioSource = backgroundMusicGameObject.AddComponent<AudioSource>();

            audioSource.loop = false;
            audioSource.spatialBlend = 0f;

            return audioSource;
        }

        public void Start()
        {
            if (_startedPlaying)
                return;

            Play(_cancellationTokenSource.Token).Forget();

            _startedPlaying = true;
        }

        private async UniTaskVoid Play(CancellationToken cancellationToken)
        {
            while (cancellationToken.IsCancellationRequested == false)
            {
                try
                {
                    AudioClip clip = await _backgroundMusicProvider.GetAudioClipAsync(cancellationToken);

                    await UniTask.Delay(TimeSpan.FromSeconds(RetryTime), cancellationToken: cancellationToken);

                    _audioSource.clip = clip;
                    _audioSource.Play();

                    await UniTask.Delay(TimeSpan.FromSeconds(clip.length), cancellationToken: cancellationToken);

                    _audioSource.Stop();

                    clip.UnloadAudioData();
                    Object.Destroy(clip);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch (Exception e)
                {
                    _logService.LogError($"Background music player failed to play audio clip {e.Message}");

                    await UniTask.Delay(TimeSpan.FromSeconds(RetryTime), cancellationToken: cancellationToken).SuppressCancellationThrow();
                }
            }
        }
    }
}