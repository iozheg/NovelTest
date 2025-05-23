namespace API.Interfaces
{
    public interface ISocialAPI
    {
        bool IsSupportsShare();
        void Share(string message = null);
        bool IsSupportsLeaderboard();
        void OpenLeaderboard();
        void SetLeaderboardValue(int score);
        bool IsSupportsNativeInvite();
        void Invite();
    }
}
