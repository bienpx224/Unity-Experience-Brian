using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class UnityAds : PersistentSingleton<UnityAds>, IUnityAdsShowListener, IUnityAdsInitializationListener,IUnityAdsLoadListener
{
    
    BannerLoadOptions bannerLoadOptions;
    BannerOptions bannerOptions;
    Action<int> callbackBannerLoaded;
    bool isShowBanner;
    bool isShowOnLoad;
    private bool isInterLoaded = false;
    private bool isRewardLoaded = false;
    public void Setup()
    {
        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(GameAdConfig.ID_UNITY, AdsManager.Instance.unityAdsTestMode, this);
        }
        else
        {
            Debug.Log("UnityAds inited or not supported : " + Advertisement.isSupported);
        }
    }

    #region INTERSTITIAL

    Action callbackInterstitial;
    public bool IsInterstitialAdLoaded()
    {
        return isInterLoaded;
    }

    public void ShowInterstitialAd(Action callback)
    {
        isInterLoaded = false;
        callbackInterstitial = callback;
        Advertisement.Show(GameAdConfig.INTER_UNITY, this);
    }

    #endregion

    #region VIDEO_REWARDED

    Action<ShowResult> callbackRewardedAds;
    public bool IsRewardedAdLoaded()
    {
        return isRewardLoaded;
    }

    public void ShowRewardedAd(Action<ShowResult> callback)
    {
        isRewardLoaded = false;
        callbackRewardedAds = callback;
        Advertisement.Show(GameAdConfig.VIDEO_UNITY, this);
    }

    #endregion
    
    // Implement Load Listener and Show Listener interface methods: 
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log($"OnUnityAdsAdLoaded : " + adUnitId);
        if (adUnitId == GameAdConfig.VIDEO_UNITY)
        {
            isRewardLoaded = true;
        }else if (adUnitId == GameAdConfig.INTER_UNITY)
        {
            isInterLoaded = true;
        }
        // Optionally execute code if the Ad Unit successfully loads content.
    }
    public void OnUnityAdsFailedToLoad(string _adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit: {_adUnitId} - {error.ToString()} - {message}");
        // Optionally execute code if the Ad Unit fails to load, such as attempting to try again.
        LeanTween.delayedCall(5f,() =>
        {
            Advertisement.Load(_adUnitId, this); 
        });
    }
    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log($"UnityAds Error showing Ad Unit {placementId}: {error.ToString()} - {message}");
        Advertisement.Load(placementId, this);
        
        if (placementId.Equals(GameAdConfig.INTER_UNITY))
        {
            callbackInterstitial();
        }

        else if (placementId.Equals(GameAdConfig.VIDEO_UNITY))
        {
            callbackRewardedAds(ShowResult.Failed);
        }

        
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        Debug.Log($"OnUnityAdsShowStart : {placementId}");
    }

    public void OnUnityAdsShowClick(string placementId)
    {
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log("OnUnityAdsShowComplete : showCompletionState : " + showCompletionState);
        Advertisement.Load(placementId, this);
        if (placementId.Equals(GameAdConfig.INTER_UNITY))
        {
            callbackInterstitial();
        }

        else if (placementId.Equals(GameAdConfig.VIDEO_UNITY))
        {
            switch (showCompletionState)
            {
                case UnityAdsShowCompletionState.COMPLETED:
                    callbackRewardedAds(ShowResult.Finished);
                    break;

                case UnityAdsShowCompletionState.SKIPPED:
                    callbackRewardedAds(ShowResult.Skipped);
                    break;
                case UnityAdsShowCompletionState.UNKNOWN:
                    callbackRewardedAds(ShowResult.Failed);
                    break;

            }
        }
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
        Advertisement.Load(GameAdConfig.INTER_UNITY, this);
        Advertisement.Load(GameAdConfig.VIDEO_UNITY, this);
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }
}
