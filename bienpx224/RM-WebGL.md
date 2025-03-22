# Unity vs WebGL 

## Lưu ý : 
- Không sử dụng Async/Await Task.Delay trong dự án build ra WebGL
- That is System.Threading. You cannot use System.Threading on WebGL (yet)

## Link hữu ích : 
- [Tips and tricks for using WebGL on desktop and mobile (tested up to 2021.3.11f1)](https://discussions.unity.com/t/tips-and-tricks-for-using-webgl-on-desktop-and-mobile-tested-up-to-2021-3-11f1/740556)

## Khi build WebGL để chạy game trên Telegram Mini App : 
- Sử dụng Unity 6 : ko có màn Splash 
- Trong Project Setting > Tích vào Auto Graphics API -> Sẽ là build webgl 2 : 
  + Khi chạy webgl 2 thì thấy trên iphone, safari gặp lỗi, thi thoảng bị crash liên tục. 
  + Build lại project ở Unity version 2022, bỏ tích Auto Graphic API và chọn web 1 và web 2 : thì thấy chạy trên IOS ổn. 
- Build ra webGL target trên điện thoại, và trên web thì nên sử dụng texture compress là ASTC 4x4, 6x6 hoặc 10x10 block : Size file giảm mà phù hợp tương thích.  
- Trên platform Android : RGBA ESTC 2 8 bit
- Trên PC thì DXT5 
## Unity tương tác với ReactJS phía web sau khi build ra WebGL :
- Sử dụng package npm (react-unity-webgl)[https://www.npmjs.com/package/react-unity-webgl]
- Document link : (How Communicate Unity vs React)[https://react-unity-webgl.dev/docs/api/event-system]
### Phía Unity : 
- VD : Tôi tạo 1 game Object bên ngoài Scene có tên là === User Data Manager , trong object đó tôi gắn script UserDataManager.cs để quản lý User data. 
- Trong UserDataManager.cs khai báo thêm các func để tương tác với React : 
```c# 
    #region Web Client Interacting

#if UNITY_WEBGL && !UNITY_EDITOR
    /* Khai báo các func mà UNITY có thể call tới WEB, khai báo cả trong file Asset/Plugins/React.jslib nữa */
    [DllImport("__Internal")]
    public static extern void UnitySayHelloToWeb(string str);
#endif
    
    /* Khai báo các func mà WEB có thể call tới UNITY */
    public void WebSayHelloToUnity(string msg)
    {
        Debug.Log("WebSayHelloToUnity : " + msg);
        Toast.Show(msg);
        EventManager.Instance.TriggerEvent(EventName.WebSayHeloToUnity,
            new Dictionary<string, object> { { "msg", msg } });
    }

    #endregion
```
- Tạo 1 file React.jslib trong Asset/Plugins/ : 
*** Lưu ý : Nếu ko tạo file define các func này trong Assets > Plugins > React.jsLib thì khi build ra webGL sẽ bị lỗi. 

```jslib
mergeInto(LibraryManager.library, {
  UnitySayHelloToWeb: function (msg) {
    window.dispatchReactUnityEvent("UnitySayHelloToWeb", UTF8ToString(msg));
  },
  GameOver: function (userName, score) {
      window.dispatchReactUnityEvent("GameOver", UTF8ToString(userName), score);
    },
});
```
- Vậy là đã setup xong cơ bản func test phía Unity. 
### Ở phía Web React : 
- Cài đặt npm react-unity-webgl: 
- Khai báo sendMessage khi load Unity ở đoạn code sau : 
```js
const { unityProvider, isLoaded, loadingProgression, sendMessage } = useUnityContext({
  loaderUrl: "build/myunityapp.loader.js",
    dataUrl: "build/myunityapp.data",
    frameworkUrl: "build/myunityapp.framework.js",
    codeUrl: "build/myunityapp.wasm",
    cacheControl: handleCacheControl,
  });

  /* Thực hiện việc cache bản build Unity. */
  function handleCacheControl(url: string) {
    return "immutable";
    if (url.match(/\.data/) || url.match(/\.bundle/)) {
      return "must-revalidate";
    }
    if (url.match(/\.mp4/) || url.match(/\.wav/)) {
      return "immutable";
    }
    return "no-store";
  }
```
- Ở phía web VD ta gắn onClick cho 1 button : 
```js
function handleClickButtonTest() {
    sendMessage("=== User Data Manager", "WebSayHelloToUnity", "Hi~ I'm web client");
  }
```