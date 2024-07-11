# Experiences about Firebase :

- Thực hiện tạo tài khoản, tạo app trên firebase, tải file google-services về để trong Assets folder, import SDK package vào trong game. (Lưu ý : dùng tính năng nào của Firebase thì import SDK đó thôi để xem run dc ko. đề phòng lỗi). Trên Document có Step 5: Confirm Google Play services version requirements. Tuy nhiên khi minhd add đoạn code này vào thì Run project bị gì đó, có thể bị loop hay lỗi làm game ko chạy dc (Đang chạy Unity 2021.3.13f, Máy Mac M1). Nên đã bỏ ko thêm đoạn code này để chạy bt. 
## Firebase Analytics : Event tracking : 
- Tạo 1 file FirebaseManager.cs : Singleton để trong DontDestroyOnLoad : Để quản lý các func thực hiện tương tác với firebase. Sau nếu nhiều tác vụ có thể chia nhỏ: VD: FirebaseEventTracking, FirebaseRealtime, FirebaseNotification... 
- Trong file FirebaseManager có các func thực hiện việc bắn các event tracking lên firebase. Ở đây mình tạo enum để quản lý các eventName. Lưu ý các eventName này không được trùng với 1 số eventName có sẵn của firebase. Tham khảo trong đây: https://support.google.com/firebase/answer/9234069?visit_id=638205830564178149-501493015&rd=1 
VD : 
``` 
public static void PushEventRoom(string eventName, int chapterId, int levelId, int roomId, int roomIndex)
    {
        try
        {
            Firebase.Analytics.FirebaseAnalytics.LogEvent(
            eventName,
            new Firebase.Analytics.Parameter[] {
            new Firebase.Analytics.Parameter(
            "chapterId", chapterId),
            new Firebase.Analytics.Parameter(
            "levelId", levelId),
            new Firebase.Analytics.Parameter(
            "roomId", roomId),
            new Firebase.Analytics.Parameter(
            "roomIndex", roomIndex),
            }
            );
            Debug.Log("Push Event DONE : " + eventName);
        }
        catch (Exception e)
        {
            Debug.Log("Push Event ERROR : " + e.ToString());
        }
    }
```

## Firebase DLL bị nặng, ko đẩy lên git được : 
- Khi start 1 dự án có sử dụng firebase, import thư viện firebase, sẽ có 1 số file DLL trong folder Assets/Firebase/Plugins/ 
- Nếu ko muốn sử dụng Git Large Files để đấy những file này lên thì hãy ignore nó đi.
- Muốn ignore file nặng này, mở .gitignore lên và thêm `/Assets/Firebase/Plugins/x86_64/` vào cuối file là được. 
- Và khi import, mở dự án ở 1 nơi khác chưa có chúng, thì import lại Firebase vào là được. 

## Import Firebase .bundle bị lỗi ko open được : 
- Khi import Firebase vào dự án, Run dự án bị báo lỗi OS ko chạy file .bundle và yêu cầu xoá nó đi
- Ta import lại Firebase. Vào Assets/Firebase/Plugins > Mở firebase.bundle đó trong VSCode. 
- Sau đó vào game chạy lại thì thấy đã thành công, push được firebase, ko còn hiện lỗi nữa. 
- Nêú vào game chạy vẫn bị báo lỗi ko open dc dc .bundle thì cancel Popup đó, rồi vào MacOS Setting > Privacy > scroll to end and Allow Anyway for this .bundle file.

