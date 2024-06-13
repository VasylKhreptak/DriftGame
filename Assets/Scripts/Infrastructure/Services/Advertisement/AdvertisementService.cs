using System;
using Infrastructure.Services.Advertisement.Core;
using Infrastructure.Services.StaticData.Core;
using UniRx;

namespace Infrastructure.Services.Advertisement
{
    public class AdvertisementService : IAdvertisementService, IDisposable
    {
        private const float InitializationTimeout = 2f;

        private readonly IStaticDataService _staticDataService;

        public AdvertisementService(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }

        private IDisposable _pauseSubscription;
        private IDisposable _timeoutSubscription;

        private bool _initialized;
        private bool _initializing;

        public event Action<bool> OnInitialized;

        public event Action OnRewarded;

        public void Initialize()
        {
            if (_initialized || _initializing)
                return;

            _initializing = true;

            IronSourceEvents.onSdkInitializationCompletedEvent += OnSuccessfullyInitialized;

            IronSource.Agent.validateIntegration();
            IronSource.Agent.init(_staticDataService.Config.AppKey);

            _timeoutSubscription = Observable.Timer(TimeSpan.FromSeconds(InitializationTimeout)).Subscribe(_ => OnFailedToInitialize());
        }

        public void LoadBanner() => IronSource.Agent.loadBanner(IronSourceBannerSize.BANNER, IronSourceBannerPosition.BOTTOM);

        public void DestroyBanner() => IronSource.Agent.destroyBanner();

        public void LoadInterstitial() => IronSource.Agent.loadInterstitial();

        public bool ShowInterstitial()
        {
            if (IronSource.Agent.isInterstitialReady() == false)
                return false;

            IronSource.Agent.showInterstitial();
            return true;
        }

        public void LoadRewardedVideo() => IronSource.Agent.loadRewardedVideo();

        public bool ShowRewardedVideo()
        {
            if (IronSource.Agent.isRewardedVideoAvailable() == false)
                return false;

            IronSource.Agent.showRewardedVideo();
            return true;
        }

        public void Dispose()
        {
            _timeoutSubscription?.Dispose();
            _pauseSubscription?.Dispose();
            IronSourceRewardedVideoEvents.onAdRewardedEvent -= OnAdRewarded;
        }

        private void OnSuccessfullyInitialized()
        {
            _timeoutSubscription?.Dispose();

            _pauseSubscription?.Dispose();
            _pauseSubscription = Observable.EveryApplicationPause().Subscribe(IronSource.Agent.onApplicationPause);

            IronSourceRewardedVideoEvents.onAdRewardedEvent += OnAdRewarded;

            _initialized = true;
            _initializing = false;

            OnInitialized?.Invoke(true);
        }

        private void OnFailedToInitialize()
        {
            _timeoutSubscription?.Dispose();
            _pauseSubscription?.Dispose();

            _initialized = false;
            _initializing = false;
            OnInitialized?.Invoke(false);
        }

        private void OnAdRewarded(IronSourcePlacement placement, IronSourceAdInfo adInfo) => OnRewarded?.Invoke();
    }
}