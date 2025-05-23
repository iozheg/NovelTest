using API.Interfaces;
using API.Mock;
using GameConfigs;

using GameCore.Models;

public class GameAPI
{
    public static IGameplayAPI GameplayAPI { get; private set; }
    public static IPlatformAPI PlatformAPI { get; private set; }
    public static IPlayerAPI<GameDataStorage> PlayerAPI { get; private set; }
    public static IPurchaseAPI PurchaseAPI { get; private set; }
    public static ISocialAPI SocialAPI { get; private set; }
    public static IAdsAPI<RewardType> AdsAPI { get; private set; }

    public static GameAPI Instance { get; private set; }

    public GameAPI()
    {
        if (Instance != null)
        {
            return;
        }
        Instance = this;

        SocialAPI = new SocialAPI();
        GameplayAPI = new GameplayAPI();
        PlatformAPI = new PlatformAPI();
        PlayerAPI = new PlayerAPI<GameDataStorage>();
        PurchaseAPI = new PurchaseAPI();
        AdsAPI = new AdsAPI<RewardType>();
    }
}