using System;

namespace Infrastructure.Services.Advertisement.Core
{
    public interface IAdvertisementService
    {
        public event Action<bool> OnInitialized;

        public event Action OnRewarded;

        public void Initialize();

        public void LoadBanner();

        public void DestroyBanner();

        public void LoadInterstitial();

        public bool ShowInterstitial();

        public void LoadRewardedVideo();

        public bool ShowRewardedVideo();
    }
}