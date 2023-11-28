// using GoogleMobileAds.Api.Mediation.AppLovin;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : PersistentSingleton<AdsManager>
{
    public string currentInterNet = "";
    public string currentVideoNet = "";

    public void Start()
    {
        LoadAd();
    }
    public void LoadAd()
    {
        Admobs.Instance.Setup();
        UnityAds.Instance.Setup();
        // AppLovin.Initialize();
    }

    public void ShowBanner()
    {
        Admobs.Instance.ShowBannerAd();
    }

    public void HideBanner()
    {
        Admobs.Instance.HideBannerAd();
    }


    public bool CheckInterstitial()
    {
        return (Admobs.Instance.IsInterstitialAdLoaded() || UnityAds.Instance.IsInterstitialAdLoaded());
    }

    Action callbackShowInterstitial;
    public void ShowInterstitial(Action callback = null)
    {
        callbackShowInterstitial = callback;

        ShowAdmobInterstitialBefore();

    }

    public void ShowAdmobInterstitialBefore()
    {
        if (Admobs.Instance.IsInterstitialAdLoaded())
        {
            HideBanner();
            Admobs.Instance.ShowInterstitialAd(OnInterstitialCompleted);
            currentInterNet = GameAdConfig.ADMOB;
        }
        else
        {
            HideBanner();
            UnityAds.Instance.ShowInterstitialAd(OnInterstitialCompleted);
            currentInterNet = GameAdConfig.UNITY;
        }
    }

    public void ShowUnityInterstitialBefore()
    {
        if (UnityAds.Instance.IsInterstitialAdLoaded())
        {
            HideBanner();
            UnityAds.Instance.ShowInterstitialAd(OnInterstitialCompleted);
            currentInterNet = GameAdConfig.UNITY;
        }
        else
        {
            HideBanner();
            Admobs.Instance.ShowInterstitialAd(OnInterstitialCompleted);
            currentInterNet = GameAdConfig.ADMOB;
        }
    }

    private void OnInterstitialCompleted()
    {
        ShowBanner();
        if (callbackShowInterstitial != null)
            callbackShowInterstitial();
    }

    public bool CheckVideoRewardReady()
    {
        return (Admobs.Instance.IsRewardedAdLoaded() || UnityAds.Instance.IsRewardedAdLoaded());
    }

    Action callbackShowVideoReward;
    public void ShowRewardAd(Action callback)
    {
        if (CheckVideoRewardReady())
        {
            callbackShowVideoReward = callback;
            ShowVideoReward(CallbackVideoReward);
        }
    }

    private void CallbackVideoReward(ShowResult result)
    {
        ShowBanner();
        switch (result)
        {
            case ShowResult.Finished:
                if (callbackShowVideoReward != null)
                    callbackShowVideoReward();
                break;

            case ShowResult.Skipped:
            case ShowResult.Failed:
                break;
        }
    }

    public void ShowVideoReward(Action<ShowResult> callback)
    {
        ShowRewardGoogleAdFirst(callback);
    }

    public void ShowRewardGoogleAdFirst(Action<ShowResult> callback)
    {
        if (Admobs.Instance.IsRewardedAdLoaded())
        {
            HideBanner();
            Admobs.Instance.ShowRewardedAd(callback);
            currentVideoNet = GameAdConfig.ADMOB;
        }
        else
        {
            HideBanner();
            UnityAds.Instance.ShowRewardedAd(callback);
            currentVideoNet = GameAdConfig.UNITY;
        }
    }

    public void ShowRewardUnityAdFirst(Action<ShowResult> callback)
    {
        if (UnityAds.Instance.IsRewardedAdLoaded())
        {
            HideBanner();
            UnityAds.Instance.ShowRewardedAd(callback);
            currentVideoNet = GameAdConfig.UNITY;
        }
        else
        {
            HideBanner();
            Admobs.Instance.ShowRewardedAd(callback);
            currentVideoNet = GameAdConfig.ADMOB;
        }

    }
}

