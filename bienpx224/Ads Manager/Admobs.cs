using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Events;

public class Admobs : PersistentSingleton<Admobs>
{
    //private AppOpenAd appOpenAd;
    private BannerView bannerView;
    private InterstitialAd interstitialAd;
    private RewardedAd rewardedAd;

    private bool isShowingAppOpenAd;

    int numReqBanner;
    int numReqInter;
    int numReqVideo;

    Action callbackInterAdClosed;
    Action<ShowResult> callbackRewardedAdClosed;
    bool hasReceivedRewardedAd;

    private List<string> listTestDevice = new List<string>()
    {
        "a1f66998-1049-4b6b-b5da-3b52934a02ba"
    };

    #region UNITY MONOBEHAVIOR METHODS

    public void Setup()
    {
        MobileAds.SetiOSAppPauseOnBackground(true);

        List<string> deviceIds = new List<string>() { AdRequest.TestDeviceSimulator };
        foreach (string s in listTestDevice)
        {
            deviceIds.Add(s);
        }

        // Configure TagForChildDirectedTreatment and test device IDs.
        RequestConfiguration requestConfiguration =
            new RequestConfiguration.Builder()
                .SetTagForChildDirectedTreatment(TagForChildDirectedTreatment.False)
                .SetTagForUnderAgeOfConsent(TagForUnderAgeOfConsent.False)
                .SetMaxAdContentRating(MaxAdContentRating.T)
                .SetTestDeviceIds(deviceIds).build();
        MobileAds.SetRequestConfiguration(requestConfiguration);

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(HandleInitCompleteAction);
    }

    private void HandleInitCompleteAction(InitializationStatus initstatus)
    {
        Toast.Show("Load success");
        Debug.Log("HandleInitCompleteAction : ");
        // Callbacks from GoogleMobileAds are not guaranteed to be called on
        // main thread.
        // In this example we use MobileAdsEventExecutor to schedule these calls on
        // the next Update() loop.
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            Debug.Log("Execute Update : HandleInitCompleteAction : " + initstatus);
#if BANNER_ON
            RequestBannerAd();
#endif
            RequestAndLoadInterstitialAd();
            RequestAndLoadRewardedAd();
            //RequestAndLoadAppOpenAd();
        });
    }

    private void Update()
    {
    }

    #endregion

    #region HELPER METHODS

    private AdRequest CreateAdRequest()
    {
        // return new AdRequest.Builder()
        //     .AddKeyword(Application.identifier)
        //     .Build();
        return new AdRequest();
    }

    #endregion

    #region BANNER ADS

    public void ShowBannerAd()
    {
        if (bannerView != null)
            bannerView.Show();
    }

    public void HideBannerAd()
    {
        if (bannerView != null)
            bannerView.Hide();
    }

    public void RequestBannerAd()
    {
        string adUnitId = GameAdConfig.BANNER_ADMOB;

        // Clean up banner before reusing
        if (bannerView != null)
        {
            bannerView.Destroy();
        }

        // Create a 320x50 banner at top of the screen
        bannerView = new BannerView(adUnitId,
            AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth), AdPosition.Bottom);

        // Add Event Handlers
        bannerView.OnBannerAdLoaded += OnBannerLoaded;
        bannerView.OnBannerAdLoadFailed += OnBannerFailedToLoad;

        // Load a banner ad
        bannerView.LoadAd(CreateAdRequest());
    }

    #region Banner Event

    private void OnBannerLoaded()
    {
        numReqBanner = 0;
    }

    private void OnBannerFailedToLoad(LoadAdError e)
    {
        if (numReqBanner < GameAdConfig.MAX_REQUEST_BANNER)
        {
            numReqBanner++;
            StartCoroutine(IEReloadBannerAd());
        }
    }

    IEnumerator IEReloadBannerAd()
    {
        yield return new WaitForSecondsRealtime(GameAdConfig.TIME_RELOAD_BANNER);
        RequestBannerAd();
    }

    #endregion

    public void DestroyBannerAd()
    {
        if (bannerView != null)
        {
            bannerView.Destroy();
        }
    }

    #endregion

    #region INTERSTITIAL ADS

    public void RequestAndLoadInterstitialAd()
    {
        string adUnitId = GameAdConfig.INTER_ADMOB;

        // Clean up interstitial before using it
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
            interstitialAd = null;
        }

        // Load an interstitial ad
        InterstitialAd.Load(adUnitId, CreateAdRequest(), OnInterAdLoaded);
    }

    private void OnInterAdLoaded(InterstitialAd ad, AdError error)
    {
        // If the operation failed with a reason.
        if (error != null)
        {
            Debug.LogError("Interstitial ad failed to load an ad with error : " + error);
            return;
        }

        // If the operation failed for unknown reasons.
        // This is an unexpected error, please report this bug if it happens.
        if (ad == null)
        {
            Debug.LogError("Unexpected error: Interstitial load event fired with null ad and null error.");
            return;
        }

        // The operation completed successfully.
        Debug.Log("Interstitial ad loaded with response : " + ad.GetResponseInfo());
        interstitialAd = ad;

        // Register to ad events to extend functionality.
        RegisterEventHandlersInterAd(ad);
        numReqInter = 0;
    }

    private void RegisterEventHandlersInterAd(InterstitialAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Interstitial ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () => { Debug.Log("Interstitial ad recorded an impression."); };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () => { Debug.Log("Interstitial ad was clicked."); };
        // Raised when an ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () => { Debug.Log("Interstitial ad full screen content opened."); };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Interstitial ad full screen content closed.");
            OnInterAdClosed();
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Interstitial ad failed to open full screen content with error : "
                           + error);
            OnInterAdClosed();
        };
    }

    private void OnInterAdFailedToLoad(AdError e)
    {
        if (numReqInter < GameAdConfig.MAX_REQUEST_INTER)
        {
            numReqInter++;
            StartCoroutine(IEReloadInterAd());
        }
    }

    IEnumerator IEReloadInterAd()
    {
        yield return new WaitForSecondsRealtime(GameAdConfig.TIME_RELOAD_INTER);
        RequestAndLoadInterstitialAd();
    }

    private void OnInterAdClosed()
    {
        if (callbackInterAdClosed != null)
            callbackInterAdClosed();
        RequestAndLoadInterstitialAd();
    }

    public bool IsInterstitialAdLoaded()
    {
        return (interstitialAd != null && interstitialAd.CanShowAd());
    }

    public void ShowInterstitialAd(Action callback)
    {
        callbackInterAdClosed = callback;

        if (interstitialAd.CanShowAd())
        {
            interstitialAd.Show();
        }
    }

    public void DestroyInterstitialAd()
    {
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
        }
    }

    #endregion

    #region REWARDED ADS

    public void RequestAndLoadRewardedAd()
    {
        string adUnitId = GameAdConfig.VIDEO_ADMOB;

        // Clean up the old ad before loading a new one.
        if (rewardedAd != null)
        {
            DestroyRewardAd();
        }

        Debug.Log("Loading rewarded ad.");

        // Create our request used to load the ad.
        var adRequest = new AdRequest();

        // Send the request to load the ad.
        RewardedAd.Load(adUnitId, adRequest, (RewardedAd ad, LoadAdError error) =>
        {
            // If the operation failed with a reason.
            if (error != null)
            {
                Debug.LogError("Rewarded ad failed to load an ad with error : " + error);
                return;
            }

            // If the operation failed for unknown reasons.
            // This is an unexpected error, please report this bug if it happens.
            if (ad == null)
            {
                Debug.LogError("Unexpected error: Rewarded load event fired with null ad and null error.");
                return;
            }

            // The operation completed successfully.
            Debug.Log("Rewarded ad loaded with response : " + ad.GetResponseInfo());
            rewardedAd = ad;

            // Register to ad events to extend functionality.
            RegisterEventHandlersRewardAd(ad);
        });
    }

    public void DestroyRewardAd()
    {
        if (rewardedAd != null)
        {
            Debug.Log("Destroying rewarded ad.");
            rewardedAd.Destroy();
            rewardedAd = null;
        }

        // Inform the UI that the ad is not ready.
        // AdLoadedStatus?.SetActive(false);
    }

    private void OnRewardedAdLoaded(object sender, EventArgs e)
    {
        numReqVideo = 0;
    }

    private void OnRewardedAdFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    {
        if (numReqVideo < GameAdConfig.MAX_REQUEST_VIDEO)
        {
            numReqVideo++;
            StartCoroutine(IERequestRewardedAd());
        }
    }

    IEnumerator IERequestRewardedAd()
    {
        yield return new WaitForSecondsRealtime(GameAdConfig.TIME_RELOAD_VIDEO);
        RequestAndLoadRewardedAd();
    }

    private void OnRewardedAdFailedToShow(object sender, AdErrorEventArgs e)
    {
        RequestAndLoadRewardedAd();
        hasReceivedRewardedAd = false;
        callbackRewardedAdClosed(ShowResult.Failed);
    }

    private void OnRewardedAdClosed(object sender, EventArgs e)
    {
        RequestAndLoadRewardedAd();
        StartCoroutine(IEWaitRewardedAdClosed());
    }

    IEnumerator IEWaitRewardedAdClosed()
    {
        yield return new WaitForEndOfFrame();

        if (hasReceivedRewardedAd)
            callbackRewardedAdClosed(ShowResult.Finished);
        else
            callbackRewardedAdClosed(ShowResult.Skipped);
    }

    private void OnUserEarnedReward(object sender, Reward e)
    {
        hasReceivedRewardedAd = true;
    }

    public bool IsRewardedAdLoaded()
    {
        return (rewardedAd != null && rewardedAd.CanShowAd());
    }

    public void ShowRewardedAd(Action<ShowResult> callback)
    {
        if (rewardedAd != null)
        {
            callbackRewardedAdClosed = callback;
            hasReceivedRewardedAd = false;
            rewardedAd.Show((Reward reward) =>
            {
                Debug.Log(String.Format("Rewarded ad granted a reward: {0} {1}",
                    reward.Amount,
                    reward.Type));

#if UNITY_EDITOR
                callbackRewardedAdClosed(ShowResult.Finished);
#else
                AdsManager.Instance.EligibleReward = callback;
#endif
                /* Request load for an other reward ad for next time */
                RequestAndLoadRewardedAd();
            });
        }
        else
        {
            RequestAndLoadRewardedAd();
        }
    }

    private void RegisterEventHandlersRewardAd(RewardedAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Rewarded ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () => { Debug.Log("Rewarded ad recorded an impression."); };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () => { Debug.Log("Rewarded ad was clicked."); };
        // Raised when the ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () => { Debug.Log("Rewarded ad full screen content opened."); };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () => { Debug.Log("Rewarded ad full screen content closed."); };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Rewarded ad failed to open full screen content with error : "
                           + error);
            callbackRewardedAdClosed(ShowResult.Failed);
        };
    }

    #endregion

    #region APPOPEN ADS

    public void RequestAndLoadAppOpenAd()
    {
        string adUnitId = "unused";

        // create new app open ad instance
        //AppOpenAd.LoadAd(adUnitId, ScreenOrientation.Portrait, CreateAdRequest(), (appOpenAd, error) =>
        //{
        //    if (error != null)
        //    {
        //        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        //        {
        //            Debug.LogError("AppOpenAd load failed, error: " + error);
        //        });
        //        return;
        //    }
        //    MobileAdsEventExecutor.ExecuteInUpdate(() =>
        //    {
        //        Debug.LogError("AppOpenAd loaded. Please background the app and return.");
        //    });
        //    this.appOpenAd = appOpenAd;
        //});
    }

    public void ShowAppOpenAd()
    {
        if (isShowingAppOpenAd)
        {
            return;
        }
        //if (appOpenAd == null)
        //{
        //    Debug.LogError("AppOpenAd not loaded.");
        //    return;
        //}
        //// Register for ad events.
        //this.appOpenAd.OnAdDidDismissFullScreenContent += (sender, args) =>
        //{
        //    isShowingAppOpenAd = false;
        //    MobileAdsEventExecutor.ExecuteInUpdate(() =>
        //    {
        //        Debug.Log("AppOpenAd dismissed.");
        //        if (this.appOpenAd != null)
        //        {
        //            this.appOpenAd.Destroy();
        //            this.appOpenAd = null;
        //        }
        //    });
        //};
        //this.appOpenAd.OnAdFailedToPresentFullScreenContent += (sender, args) =>
        //{
        //    isShowingAppOpenAd = false;
        //    var msg = args.AdError.GetMessage();
        //    MobileAdsEventExecutor.ExecuteInUpdate(() =>
        //    {
        //        Debug.LogError("AppOpenAd present failed, error: " + msg);
        //        if (this.appOpenAd != null)
        //        {
        //            this.appOpenAd.Destroy();
        //            this.appOpenAd = null;
        //        }
        //    });
        //};
        //this.appOpenAd.OnAdDidPresentFullScreenContent += (sender, args) =>
        //{
        //    isShowingAppOpenAd = true;
        //    MobileAdsEventExecutor.ExecuteInUpdate(() =>
        //    {
        //        Debug.Log("AppOpenAd presented.");
        //    });
        //};
        //this.appOpenAd.OnAdDidRecordImpression += (sender, args) =>
        //{
        //    MobileAdsEventExecutor.ExecuteInUpdate(() =>
        //    {
        //        Debug.Log("AppOpenAd recorded an impression.");
        //    });
        //};
        //this.appOpenAd.OnPaidEvent += (sender, args) =>
        //{
        //    string currencyCode = args.AdValue.CurrencyCode;
        //    long adValue = args.AdValue.Value;
        //    string suffix = "AppOpenAd received a paid event.";
        //    MobileAdsEventExecutor.ExecuteInUpdate(() =>
        //    {
        //        string msg = string.Format("{0} (currency: {1}, value: {2}", suffix, currencyCode, adValue);
        //        Debug.LogError(msg);
        //    });
        //};
        //appOpenAd.Show();
    }

    #endregion
}