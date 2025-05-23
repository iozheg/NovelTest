using System.Collections.Generic;

namespace GameCore.Models
{
    [System.Serializable]
    public class GameDataStorage : IDataStorage, IAudioData
    {
        public string Version;
        public int GameLevel = 1;
        public int GameScore = 0;

        public int Coins = 0;

        public bool IsSoundOn = true;
        public bool IsMusicOn = true;

        public UIState UIState = new() {
            ShowTutorial = true
        };

        public PromotionsState PromotionsState = new() {
        };

        public PurchaseState PurchaseState = new();

        public List<SpecialEventData> SpecialEvents = new();

        private readonly string _version = "1.0.0";

        public GameDataStorage() {
            Version = _version;
        }

        public void GetAudioState(out bool isMusicOn, out bool isSoundOn)
        {
            isMusicOn = IsMusicOn;
            isSoundOn = IsSoundOn;
        }

        public void SetAudioState(bool isMusicOn, bool isSoundOn)
        {
            IsMusicOn = isMusicOn;
            IsSoundOn = isSoundOn;
        }

        public void ApplyMigrations()
        {
            switch (Version) {
                default:
                    break;
            }

            Version = _version;
        }
    }

    [System.Serializable]
    public class UIState
    {
        public bool ShowTutorial = true;
    }

    [System.Serializable]
    public class PromotionsState
    {
    }

    [System.Serializable]
    public class PurchaseState
    {
        public bool NoAdsBought = false;
        public List<string> OneTimePurchases = new();
    }

    [System.Serializable]
    public class SpecialEventData
    {
        public string Id;
        public string Data;
    }
}
