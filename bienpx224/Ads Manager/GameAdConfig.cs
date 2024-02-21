using Unity.VisualScripting;
using UnityEngine;

public class GameAdConfig
{
    [Header("UnityAds")]
#if UNITY_ANDROID
    public const string ID_UNITY = "";

    public const string INTER_UNITY = "Interstitial_Android";
    public const string VIDEO_UNITY = "Rewarded_Android";
#else
    public const string ID_UNITY = "";
    public const string INTER_UNITY = "Interstitial_iOS";
    public const string VIDEO_UNITY = "Rewarded_iOS";

#endif

#if TEST_AD
    public const string ID_ADMOB = "ca-app-pub-3940256099942544~3347511713";
    public const string BANNER_ADMOB = "ca-app-pub-3940256099942544/6300978111";
    public const string INTER_ADMOB = "ca-app-pub-3940256099942544/1033173712";
    public const string OPEN_AD_ADMOB = "ca-app-pub-3940256099942544/3419835294";
    public const string VIDEO_ADMOB = "ca-app-pub-3940256099942544/5224354917";
#else
    #if UNITY_ANDROID
        public const string ID_ADMOB = "ca-app-pub-8007611864270096~";
        public const string BANNER_ADMOB = "ca-app-pub-8007611864270096/";
        public const string INTER_ADMOB = "ca-app-pub-8007611864270096/";
        public const string OPEN_AD_ADMOB = "";
        public const string VIDEO_ADMOB = "ca-app-pub-8007611864270096/";
    #else
        public const string ID_ADMOB = "ca-app-pub-3940256099942544~3347511713";
        public const string BANNER_ADMOB = "ca-app-pub-3940256099942544/6300978111";
        public const string INTER_ADMOB = "ca-app-pub-3940256099942544/1033173712";
        public const string OPEN_AD_ADMOB = "ca-app-pub-3940256099942544/3419835294";    
        public const string VIDEO_ADMOB = "ca-app-pub-3940256099942544/5224354917";
    #endif
#endif

    public const int MAX_REQUEST_BANNER = 5;
    public const int TIME_RELOAD_BANNER = 15;

    public const int MAX_REQUEST_INTER = 5;
    public const int TIME_RELOAD_INTER = 15;

    public const float MIN_TIME_SHOW_NEXT_AD = 1f; /* 360s = 6 minutes, default = 100f */


    public const int MAX_REQUEST_VIDEO = 5;
    public const int TIME_RELOAD_VIDEO = 15;

    public const string ADMOB = "admob";
    public const string UNITY = "unity";
}