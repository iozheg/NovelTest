using API.Interfaces;

using Cysharp.Threading.Tasks;

namespace API.Mock
{
    public class PlatformAPI : IPlatformAPI
    {
        public event System.Action OnGamePause;
        public event System.Action OnGameResume;

        public UniTask InitSDK()
        {
            return UniTask.CompletedTask;
        }

        public string GetNativePlatformName()
        {
            return "Mock";
        }

        public string GetPlatformName()
        {
            return "Mock";
        }

        public string GetLanguage()
        {
            return "en";
        }
    }
}
