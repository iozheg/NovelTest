using API.Interfaces;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace API.Mock
{
    public class AdsAPI<RewardType> : IAdsAPI<RewardType>
    {
        private readonly int _adsDurationMs = 2000;

        public void Initialize()
        {
            Debug.Log("Initializing ads API");
        }

        public UniTask PrepareRewarded()
        {
            Debug.Log("Preparing rewarded ads");
            return UniTask.CompletedTask;
        }

        public bool IsRewardedReady()
        {
            return true;
        }

        public UniTask<RewardResult<RewardType>> ShowRewarded(RewardType type)
        {
            RewardResult<RewardType> rewardResult = new ()
            {
                IsRewarded = false,
                IsDismissed = false,
                Type = type
            };

            var utcs = new UniTaskCompletionSource<RewardResult<RewardType>>();

            UniTask OnRewarded()
            {
                Debug.Log($"Rewarded ad for {type} completed");
                return UniTask
                    .Delay(_adsDurationMs)
                    .ContinueWith(() => rewardResult.IsRewarded = true);
            }

            void OnClosed() {
                Debug.Log($"Rewarded ad for {type} closed");
                UniTask
                    .Delay(_adsDurationMs)
                    .ContinueWith(() => utcs.TrySetResult(rewardResult))
                    .Forget();
            }

            Debug.Log($"Showing rewarded ad for {type}");

            OnRewarded().ContinueWith(() => OnClosed()).Forget();

            return utcs.Task;
        }

        public UniTask PrepareFullscreen()
        {
            Debug.Log("Preparing fullscreen ads");
            return UniTask.CompletedTask;
        }

        public UniTask ShowFullscreen()
        {
            var utcs = new UniTaskCompletionSource();
            Debug.Log("Showing fullscreen ad");
            UniTask.Delay(_adsDurationMs).ContinueWith(() => {
                Debug.Log("Fullscreen ad closed");
                utcs.TrySetResult();
            }).Forget();

            return utcs.Task;
        }

        public void EnableStickyBanner()
        {
            Debug.Log("Enabling sticky banner");
        }

        public void DisableStickyBanner()
        {
            Debug.Log("Disabling sticky banner");
        }

        public UniTask ShowPreloaderAds()
        {
            Debug.Log("Showing preloader ads");
            return UniTask.CompletedTask;
        }
    }
}