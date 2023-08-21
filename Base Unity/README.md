# Unity-Experience-Brian
Base experience about Unity : 

## Start, OnEnable và Awake : 
- Awake dc gọi đầu tiên, và chỉ gọi 1 lần khi Spawn Object. 
- TIếp đến sẽ gọi OnEnable, sau đó mới gọi đến Start.
- Tuy nhiên hàm Start sẽ chỉ được gọi 1 lần đầu khi Spawn Object. Còn hàm OnEnable thì sẽ được gọi mỗi khi Object được enable lên. 
## Canvas : 
- Trong Canvas > Canvas Scaler nên chọn Scale with Screen Size. 

## Transform , RectTransform
- Trong RectTransform: Để setup position của RectTransform: dùng hàm .anchoredPosition, tuy nhiên hàm này chỉ set cho pos x và y (Bỏ qua pos z). 
Còn pos z thì phải set qua hàm .localPosition (chỉ set pos z, bỏ qua pos x và y)

- Transform thường dùng localPosition để set position dựa theo position của parent. Nếu dùng .position thì sẽ là vị trí tính theo world position.

## Vector : 
(https://www.youtube.com/watch?v=YUk-A9dAPJk)
- Ví dụ: Vector(4,3) thì có toạ độ x = 4 và y = 3, hướng của vector là hướng lên trên về phía Đông.
- Công thức tính độ dài Vector : magnitude = căn bậc 2 (x bình + y bình phương)
- Nomalize() dùng để chuẩn hoá vector, đưa độ dài về 1 nhưng giữ nguyên hướng của vector.
- AddForce() thường dùng để rigidbody nhận 1 lực là 1 vector()
- rigidbody.velocity = new Vector2(x,y) : Dùng để thay đổi vận tốc theo Vector 

## IEnumerator và IEnumerable : 
- Trong C# có rất nhiều kiểu danh sách như Dictionary, List, ArrayList, HashSet.. và ngay kiểu dữ liệu string đều implement từ IEnumerable hoặc IEnumerable<T>.
- Vậy IEnumerable giúp một đối tượng có thể thực hiện duyệt các phần tử bằng foreach
( https://codelearn.io/sharing/ienumerable-va-ienumerator-trong-csharp ) 
- Cấu trúc hàm : 
```c# 
public interface IEnumerable
{
	IEnumerator GetEnumerator();
}
```
- Cấu trúc của IEnumerator : 
```c#
public interface IEnumerator
{
	object Current { get; }
	bool MoveNext();
	void Reset();
}
public interface IEnumerator<out T> : IDisposable, IEnumerator
{
	T Current { get; }
}
```
- IEnumerator là kiểu trả về của StartCoroutine. Ta thường dùng để tạo hàm thực thi của Coroutine. 
Với mỗi lần gặp yield return thì hàm đó sẽ trả về kết quả yield return tại thời điểm đó và tạm dừng hàm đó tại frame đó. Đến frame tiếp theo, nó sẽ tiếp tục chạy hàm đó ở đoạn dừng trước đó, đến khi nào hết hàm IEnumerator đó hoặc gặp yield break;

## THe In, Ref and Out in c# : 
Method parameters have modifiers available to change the desired outcome of how the parameter is treated. Each method has a specific use case:

ref is used to state that the parameter passed may be modified by the method.

in is used to state that the parameter passed cannot be modified by the method.

out is used to state that the parameter passed must be modified by the method.
Both the ref and in require the parameter to have been initialized before being passed to a method. The out modifier does not require this and is typically not initialized prior to being used in a method.

## Phân biệt giữa event, delegate, action, unity event : 
- Link refers : https://gamedevbeginner.com/events-and-delegates-in-unity/

## Hướng dẫn dùng delegate
*** Hướng dẫn dùng delegate : 

Khai báo đầu game : 
    public delegate void OnSelectHandler();
Khai báo biến sử dụng delegate : 
    public OnSelectHandler _leftSelectEvent;
Gắn call back cho event đó : 
                _leftSelectEvent += item.OnNewSelect;
Ở chỗ cần bắn event thì call :         _leftSelectEvent.Invoke();
-> Func item.OnNewSelect sẽ được gọi 

## Làm việc với WorldToViewportPoint : 
- cam.WorldToViewportView(object) là hàm để chuyển từ vị trí world position sang Vector3 vị trí của object đó trong Camera View. Góc trái bên dưới cùng sẽ là Vector3(0,0,0) -> ở giữa là (0.5, 0.5, 0). Nếu nằm trong viewport thì giá trị sẽ dao động trong [0, 1]
- VD : Để kiểm tra gameObject nằm trong hay ngoài camera : 
```
// Kiểm tra nếu enemy nằm trong hay ngoài camera
        Vector3 enemyViewportPos = mainCamera.WorldToViewportPoint(transform.position);
        bool isEnemyVisible = enemyViewportPos.x >= 0 && enemyViewportPos.x <= 1 && enemyViewportPos.y >= 0 && enemyViewportPos.y <= 1;
```
- cam.ViewportToWorldPoint : CHuyển đổi position vị trí từ viewport trong camera thành world position. 

## Hướng dẫn sử dụng Event Manager để quản lý, publishing và listerner event trong game : 
- Link : http://bernardopacheco.net/using-an-event-manager-to-decouple-your-game-in-unity
- Bản chất : Tạo 1 Singleton Event Manager để quản lý list các event, subscribe và trigger các event. 
- Listening và Trigger các event dựa theo event Name được định nghĩa là enum để dễ quản lý. 
- Xem file EventManager.cs template ở cùng folder này. 
- Các param của các event được truyền dưới dạng Dictionary<string, object>, khi muốn lấy ra ta cần ép kiểu để chuẩn chỉnh hơn. 
## Build UNITY IOS

1. Switch to IOS platform.
2. Add firebase file to asset folder
3. Để ý thay đổi id quảng cáo admob
4. Builder Player Setting Config, xem lại 1 lượt. 
5. Build ra folder Xcode
6. Nếu có lỗi, chưa có pod, mở terminal folder đó, chạy pod install
7. Mở folder đó bằng Xcode với .xcodeproject
8. Check lại signature, project team trong Hirearchy
9. Build máy thật. 

Compress cho Android size/4 : RGBA Compressed ETC2 8 bits


Build android, ko cần x86 -> giảm dung lượng app
standard assets của Unity có sẵn model, texture, … dùng sẵn
Smart Localization giúp multi language dễ dàng. Import thôi





