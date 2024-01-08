using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class UnityAds : PersistentSingleton<UnityAds>, IUnityAdsShowListener, IUnityAdsInitializationListener
{
 
    void Update()
    {
        
    }

    BannerLoadOptions bannerLoadOptions;
    BannerOptions bannerOptions;
    Action<int> callbackBannerLoaded;
    bool isShowBanner;
    bool isShowOnLoad;
    public void Setup()
    {
        Advertisement.Initialize(GameAdConfig.ID_UNITY,false,this);
    }

    public bool IsInit()
    {
        return Advertisement.isInitialized;
    }

    #region INTERSTITIAL

    Action callbackInterstitial;
    public bool IsInterstitialAdLoaded()
    {
        return true;
    }

    public void ShowInterstitialAd(Action callback)
    {
        callbackInterstitial = callback;
        Advertisement.Show(GameAdConfig.INTER_UNITY, this);
    }

    #endregion

    #region VIDEO_REWARDED

    Action<ShowResult> callbackRewardedAds;
    public bool IsRewardedAdLoaded()
    {
        return true;
    }


    public void RequestRewardedAd()
    {
        Debug.Log("Request Rewarded Ads");
    }

    public void ShowRewardedAd(Action<ShowResult> callback)
    {
        callbackRewardedAds = callback;
        Advertisement.Show(GameAdConfig.VIDEO_UNITY, this);
    }

    #endregion

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.LogError("Ad Show Failure");

        
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
    }

    public void OnUnityAdsShowClick(string placementId)
    {
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {


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
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
    }
}
