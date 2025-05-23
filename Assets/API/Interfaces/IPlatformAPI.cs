using Cysharp.Threading.Tasks;

namespace API.Interfaces
{
    public interface IPlatformAPI
    {
        event System.Action OnGamePause;
        event System.Action OnGameResume;

        UniTask InitSDK();
        string GetNativePlatformName();
        string GetPlatformName();
        string GetLanguage();
    }
}