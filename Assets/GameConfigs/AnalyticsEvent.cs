namespace GameConfigs
{
    public enum AnalyticsEvent
    {
        APP_START,
        BUILDING_SCREEN_SHOWN,
        LEVEL_STARTED,
        LEVEL_FINISHED,
        LEVEL_FAILED,
        REVIVE_FOR_COINS,

        REQUEST_DOUBLE_REWARD,
        DOUBLE_REWARD_RECEIVED,
        REQUEST_REVIVE_REWARD,
        REVIVE_REWARD_RECEIVED,
        REQUEST_UNLOCK_TILE_REWARD,
        UNLOCK_TILE_REWARD_RECEIVED,

        BUILDING_PROGRESS_SHOWN,
        BUILDING_FINISHED,
        INTERSTITIAL_AD_SHOWN,
        INTERSTITIAL_AD_ERROR,
        STICKY_BANNER_AD_SHOWN,
        STICKY_BANNER_AD_ERROR,

        GEM_EXCHANGE_POPUP_SHOWN,
        GEMS_SOLD,
        NO_ADS_POPUP_SHOWN,
        ADD_TILE_POPUP_SHOWN,
        NEW_TILE_ADDED,

        GeneralError,
        PurchaseError,
        PurchaseStateInfo,
        ShopOpen,
        ShopPurchaseStart,
        ShopPurchaseSuccess,
        RewardVideoRequest,
        RewardVideoSuccess,
        AdError
    }
}