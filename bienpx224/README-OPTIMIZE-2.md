# OPTIMIZE IN UNITY 
Sưu tầm : [Link](https://hocunity.3dvietpro.vn/default.aspx?g=posts&t=5588)
## 1: Dưới đây là một số lưu ý quan trọng khi code trong Unity để tối ưu code, các bạn xem và tận dụng:

Sử dụng hàm Awake thay vì Start: Hàm Awake được gọi trước hàm Start, nên sử dụng nó để khởi tạo các biến trong scene.

Sử dụng Object Pooling: Việc tạo và hủy đối tượng trong Unity là một hoạt động tốn tài nguyên, nên sử dụng Object Pooling để tối ưu việc tạo và hủy đối tượng.

Sử dụng Occlusion Culling: Sử dụng Occlusion Culling để loại bỏ các đối tượng không cần thiết khỏi hiển thị.

Sử dụng Batching: Sử dụng Batching để gộp nhiều đối tượng cùng kiểu lại với nhau để tối ưu việc vẽ.

Sử dụng Profiler: Sử dụng Profiler để phân tích và tìm ra những điểm cần cải thiện trong code.

Sử dụng Static Batching: Sử dụng Static Batching để tối ưu việc vẽ các đối tượng tĩnh.

Sử dụng Async Operation: Sử dụng Async Operation để tải nội dung bất đồng bộ và không làm chậm trò chơi.


Dưới đây là chi tiết một Cách sử dụng Occlusion Culling trong Unity:



Mở cài đặt Occlusion Culling: Trong Unity Editor, chọn "Window" > "Rendering" > "Occlusion Culling" để mở cài đặt Occlusion Culling.

Tạo một mảng Occlusion Area: Tạo một hoặc nhiều mảng Occlusion Area trong scene bằng cách chọn "GameObject" > "3D Object" > "Occlusion Area".

Cấu hình mảng Occlusion Area: Chọn mảng Occlusion Area và cấu hình thuộc tính trong Inspector, bao gồm Size và Center.

Đặt các mảng Occlusion Area xung quanh các đối tượng mà bạn muốn làm việc với Occlusion Culling.

Chạy bake: Trong cài đặt Occlusion Culling, chọn "Bake" để chạy quá trình bake và tạo các thông tin về Occlusion.

Sau khi hoàn thành các bước trên, Unity sẽ tự động loại bỏ các đối tượng không nhìn thấy từ camera và giảm tải cho trò chơi.

## 2: Để tối ưu code trong Unity C#, có một số kỹ thuật mà bạn có thể sử dụng:
1. Sử dụng tối ưu hoá mức độ tốt nhất: Để tối ưu hoá game của bạn, hãy sử dụng mức độ tối ưu hoá tốt nhất trong Unity.
2. Sử dụng cache cho các biến thường dùng: Sử dụng cache cho các biến thường dùng giúp giảm thời gian tìm kiếm và truy cập các giá trị.
3. Sử dụng pooling cho các đối tượng tạo ra động: Sử dụng pooling cho các đối tượng tạo ra động giúp giảm số lượng bộ nhớ được sử dụng và tăng tốc độ tạo ra đối tượng.
4. Sử dụng coroutines thay vì Update: Sử dụng coroutines thay vì Update giúp giảm số lần gọi hàm và tối ưu hoá việc thực thi.
5. Sử dụng multithreading cho các tác vụ tải dữ liệu: Sử dụng multithreading cho các tác vụ tải dữ liệu giúp tăng tốc độ tải dữ liệu và giảm tải trên luồng chính của game.
Lưu ý: Các kỹ thuật trên có thể giúp tối ưu hoá code trong Unity C#, nhưng cần phải tùy chỉnh cho mỗi trường hợp cụ thể của bạn và kiểm tra các hiệu năng của code sau khi tối ưu.
6. Sử dụng component component instead of GameObject: Sử dụng component thay vì GameObject giúp giảm số lượng bộ nhớ được sử dụng và tăng tốc độ truy cập các component.
7. Sử dụng một lớp quản lý cho các component chung: Sử dụng một lớp quản lý cho các component chung giúp giảm số lần gọi hàm và tăng tốc độ truy cập các component.
8. Sử dụng lazy evaluation: Sử dụng lazy evaluation giúp giảm số lần tính toán và tăng tốc độ truy cập các giá trị.
9. Sử dụng các phương thức tối ưu: Sử dụng các phương thức tối ưu như StringBuilder thay vì String concatenation giúp giảm số lượng bộ nhớ được sử dụng và tăng tốc độ xử lý.
10. Sử dụng các kỹ thuật tối ưu khác: Có rất nhiều kỹ thuật tối ưu khác có thể áp dụng cho code trong Unity C# như sử dụng các phương thức tối ưu trong LINQ, sử dụng các biến static thay vì instance variables, vv.
11. Tối ưu hình ảnh và audio: Tối ưu hình ảnh và audio bằng cách giảm kích thước và chất lượng của chúng giúp giảm tải trên bộ nhớ và tăng tốc độ truy cập.
12. Sử dụng các công cụ phân tích hiệu năng: Sử dụng các công cụ phân tích hiệu năng như Unity Profiler giúp bạn tìm ra và giải quyết các vấn đề hiệu năng.
13. Sử dụng các tính năng mới trong Unity: Luôn tìm hiểu về các tính năng mới trong Unity và sử dụng chúng để giải quyết các vấn đề hiệu năng và tối ưu hoá dự án.
14. Đồng bộ hóa các tác vụ để tránh việc chạy song song: Tránh việc chạy song song các tác vụ giúp giảm tải trên CPU và tăng tốc độ xử lý.
15. Tối ưu hoá mạng và truyền dữ liệu: Tối ưu hoá mạng và truyền dữ liệu bằng cách sử dụng các giao thức và công nghệ mạng hiệu quả và giảm tải trên mạng.
16. Sử dụng các cấu hình tối ưu: Sử dụng các cấu hình tối ưu để tăng tốc độ xử lý và giảm tải trên hệ thống. Điều này có thể bao gồm cấu hình cho hệ thống xử lý, bộ nhớ và mạng.
17. Sử dụng các công cụ phân tích hiệu năng: Sử dụng các công cụ phân tích hiệu năng để phân tích và tìm ra các vấn đề hiệu năng và cải thiện chúng.
18. Giữ mã code dễ đọc và sử dụng tốt: Giữ mã code dễ đọc và sử dụng tốt giúp cho những người khác có thể dễ dàng hiểu và tìm ra các vấn đề với mã của bạn.

CÁCH 2: Sử dụng cache cho các biến thường dùng
Cache là một bộ nhớ tạm thời trong máy tính hoặc trong hệ thống máy tính, được sử dụng để lưu trữ các dữ liệu thường xuyên sử dụng hoặc từ Internet. Việc sử dụng cache giúp tăng tốc độ xử lý và truy xuất dữ liệu, bởi vì dữ liệu đã được lưu trữ trong cache có thể truy xuất nhanh hơn so với việc truy xuất từ bộ nhớ chính hoặc từ Internet.
Cache cũng có thể được sử dụng trong lập trình để lưu trữ các giá trị tạm thời của các biến hoặc đối tượng, để truy xuất nhanh hơn khi cần sử dụng lại.
Sử dụng cache cho các biến thường dùng là một kỹ thuật tối ưu hoá mà bạn có thể sử dụng để giảm tải trên bộ nhớ và tăng tốc độ xử lý. Khi một biến được sử dụng rất nhiều trong một chương trình, việc lấy giá trị của biến này từ bộ nhớ có thể làm chậm tốc độ xử lý của chương trình. Để giải quyết vấn đề này, bạn có thể sử dụng cache để lưu giá trị của biến đó trong bộ nhớ đệm, đảm bảo rằng việc truy xuất giá trị của biến này sẽ nhanh hơn.
Cách sử dụng cache cho các biến thường dùng trong Unity C# như sau:
1. Tạo một biến toàn cục để lưu giá trị của biến cần cache: Tạo một biến toàn cục với kiểu dữ liệu tương ứng với biến cần cache.
2. Lưu giá trị của biến vào cache: Sử dụng một hàm hoặc phương thức để lưu giá trị của biến vào biến toàn cục tạo ra trong bước 1.
3. Sử dụng giá trị từ cache: Khi bạn muốn truy xuất giá trị của biến, hãy sử dụng biến toàn cục để truy xuất giá trị từ cache. Nếu giá trị của biến đã được lưu trữ trong cache, bạn sẽ truy xuất giá trị này từ cache mà không cần phải truy xuất lại từ bộ nhớ.
4. Cập nhật giá trị trong cache: Nếu giá trị của biến thay đổi, hãy cập nhật giá trị trong cache bằng cách gọi hàm hoặc phương thức để cập nhật giá trị mới vào biến toàn cục.
Lưu ý: Sử dụng cache chỉ có lợi khi biến được sử dụng rất nhiều và việc truy xuất giá trị từ bộ nhớ của biến đó có thể làm chậm tốc độ xử lý của chương trình. Nếu biến chỉ được sử dụng ít, sử dụng cache sẽ không có lợi gì về tốc độ xử lý.
Cách sử dụng cache trong Unity C# rất đơn giản:
1. Khai báo biến hoặc đối tượng là "static": Bạn có thể sử dụng từ khóa static để khai báo các biến hoặc đối tượng là "static".
Ví dụ:
public static Transform playerTransform;
2. Gán giá trị cho biến hoặc đối tượng: Sau khi khai báo biến hoặc đối tượng, bạn cần gán giá trị cho nó trong quá trình khởi tạo hoặc trong một hàm khác.
Ví dụ:
void Start()
{
playerTransform = transform;
}
3. Truy cập biến hoặc đối tượng từ bất kỳ nơi nào trong mã: Sau khi gán giá trị cho biến hoặc đối tượng, bạn có thể truy cập nó từ bất kỳ nơi nào trong mã.
Ví dụ:
void Update()
{
transform.position = playerTransform.position;
}
Sử dụng cache sẽ giúp giảm thời gian tìm kiếm và truy xuất dữ liệu, tăng tốc độ xử lý của game.

CÁCH 3: Sử dụng pooling cho các đối tượng tạo ra động
Sử dụng pooling cho các đối tượng tạo ra động trong Unity C# là một trong những cách tiếp cận tối ưu mà bạn có thể sử dụng để giảm chi phí tạo ra và hủy đối tượng mỗi khi chúng cần thiết. Đây là một quá trình được thực hiện theo các bước sau:
1. Tạo một class pool: Bạn cần tạo một class pool để quản lý các đối tượng cần tạo ra và hủy.
```c#
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
public GameObject prefab;
private List<GameObject> inactiveInstances = new List<GameObject>();

private GameObject GetInactiveInstance()
{
GameObject spawnedGameObject;
int lastIndex = inactiveInstances.Count - 1;
if (lastIndex >= 0)
{
spawnedGameObject = inactiveInstances[lastIndex];
inactiveInstances.RemoveAt(lastIndex);
}
else
{
spawnedGameObject = (GameObject)GameObject.Instantiate(prefab);

ObjectPoolItem poolItem = spawnedGameObject.AddComponent<ObjectPoolItem>();
poolItem.pool = this;
}
return spawnedGameObject;
}

public void ReturnObjectToPool(GameObject toReturn)
{
ObjectPoolItem poolItem = toReturn.GetComponent<ObjectPoolItem>();
if (poolItem != null && poolItem.pool == this)
{
toReturn.SetActive(false);
inactiveInstances.Add(toReturn);
}
else
{
Debug.LogWarning(toReturn.name + " was returned to a pool it wasn't spawned from! Destroying.");
GameObject.Destroy(toReturn);
}
}
}
```
2. Tạo một class pool item: Bạn cần tạo một class pool item để giữ tham chiếu đến pool của đối tượng.
using UnityEngine;

public class ObjectPoolItem : MonoBehaviour
{
public ObjectPool pool;
}
3. Tạo một đối tượng pool: Bạn cần tạo một đối tượng pool từ prefab của đối tượng mà bạn muốn sử dụng pooling. Đối tượng pool này có thể được tạo ra trong scene hoặc trong một prefab riêng biệt.
4. Sử dụng pooling: Khi bạn muốn tạo ra một đối tượng, hãy gọi hàm GetInactiveInstance() trong class pool để lấy ra một đối tượng từ pool. Nếu pool chưa có đối tượng, hàm này sẽ tạo ra một đối tượng mới từ prefab. Khi bạn không còn sử dụng một đối tượng, hãy gọi hàm ReturnObjectToPool(GameObject toReturn) để trả lại đối tượng vào pool để sử dụng lại sau này.
Với việc sử dụng pooling, bạn sẽ giảm chi phí tạo ra và hủy đối tượng mỗi khi chúng cần thiết, giúp tăng tốc độ chạy của game của bạn.

CÁCH 4: Sử dụng coroutines thay vì Update
1. Tạo một coroutine: Bạn có thể tạo một coroutine bằng cách sử dụng hàm StartCoroutine(). Hàm này yêu cầu một đối số là một delegate hoặc một chuỗi tên của hàm.
2. Thực hiện các tác vụ trong coroutine: Trong coroutine, bạn có thể thực hiện bất kỳ tác vụ nào mà bạn muốn, bao gồm cả chờ cho một số thời gian bằng cách sử dụng hàm yield return new WaitForSeconds(time).
3. Sử dụng coroutines: Khi bạn muốn thực hiện một tác vụ trong một coroutine, hãy gọi hàm StartCoroutine() với đối số là delegate hoặc tên của hàm tương ứng.
Với việc sử dụng coroutines, bạn có thể tách biệt các tác vụ khác nhau và chạy chúng theo thứ tự của mình, giúp giảm sự rối mắc và tăng tốc độ chạy của game. Điều này đặc biệt hữu ích khi bạn cần chạy nhiều tác vụ song song hoặc khi bạn muốn chạy một tác vụ trong một thời gian nhất định.
- Delegate là một kiểu dữ liệu trong C# mà cho phép bạn tạo một tham chiếu đến một hàm. Nó giống như một biến, nhưng nó chứa tham chiếu đến một hàm cụ thể. Delegate cho phép bạn truyền một hàm như một đối số cho một hàm khác hoặc trả về một hàm như kết quả của một hàm. Điều này giúp cho việc viết mã trở nên linh hoạt và dễ dàng hơn, giúp bạn tách rời logic của một hàm ra khỏi hàm gọi nó.
Ví dụ về delegate:
```c#
delegate int DelegateExample(int x, int y);
class Program
{
static int Add(int x, int y)
{
return x + y;
}
static int Multiply(int x, int y)
{
return x * y;
}
static void Main(string[] args)
{
DelegateExample delegateAdd = new DelegateExample(Add);
DelegateExample delegateMultiply = new DelegateExample(Multiply);

Debug.Log(delegateAdd(10, 20));
Debug.Log (delegateMultiply(10, 20));

Console.ReadLine();
}
}
```
Ví dụ tiếp theo về
Trong ví dụ trên, chúng ta định nghĩa một delegate với tên DelegateExample với 2 tham số int x và int y. Sau đó, chúng ta tạo 2 hàm Add và Multiply sử dụng delegate DelegateExample. Cuối cùng, chúng ta tạo 2 đối tượng delegate delegateAdd và delegateMultiply và gán cho chúng 2 hàm Add và Multiply. Khi gọi delegate, nó sẽ gọi hàm được gán cho nó.
Ví dụ tiếp theo về delegate:
```c#
using UnityEngine;
public class ExampleClass : MonoBehaviour
{
// Tạo delegate có tên MyDelegate
public delegate void MyDelegate(int num);

// Tạo một hàm có thể được gọi bằng delegate
public void MyFunction(int num)
{
Debug.Log("MyFunction called with num: " + num);
}

private void Start()
{
// Tạo một instance của delegate trỏ đến hàm MyFunction
MyDelegate myDelegate = new MyDelegate(MyFunction);

// Gọi hàm MyFunction bằng delegate
myDelegate(10);
}
}
```
Trong ví dụ trên, chúng ta tạo một delegate có tên MyDelegate với kiểu trả về là void và một tham số là int. Sau đó, chúng ta tạo một hàm MyFunction mà delegate có thể trỏ đến. Trong hàm Start, chúng ta tạo một instance của delegate trỏ đến hàm MyFunction và gọi hàm MyFunction bằng delegate.
Kết quả từ chương trình sẽ in ra: "MyFunction called with num: 10".

CÁCH 5: Sử dụng multithreading cho các tác vụ tải dữ liệu
Sử dụng multithreading là một kỹ thuật tốt để tải dữ liệu trong Unity, đặc biệt là khi tải dữ liệu có dung lượng lớn hoặc tải dữ liệu từ mạng. Khi tải dữ liệu từ mạng, nếu chỉ sử dụng một luồng, việc tải dữ liệu sẽ gây ra tình trạng blocking và gián đoạn các tác vụ khác trong game.
Để sử dụng multithreading để tải dữ liệu, bạn có thể sử dụng Thread hoặc Task từ thư viện System.Threading trong C#. Bạn có thể tạo một luồng mới để tải dữ liệu và gọi hàm Join để chờ luồng tải dữ liệu hoàn tất.
Ví dụ:
```c#
using System.Threading;
using UnityEngine;

public class ExampleClass : MonoBehaviour
{
private void Start()
{
// Tạo một luồng mới để tải dữ liệu
Thread loadDataThread = new Thread(LoadData);
loadDataThread.Start();
}

private void LoadData()
{
// Tải dữ liệu từ mạng
// ...

// Gọi hàm MainThreadUpdate để cập nhật dữ liệu tải được vào Main Thread
MainThreadUpdate();
}

private void MainThreadUpdate()
{
// Cập nhật dữ liệu tải được vào Main Thread
// ...
}
}
```
Multithreading có thể giúp bạn tải dữ liệu một cách hiệu quả và tránh làm chậm hoạt động của ứng dụng của bạn. Cách thức sử dụng multithreading trong Unity C# như sau:
1. Tạo một luồng mới: Bạn có thể tạo một luồng mới bằng cách sử dụng lớp Thread. Bạn có thể chỉ định một phương thức nào đó để chạy trên luồng mới này.
2. Sử dụng Async/Await: Bạn có thể sử dụng từ khoá Async và Await để tải dữ liệu trong một luồng riêng biệt và đợi cho nó hoàn thành trước khi tiếp tục với các tác vụ khác.
3. Sử dụng Task Parallel Library (TPL): TPL là một thư viện cung cấp các tiện ích để tạo và quản lý các tác vụ phức tạp trong một luồng riêng biệt. Bạn có thể sử dụng TPL để tải dữ liệu trong một luồng riêng biệt và đợi cho nó hoàn thành trước khi tiếp tục với các tác vụ khác.
Lưu ý rằng multithreading trong Unity có một số hạn chế và cần phải được sử dụng cẩn thận để tránh gây ra các vụ tải dữ liệu là một trong những nhiệm vụ mà việc sử dụng multithreading có thể cải thiện tốc độ và hiệu suất. Bằng cách sử dụng multithreading, các tác vụ tải dữ liệu có thể chạy trong một luồng riêng biệt, tránh làm chậm hoạt động của các luồng chính trong ứng dụng
Ví dụ, nếu bạn muốn tải dữ liệu từ một URL, bạn có thể sử dụng một luồng để thực hiện tác vụ tải dữ liệu, trong khi các luồng chính tiếp tục hoạt động bình thường. Điều này có thể được thực hiện bằng cách sử dụng lớp Thread trong C#.
Ví dụ:
using System.Threading;

// Tạo một luồng mới để tải dữ liệu
Thread downloadThread = new Thread(DownloadData);
downloadThread.Start();

// Phương thức tải dữ liệu
private void DownloadData()
{
// Tải dữ liệu từ một URL
// ...
}
Sử dụng multithreading trong Unity C# cần cẩn thận vì Unity có một số hạn chế về việc sử dụng multithreading. Các nhiệm vụ nặng nhọc nhất, như tải dữ liệu từ mạng, nên được chạy trong một luồng bất đồng bộ. Bạn có thể sử dụng các lớp System.Threading để tạo ra một luồng mới và chạy nhiệm vụ tải dữ liệu trong đó. Tuy nhiên, bạn cần phải chú ý rằng, nếu bạn muốn truy cập các đối tượng Unity từ luồng bất đồng bộ này, bạn phải sử dụng các hàm UnitySendMessage hoặc Invoke.
Ví dụ, để tải dữ liệu từ mạng trong một luồng bất đồng bộ, bạn có thể làm như sau:
using System.Threading;
using UnityEngine;

public class Example : MonoBehaviour
{
private Thread downloadThread;

private void Start()
{
downloadThread = new Thread(DownloadData);
downloadThread.Start();
}

private void DownloadData()
{
// Tải dữ liệu từ mạng và lưu lại
// ...

// Gửi tin nhắn đến các đối tượng Unity để cập nhật giao diện
UnityThread.ExecuteInUpdate(() =>
{
// Cập nhật giao diện
// ...
});
}
}
Việc sử dụng multithreading cần cẩn thận, bởi vì nó có thể dẫn đến việc tác vụ không đồng bộ hoặc xảy ra lỗi trong quá trình chạy.
Một cách để tránh những vấn đề này là sử dụng các phương pháp đồng bộ hóa, chẳng hạn như sử dụng lock hoặc semaphore để đảm bảo rằng chỉ có một luồng được thực thi cùng một lúc trong một đoạn mã cụ thể.
Còn một cách khác là sử dụng Unity's Job System, một công cụ để tạo và quản lý các tác vụ multithreading trong Unity. Nó cho phép bạn tạo các tác vụ và giao việc cho Unity để thực hiện, đảm bảo rằng việc multithreading sẽ được thực hiện một cách an toàn và hiệu quả.
Tuy nhiên, multithreading vẫn phải được sử dụng cẩn thận và chỉ nên sử dụng khi cần thiết, vì việc sử dụng nhiều luồng có thể gây tăng thời gian xử lý và tăng chi phí bộ nhớ.

Task là một công cụ quan trọng trong việc tạo ra các tiến trình song song trong C#. Nó cho phép bạn tạo ra các tiến trình độc lập và chạy chúng song song với tiến trình chính. Điều này có thể giúp tăng hiệu suất cho các tác vụ tải dữ liệu, xử lý dữ liệu và tác vụ tương tự.
Ví dụ:
using System.Threading.Tasks;

public class DataProcessor
{
public void ProcessData()
{
Task.Factory.StartNew(() => LoadData());
}

private void LoadData()
{
// Code to load data from a file or database.
}
}
Trong ví dụ trên, chúng ta tạo ra một phương thức ProcessData và gọi Task.Factory.StartNew để tạo ra một tiến trình mới và gọi phương thức LoadData trong tiến trình mới đó.
Lưu ý: Sử dụng multithreading có thể gây rối tắc hoặc gây ra những vấn đề về tương tác giữa các tiến trình, do đó, bạn cần phải cẩn thận khi sử dụng nó và kiểm soát việc truy cập dữ liệu chung giữa các tiến trình.
Unity's Job System là một tính năng mới trong Unity 2018.3, cho phép bạn tạo và chạy các tác vụ trên nhiều luồng dữ liệu để tối ưu hóa hiệu năng của ứng dụng của bạn.
Để sử dụng Unity's Job System, bạn cần thực hiện các bước sau:
1. Tạo một struct của công việc với thuộc tính Execute, định nghĩa hành động của công việc.
2. Sử dụng Schedule để chạy công việc và chờ đợi kết quả.
3. Sử dụng Complete để kiểm tra xem công việc có hoàn thành hay không.
Ví dụ:
using Unity.Jobs;
using Unity.Collections;

public struct MyJob : IJob
{
public float a;
public float b;
public NativeArray<float> result;

public void Execute()
{
result[0] = a + b;
}
}

public class ExampleClass : MonoBehaviour
{
void Start()
{
// Tạo một job với hai giá trị `a` và `b` và một mảng kết quả.
MyJob jobData = new MyJob
{
a = 1,
b = 2,
result = new NativeArray<float>(1, Allocator.TempJob)
};

// Schedule job.
JobHandle handle = jobData.Schedule();

// Chờ đợi job hoàn thành.
handle.Complete();

// Lấy kết quả từ job.
float result = jobData.result[0];
jobData.result.Dispose();

Debug.Log(result);
}
}
Unity's Job System là một tính năng mới được giới thiệu trong Unity 2018.3 và cho phép bạn chia tác vụ để thực hiện trên nhiều luồng đồng thời. Nó cung cấp một cách dễ dàng để sử dụng các luồng của CPU để tăng tốc độ xử lý các tác vụ nặng.
Để sử dụng Unity's Job System, bạn cần tạo một lớp Job để chứa tác vụ cần thực hiện và sử dụng JobHandle để thực hiện Job.
Ví dụ:
using Unity.Jobs;
using Unity.Collections;

public struct MyJob : IJob
{
public NativeArray<float> result;
public void Execute()
{
for (int i = 0; i < result.Length; i++)
{
result[i] = i * 2;
}
}
}

public class JobSystemExample : MonoBehaviour
{
void Start()
{
NativeArray<float> resultArray = new NativeArray<float>(100, Allocator.TempJob);
MyJob myJob = new MyJob();
myJob.result = resultArray;

JobHandle handle = myJob.Schedule();
handle.Complete();

// Use the result
for (int i = 0; i < resultArray.Length; i++)
{
Debug.Log(resultArray[i]);
}

resultArray.Dispose();
}
}
Lưu ý rằng, việc sử dụng Unity's Job System cần phải chú ý về việc quản lý bộ nhớ và việc sử dụng các dữ liệu chung giữa các luồng, nên chỉ nên sử dụng khi bạn đã có kiến thức vững về multithreading.

CÁCH 6: Sử dụng component thay vì GameObject
Sử dụng component thay vì GameObject là một trong những cách tối ưu hoá hiệu suất trong Unity.
Trong Unity, mỗi GameObject là một đối tượng đại diện cho một đối tượng trong trò chơi của bạn. Những đối tượng này có thể có nhiều component, chẳng hạn như Transform, Collider, Renderer, vv.
Khi bạn tạo một GameObject, nó sẽ tạo ra tất cả các component cần thiết để tạo nên đối tượng trong trò chơi. Tuy nhiên, mỗi GameObject cũng sẽ có một số chi phí về tài nguyên để quản lý, chẳng hạn như bộ nhớ và CPU.
Vì vậy, nếu bạn cần một số component rất nhiều trong trò chơi của mình, sử dụng component thay vì GameObject có thể giúp giảm chi phí tài nguyên và tăng hiệu suất. Bạn có thể tạo một số component chung cho nhiều đối tượng trong trò chơi, và sau đó sử dụng chúng nhiều lần cho nhiều đối tượng khác nhau.
Ví dụ: Nếu bạn muốn tạo một đối tượng với tính năng di chuyển, bạn có thể tạo một Component chứa các hàm Update để di chuyển đối tượng. Sau đó, bạn có thể thêm Component này vào một GameObject hoặc một đối tượng khác, giúp tăng tính modular và giảm kích thước của đối tượng. Trong Unity, việc sử dụng Component thay vì GameObject có những lợi ích như:
1. Giảm số lượng GameObject: Khi bạn sử dụng Component, bạn có thể gắn nhiều chức năng vào một đối tượng duy nhất mà không cần tạo ra nhiều GameObject khác. Điều này giúp giảm số lượng đối tượng trong cảnh và giảm tải cho bộ nhớ.
2. Dễ dàng quản lý: Khi bạn sử dụng Component, bạn có thể dễ dàng quản lý và sửa chữa tất cả các chức năng liên quan đến đối tượng đó trong một nơi duy nhất.
Ví dụ:
• Bạn có một đối tượng Player trong game của mình. Bạn muốn thêm các chức năng như di chuyển, tấn công và nhận sát thương.
• Nếu bạn sử dụng GameObject, bạn có thể tạo ra 3 đối tượng riêng biệt cho di chuyển, tấn công và nhận sát thương.
• Nếu bạn sử dụng Component, bạn có thể tạo ra 3 Component cho di chuyển, tấn công và nhận sát thương và gắn chúng vào đối tượng Player. Điều này sẽ giúp bạn quản lý và sửa chữa tất cả.

Ví dụ nữa, khi bạn muốn tạo ra một đối tượng Player trong game, thì bạn có thể sử dụng một Component "PlayerController" để lưu trữ tất cả thông tin về trạng thái, hành vi và tương tác của đối tượng Player. Điều này giúp bạn tách biệt rõ ràng các thành phần của đối tượng và cho phép bạn dễ dàng tìm và sửa chữa lỗi nếu cần thiết.
Bạn cũng có thể sử dụng các Component khác như Animation, Physics, Audio, v.v. để tạo ra các đối tượng trong game với các tính năng riêng biệt và sử dụng chúng trong một GameObject.
Tất cả các Component đều có thể được tùy biến và mở rộng để phù hợp với nhu cầu của bạn, giúp cho việc phát triển game trở nên dễ dàng hơn và hiệu quả hơn.
Ví dụ:
public class PlayerController : MonoBehaviour
{
private Rigidbody2D rigidbody2D;
private Animator animator;
private Collider2D collider2D;

private void Awake()
{
rigidbody2D = GetComponent<Rigidbody2D>();
animator = GetComponent<Animator>();
collider2D = GetComponent<Collider2D>();
}

private void Update()
{
float horizontal = Input.GetAxis("Horizontal");
float vertical = Input.GetAxis("Vertical");

rigidbody2D.velocity = new Vector2(horizontal * 5, vertical * 5);
animator.SetFloat("Horizontal", horizontal);
animator.SetFloat("Vertical", vertical);

if (horizontal != 0 || vertical != 0)
{
collider2D.enabled = false;
}
else
{
collider2D.enabled = true;
}
}
}

Trong đoạn code trên, ta đã khai báo các thành phần Rigidbody2D, Animator, và Collider2D là các thuộc tính của lớp PlayerController. Ta sử dụng hàm Awake() để lấy các component này từ game object tương ứng bằng cách sử dụng hàm GetComponent<T>(). Sau đó, ta sử dụng các component này trong hàm Update() để cập nhật trạng thái của game object mỗi khi nó thực hiện cập nhật.
Bằng cách sử dụng component thay vì GameObject, ta có thể giảm bớt số lần tìm kiếm và tăng tốc độ của chương trình.

CÁCH 7: Sử dụng một lớp quản lý cho các component chung
Bạn có thể sử dụng một lớp quản lý để quản lý các component chung như nhất định, tạo một tập hợp các component và quản lý các phương thức hoặc giá trị liên quan đến chúng. Ví dụ, nếu bạn có một số component đồ họa cần được quản lý, bạn có thể tạo một lớp tên là "GraphicsManager" và thêm các component đồ họa vào trong đó.
public class GraphicsManager : MonoBehaviour
{
public List<MeshRenderer> MeshRenderers;
void Start()
{
MeshRenderers = GetComponentsInChildren<MeshRenderer>().ToList();
}
public void EnableRenderers()
{
foreach (MeshRenderer renderer in MeshRenderers)
{
renderer.enabled = true;
}
}
public void DisableRenderers()
{
foreach (MeshRenderer renderer in MeshRenderers)
{
renderer.enabled = false;
}
}
}


Bạn có thể gọi các phương thức này từ bất kỳ nơi nào trong game của bạn, ví dụ:
GraphicsManager graphicsManager = FindObjectOfType<GraphicsManager>();
graphicsManager.EnableRenderers();
Bạn có thể sử dụng một lớp quản lý để quản lý các component của tất cả các đối tượng trong game của bạn. Điều này giúp cho việc quản lý các component trở nên dễ dàng hơn và có thể giảm bớt số lượng mã trùng lặp trong game.
Ví dụ, nếu bạn muốn quản lý việc di chuyển của tất cả các đối tượng trong game, bạn có thể tạo một lớp "MovementManager" với các thuộc tính và phương thức để quản lý việc di chuyển của tất cả các đối tượng. Rồi bạn có thể gắn component "MovementManager" vào đối tượng gốc của game và sử dụng nó để quản lý việc di chuyển của tất cả các đối tượng trong game.
public class MovementManager : MonoBehaviour
{
// Quản lý việc di chuyển của tất cả các đối tượng trong game
public void MoveObjects()
{
// Các xử lý di chuyển
}
}
Bạn có thể sử dụng cách tiếp cận này để quản lý tất cả các component khác nhau trong game, chẳng hạn như việc quản lý tính toán, âm thanh, hiển thị, v.v.

Sử dụng một lớp quản lý cho các component chung có thể giúp bạn tối ưu hoá và dễ quản lý hơn cho code của mình.
Để minh họa cách sử dụng một lớp quản lý, chúng ta có thể tạo một lớp tên là "GameManager" và chứa các thuộc tính và hàm hoạt động chung cho một số component trong game.
Ví dụ, nếu bạn có một số component cần truy cập vào một biến thời gian, bạn có thể định nghĩa một thuộc tính "currentTime" trong lớp GameManager và truy cập nó từ các component khác.
public class GameManager : MonoBehaviour
{
public static GameManager instance;
public float currentTime;

private void Awake()
{
instance = this;
}
}
Sau đó, bạn có thể truy cập thuộc tính "currentTime" từ bất kỳ component nào bằng cách sử dụng GameManager.instance.currentTime.
Việc sử dụng một lớp quản lý cho các component chung có thể giúp bạn tối ưu hoá và dễ quản lý hơn code của mình, và cũng giúp bạn giảm số lượng component trong một GameObject, giúp giảm độ phức tạp và tăng tốc độ của game.

CÁCH 8: Sử dụng lazy evaluation
Lazy evaluation là một kỹ thuật trong lập trình, cho phép bạn tạm thời tránh việc tính toán hoặc tải dữ liệu cho một giá trị cho đến khi nó cần thiết. Trong Unity C#, bạn có thể sử dụng lazy evaluation để tối ưu hóa hiệu suất cho các đối tượng hoặc tác vụ mà bạn chỉ cần khi cần thiết.
Ví dụ: Bạn muốn tải một tài nguyên lớn (ví dụ như một hình ảnh hoặc một tập tin âm thanh) cho một đối tượng trong trò chơi của bạn, nhưng bạn muốn tải nó chỉ khi đối tượng đó được hiển thị trên màn hình. Bạn có thể sử dụng lazy evaluation để tải tài nguyên đó chỉ khi nó cần thiết, và tránh tải nó sớm hơn.
Để sử dụng lazy evaluation trong Unity C#, bạn có thể tạo một biến kiểu Lazy<T> với kiểu dữ liệu T là kiểu dữ liệu của tài nguyên bạn muốn tải. Bạn có thể xác định một hàm delegate hoặc lambda expression để tạo ra giá trị cho tài nguyên đó khi nó được truy cập lần đầu.
Ví dụ:
Lazy<Texture2D> myTexture = new Lazy
Lazy evaluation là một kỹ thuật xử lý tạm thời và chỉ tính toán khi cần thiết. Nó giúp tối ưu hóa tốc độ hoạt động của chương trình bằng cách chỉ tạo ra các đối tượng hoặc tính toán khi cần thiết thay vì tạo ra hoặc tính toán trước.
Ví dụ, nếu bạn cần tính toán một giá trị rất phức tạp cho một đối tượng, bạn có thể sử dụng lazy evaluation để chỉ tính toán giá trị đó khi nó được yêu cầu hoặc khi nó đầu tiên được sử dụng.
Để sử dụng lazy evaluation trong Unity, bạn có thể sử dụng một trong những cách sau:
1. Sử dụng một delegate hoặc lambda expression để chỉ tính toán giá trị khi nó được yêu cầu.
2. Sử dụng một thuộc tính tự động để chỉ tính toán giá trị khi nó đầu tiên được truy cập.
3. Sử dụng một lớp wrapper để chỉ tính toán giá trị khi nó đầu tiên được truy cập.
Lazy evaluation là một từ khóa liên quan đến việc tạm hoãn tính toán hoặc tính năng cho đến khi nó được yêu cầu hoặc cần thiết. Điều này có thể giúp tối ưu hóa hiệu suất và tạo ra một cách tương tác cải tiến với người dùng.

Trong Unity, bạn có thể sử dụng lazy evaluation cho các trường hợp như:

Tải dữ liệu từ mạng: Chỉ tải dữ liệu khi nó được yêu cầu hoặc cần thiết, chứ không phải tải tất cả một lần.
Tính toán dữ liệu: Chỉ tính toán dữ liệu khi nó được yêu cầu hoặc cần thiết, chứ không phải tính toán tất cả ngay tức thì.
Ví dụ:
public class LazyEvaluationExample : MonoBehaviour
{
private int _value;
private bool _isValueSet;

public int Value
{
get
{
if (!_isValueSet)
{
// Do some heavy calculation here
_value = CalculateValue();
_isValueSet = true;
}

return _value;
}
}

private int CalculateValue()
{
// Perform some complex calculation
return 10;
}
}
Trong ví dụ trên, chúng ta sử dụng một biến _isValueSet để kiểm tra xem giá trị _value đã được tính toán hay chưa. Nếu chưa, chúng ta sẽ tính toán nó bằng hàm CalculateValue và gán giá trị cho biến _isValueSet