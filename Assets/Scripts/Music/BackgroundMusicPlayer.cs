using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Music
{
    public class BackgroundMusicPlayer
    {
        private const float RetryTime = 2f;

        private readonly BackgroundMusicProvider _backgroundMusicProvider;
        private readonly AudioSource _audioSource;

        public BackgroundMusicPlayer(BackgroundMusicProvider backgroundMusicProvider)
        {
            _backgroundMusicProvider = backgroundMusicProvider;
            _audioSource = CreateAudioSource();
        }

        private bool _startedPlaying;

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

            Play().Forget();

            _startedPlaying = true;
        }

        private async UniTaskVoid Play()
        {
            while (true
            {
                AudioClip clip = await _backgroundMusicProvider.GetAudioClipOrNull();

                if (clip == null)
                {
                    await UniTask.Delay(TimeSpan.FromSeconds(RetryTime));
                    continue;
                }

                _audioSource.clip = clip;
                _audioSource.Play();

                await UniTask.Delay(TimeSpan.FromSeconds(clip.length));

                _audioSource.Stop();

                clip.UnloadAudioData();
                Object.Destroy(clip);
            }
        }
    }
}