
# Ads Manager and integration : 

## Google Admob : 
- Import Google Admob SDK : Follow instruction in [link](https://developers.google.com/admob/unity/quick-start#android)

## Unity Ads : 
- Follow Unity Home page to see instruction. 

## Ads Manager files: 
- Import list scripts in this folder : `AdsManager, Admobs, UnityAds, GameAdConfig, Singleton`. 
- Create GO in first scene, add `AdsManager.cs` component. 
- The flow of works : Game loaded -> Init Ads -> Load Ads -> Show Ads in game -> Show completed -> Reload an other ad. 
- Here we are maining focus on Google Admob and Unity Ads. Init both of them. But I prefer showing Google Admob first, if it's available, so change to show Unity ads. 
- Config ad unit id, app id and some params in `GameAdConfig.cs`.
- Config app id for Google admob in Assets > Google Mobile Ads > Settings ... 

## Error : get_gameObject can only be called from the main thread :
- Lỗi này xảy ra khi đang thực hiện xem video ads xong để nhận phần thưởng phiên bản Unity là 2021.3.13f, phiên bản gg admob là 8.6.
- "I think the problem is, after playing rewarded video you can't call unity method before unity switches to main thread again. You need to wait till onApplicationPause called."
- Trong Editor thì việc xem video xong, call func nhận thưởng ở onRewardedVideoClosed hoạt động tốt. 
Tuy nhiên khi thực chạy trên thiết bị Android thì khi quảng cáo bật lên, main thread của Unity sẽ bị pause lại, và khi video kết thúc thì đã thực hiện hàm nhận thưởng trước khi main thread được unpause -> có lỗi trên. 
- Cách giải quyết : 
Thêm biến check để đợi khi nào main thread hoạt động trở lại thì sẽ thực hiện nhận thưởng cũng như các actions khác.
-> Cách này đã dc triển khai trong cuối file `AdsManager.cs` 
- (Link issues in Unity forum)[https://forum.unity.com/threads/solved-bizarre-error-unityexception-get_gameobject-can-only-be-called-from-the-main-thread.539830/]

## Kinh nghiệm setup show ads : 
- Khoảng cách giữa các lần show ad inter liên tiếp là : ít nhất 30s tới 60s.


## Lưu ý Khi tích hợp Google ads mà build bản Release cho Production : 
- Khi build bản Release, sử dụng minify release mà ko custom Proguard thì khi build nó sẽ ko build các lib như gms, gms.ads, ump,...
- Vậy nên khi build rồi chạy thì sẽ bị lỗi Exception ClassNotFound của các lib trên. 
- Cách giải quyết : 
    + Trong Project Settings > Publishing Setting > Chọn Custom Proguard : 
    + Trong file proguard-user.txt đó thêm đoạn mã sau : 
    ```c#
        -keep class com.google.unity.** {
   *;
}
-keep public class com.google.android.gms.ads.**{
   public *;
}
-keep public class com.google.ads.**{
   public *;
}
-keep public class com.google.ump.**{
   public *;
}
-keepattributes *Annotation*
-dontobfuscate
    ```

- Khi mở project ra hoặc chuyển sang branch build release, nên Force Resolve trước. Đã gặp TH ko Force Resolve, build vẫn bị proguard ignore.
- Vậy là đã giải quyết xong. 


## Unity admob show trên Iphone game bị pause, hết ads vẫn không resume lại : 
- Khi show ads, trên Android sẽ pause game lại, kết thúc ads thì resume. Còn trên Iphone thì không. Muốn pause game khi show ad thì cần `MobileAds.SetiOSAppPauseOnBackground(true);` khi init Admob. 
Và trên Iphone cần thêm đoạn chỉnh `Time.timeScale = 1` khi ads đã bị tắt đi hoặc trong OnApplicationPause. 

[Link](https://discussions.unity.com/t/interstitial-pause-game-on-android-but-not-on-ios/195399/6)