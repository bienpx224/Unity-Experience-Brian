# Unity-Experience-Brian
Base experience about Unity : 

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

## Animator and Animations for movements: 
- Vào Object Player, tạo Animations và Animator: 
- Kéo Sprites vào khung animations, chỉnh sửa timing cho phù hợp. VD cho anim Run
- Vào Animator chỉnh transactions. Tạo transaction từ AnyState sang Idle, từ Idle sang Run. 
- Trong mũi tên từ Idle > Run : Tắt has exit time và chỉnh Transition Duration trong Setting = 0.
- Tạo Param để làm điều kiện chuyển đổi anim. VD tạo biến bool isRun. Sau đó trong mũi tên từ Idle > Run ta thêm điều kiện isRun = true.

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



