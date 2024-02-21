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
    public Action<ShowResult> EligibleReward = null;
    [SerializeField] private float lastTimeInterAdShowed = -50f;
    [SerializeField] public bool unityAdsTestMode = true;
    [SerializeField] public bool showAdmobAdsFirst = true;
    public void Start()
    {
        // LoadAd();
    }
    public void LoadAd()
    {
        Debug.Log("=== Ads Manager Load Ad ()");
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
    public void ShowInterstitial(Action callback = null, bool showAdmobBefore = true)
    {
        callbackShowInterstitial = callback;
        /* Check just show ad if in some minutes before, not show ad yet.  */
        if (Time.time - lastTimeInterAdShowed >= GameAdConfig.MIN_TIME_SHOW_NEXT_AD)
        {
            if (showAdmobBefore)
            {
                ShowAdmobInterstitialBefore();
            }
            else
            {
                ShowUnityInterstitialBefore();
            }

            lastTimeInterAdShowed = Time.time;
        }
        else
        {
            callbackShowInterstitial?.Invoke();
        }
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
            Debug.Log("ShowUnityInterstitialBefore Inter is loaded");
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
        // ShowBanner();
        if (callbackShowInterstitial != null)
            callbackShowInterstitial();
    }

    public bool CheckVideoRewardReady()
    {
        return (Admobs.Instance.IsRewardedAdLoaded() || UnityAds.Instance.IsRewardedAdLoaded());
    }

    Action<ShowResult> callbackShowVideoReward;
    public void ShowRewardAd(Action<ShowResult> callback)
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
                    callbackShowVideoReward(ShowResult.Finished);
                break;

            case ShowResult.Skipped:
            case ShowResult.Failed:
                break;
        }
    }

    public void ShowVideoReward(Action<ShowResult> callback)
    {
        if (showAdmobAdsFirst)
        {
            ShowRewardGoogleAdFirst(callback);
        }
        else
        {
            ShowRewardUnityAdFirst(callback);
        }
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
    
    /* Nếu là trên Mobile, thì check Game hoạt động lại thì cho nhận thưởng ?
     Để giải quyết lỗi google admob video ads : get_gameObject can only be called from the main thread
     */
    #if !UNITY_EDITOR
    // Auto-called AFTER onRewardedVideoClosed is called. This method is documented on Unity website.
     private void OnApplicationPause(bool pause)
     {
         if (EligibleReward != null && !pause)
         {
             
             Debug.LogWarning("OnApplicationPause FALSE and resume reward");
             EligibleReward.Invoke(ShowResult.Finished);
             EligibleReward = null;
         }
     }
    #endif
}

