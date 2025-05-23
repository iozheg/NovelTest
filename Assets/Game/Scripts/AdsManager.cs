using System;
using System.Collections.Generic;

using API.Interfaces;

using Common.Analytics;

using Cysharp.Threading.Tasks;

using GameConfigs;

using UnityEngine;

public class AdsManager
{
    public IAdsAPI<RewardType> AdsAPI { get; private set; }

    public static AdsManager Instance { get; private set; }

    public event Action OnAdsStarted;
    public event Action OnAdsFinished;

    private readonly int _fullscreenAdThresholdSec = 180;
    private float _lastFullscreenAdTime;
    private bool _isFullscreenAdAllowed = false;
    private bool _isPreloaderAdAllowed = false;
    private bool _skipNextFullscreenAd = false;
    private readonly bool _resetFullscreenTimerOnRewarded = false;

    public AdsManager(IAdsAPI<RewardType> adsAPI, AdsManagerOptions options)
    {
        if (Instance != null)
        {
            return;
        }
        Instance = this;

        _isFullscreenAdAllowed = options.IsFullscreenAdAllowed;
        _isPreloaderAdAllowed = options.IsPreloaderAdAllowed;
        _resetFullscreenTimerOnRewarded = options.ResetFullscreenTimerOnRewarded;

        AdsAPI = adsAPI;
        AdsAPI.Initialize();
    }

    public UniTask PrepareRewarded()
    {
        return AdsAPI.PrepareRewarded();
    }

    public async UniTask<bool> ShowRewarded(RewardType type)
    {
        try {
            AnalyticsManager.Instance.LogEvent(
                AnalyticsEvent.RewardVideoRequest,
                "type",
                type.ToString()
            );
            OnAdsStarted?.Invoke();
            AudioManager.Instance.MuteToggle(true);
            if (!AdsAPI.IsRewardedReady())
            {
                await PrepareRewarded();
            }
            RewardResult<RewardType> result = await AdsAPI.ShowRewarded(type);

            if (result.IsRewarded)
            {
                _lastFullscreenAdTime = _resetFullscreenTimerOnRewarded
                    ? Time.realtimeSinceStartup
                    : _lastFullscreenAdTime;
                AnalyticsManager.Instance.LogEvent(
                    AnalyticsEvent.RewardVideoSuccess,
                    "type",
                    type.ToString()
                );
            }
            return result.IsRewarded;
        } catch (Exception e) {
            AnalyticsManager.Instance.LogEvent(
                AnalyticsEvent.GeneralError,
                new Dictionary<string, object> {
                    { "message", $"RewardedAds error: {e.Message}" },
                }
            );
            return false;
        }
        finally {
            OnAdsFinished?.Invoke();
            AudioManager.Instance.MuteToggle(false);
        }
    }

    public UniTask PrepareFullscreen()
    {
        return _isFullscreenAdAllowed ? AdsAPI.PrepareFullscreen() : UniTask.CompletedTask;
    }

    public async UniTask ShowFullscreen()
    {
        if (_skipNextFullscreenAd)
        {
            _skipNextFullscreenAd = false;
            return;
        }

        if (_isFullscreenAdAllowed
            && Time.realtimeSinceStartup - _lastFullscreenAdTime > _fullscreenAdThresholdSec)
        {
            try {
                OnAdsStarted?.Invoke();
                AudioManager.Instance.MuteToggle(true);
                _lastFullscreenAdTime = Time.realtimeSinceStartup;
                await AdsAPI.ShowFullscreen();
            } catch (Exception e) {
                AnalyticsManager.Instance.LogEvent(
                    AnalyticsEvent.GeneralError,
                    new Dictionary<string, object> {
                        { "message", $"FullscreenAds error: {e.Message}" },
                    }
                );
            }
            finally {
                OnAdsFinished?.Invoke();
                AudioManager.Instance.MuteToggle(false);
            }
        } else {
#if UNITY_EDITOR
            if (!_isFullscreenAdAllowed)
            {
                Debug.Log("Fullscreen ad is not allowed");
            }
            else
            {
                Debug.Log($"Fullscreen ad is not allowed at this time {_lastFullscreenAdTime} {Time.realtimeSinceStartup}");
            }
#endif
        }
    }

    public void EnableStickyBanner()
    {
        AdsAPI.EnableStickyBanner();
    }

    public void DisableStickyBanner()
    {
        AdsAPI.DisableStickyBanner();
    }

    public void DisableFullscreenAd()
    {
        _isFullscreenAdAllowed = false;
    }

    public void EnableFullscreenAd()
    {
        _isFullscreenAdAllowed = true;
    }

    public void DisablePreloaderAd()
    {
        _isPreloaderAdAllowed = false;
    }

    public void EnablePreloaderAd()
    {
        _isPreloaderAdAllowed = true;
    }

    public void SkipNextFullscreenAd()
    {
        _skipNextFullscreenAd = true;
    }

    public async UniTask ShowPreloaderAds()
    {
        if (_isPreloaderAdAllowed)
        {
            try {
                // AudioManager.Instance.MuteToggle(true);
                await AdsAPI.ShowPreloaderAds();
            } catch (Exception e) {
                AnalyticsManager.Instance.LogEvent(
                    AnalyticsEvent.GeneralError,
                    new Dictionary<string, object> {
                        { "message", $"PreloaderAds error: {e.Message}" },
                    }
                );
            } finally {
                // AudioManager.Instance.MuteToggle(false);
            }
        }
    }
}

public class AdsManagerOptions
{
    public bool IsFullscreenAdAllowed { get; set; }
    public bool IsPreloaderAdAllowed { get; set; }
    public bool ResetFullscreenTimerOnRewarded { get; set; }
}