using Cysharp.Threading.Tasks;

namespace API.Interfaces
{
    public interface IAdsAPI<RewardType>
    {
        public void Initialize();
        public UniTask PrepareRewarded();
        public bool IsRewardedReady();
        public UniTask<RewardResult<RewardType>> ShowRewarded(RewardType type);
        public UniTask PrepareFullscreen();
        public UniTask ShowFullscreen();
        public void EnableStickyBanner();
        public void DisableStickyBanner();
        public UniTask ShowPreloaderAds();
    }

    public class RewardResult<RewardType>
    {
        public bool IsRewarded;
        public bool IsDismissed;
        public RewardType Type;
    }
}