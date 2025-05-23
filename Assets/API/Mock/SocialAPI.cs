using UnityEngine;

namespace API.Mock
{
    public class SocialAPI : Interfaces.ISocialAPI
    {
        public bool IsSupportsShare()
        {
            return true;
        }

        public void Share(string message = null)
        {
            Debug.Log("Share");
        }

        public bool IsSupportsLeaderboard()
        {
            return true;
        }

        public void OpenLeaderboard()
        {
            Debug.Log("OpenLeaderboard");
        }

        public void SetLeaderboardValue(int value)
        {
            Debug.Log($"SetLeaderboardScore {value}");
        }

        public bool IsSupportsNativeInvite()
        {
            return true;
        }

        public void Invite()
        {
            Debug.Log("Invite");
        }
    }
}