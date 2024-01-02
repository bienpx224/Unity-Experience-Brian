# Unity vs WebGL 

## Unity tương tác với ReactJS phía web sau khi build ra WebGL :
- Sử dụng package npm (react-unity-webgl)[https://www.npmjs.com/package/react-unity-webgl]
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
- Ở phía web VD ta gắn onClick cho 1 button : 
```js
function handleClickButtonTest() {
    sendMessage("=== User Data Manager", "WebSayHelloToUnity", "Hi~ I'm web client");
  }
```