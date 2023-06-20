# Tổng quan 1 chút về OOP : 

## Khác biệt giữa Virtual và Abstract trong Abstract Class trong OOP : 
Giống nhau : 
- Từ khóa Virtual hoặc Abstract (bên lớp cơ sở) và Override (trên lớp dẫn xuất) hỗ trợ tạo đa hình (Polymorphism) cho các phương thức (Method) của object (1 trong những điểm mạnh của OOP).

Sự khác nhau:
- Điểm đầu tiên là Virtual cho phép lớp Con không nhất thiết phải tạo Override cho method Virtual ở lớp Cha. Ngược lại Abstract thì bắt buộc.
- Thứ 2 là cách khai báo, method Virtual có các hàm bên trong còn Abstract thì tuyệt tối không.
- Thứ 3 là method abstract phải nằm trong class abstract như bên dưới.

``` c# 
   Abstract Class Cha 
   {
   public virtual void AAA() { }
   public abstract void BBB() ;
   }

   Class Con : Cha
   {
   public override void AAA() { } // có thể không cần hàm này 
   public override void BBB() { } // buộc có.
   }
```

## Sử dụng virtual và override để ghi đè hoặc sử dụng phương thức cha: 
- Class cha khai báo virtual func. 
- Class con khai báo override func đó, và trong func có thể sử dụng base.func cha để gọi thực thi hàm cha.
## Khác biệt giữa Class và Struct : 
- Khi tạo Struct, sẽ mặc định sinh ra một hàm khởi tạo với đầy đủ tham số tương ứng với tất cả Stored Properties (gọi là Memberwise Initializer). Còn Class thì không có. Do đó khi tạo một class, ta sẽ phải khai báo Optional cho các thuộc tính hoặc phải tự định nghĩa một hàm khởi tạo.
- Class có hàm Denit (khi instance chuẩn bị được giải phóng khỏi bộ nhớ). Chúng ta có thể gọi hàm này để kiểm tra khi cần thiết (đảm bảo instance đã bị huỷ để kiểm tra Retain Cycle)
- Struct là Value types còn Class là Reference types 
- Class là kiểu Reference Type, do đó có thể thay đổi trực tiếp giá trị của các thuộc tính qua phương thức của nó. Còn với Struct ta phải khai báo từ khoá mutating trước phương thức để làm việc đó.
- Ngoài ra, Class hỗ trợ Type Casting, cho phép sử dụng toán tử is và as để kiểm tra hoặc ép kiểu thể hiện của một Class. Struct thì không.
- Class có thể kế thừa, còn struct thì không 
- Class là kiểu tham chiếu, do đó hỗ trợ các toán tử === và !== để kiểm tra các đối tượng có đang trỏ tới cùng một instance hay không.
- Khi nào nên sử dụng struct / class
Recommend sử dụng struct bởi:
Struct nhanh hơn class bởi struct sử dụng method dispatch là static dispatch, class sử dụng dynamic dispatch. Ngoài ra, struct lưu dữ liệu trong stack, còn class sử dụng stack + heap -> Xử lí trong class sẽ lâu hơn.
- Class là 1 reference type. Do đó, nếu không cẩn thận khi truyền biến sẽ dễ gây ra lỗi ngoài ý muốn ( Xem phần value type vs reference type ở trên). -> Sử dụng struct sẽ an toàn hơn.
- Việc copy các instance là không hợp lý hoặc không cần thiết. Vậy nên Class sẽ là tuyệt vời để tham chiếu tới các đối tượng như DatabaseConnection,.. bởi vì ta chỉ cần 1 kết nối và dùng chung nó chứ ko cần phải coppy tạo ra nhiều instance ko cần thiết. 

## Cơ bản về OOP : 
Gồm 4 nguyên lý cơ bản là : 
### Tính đóng gói - Encapsulation: 
- Các dữ liệu và phương thức có liên quan với nhau được đóng gói thành các lớp để tiện cho việc quản lý và sử dụng. Tức là mỗi lớp được xây dựng để thực hiện một nhóm chức năng đặc trưng của riêng lớp đó.
- Ngoài ra, đóng gói còn để che giấu một số thông tin và chi tiết cài đặt nội bộ để bên ngoài không thể nhìn thấy. (private - protected - public) Đại loại dễ hiểu là : 
    + Private : Chỉ gọi khi cùng ở trong class. 
    + Protected : Chỉ gọi khi ở trong cùng folder, package . 
    + Public : Gọi được khắp mọi nơi. 
- Trong lập trình OOP ta thường để các thuộc tính là private, và sử dụng func get set để xác định xem có thể lấy các thuộc tính đó bằng public hay private. 

### Tính Kế thừa - Inheritance : 
- Cho phép xây dựng 1 lớp mới (Lớp con) dựa trên định nghĩa của lớp đã có (Lớp cha). Lớp cha có thể chia sẻ dữ liệu và func cho lớp con. CÒn lớp con thì ngoài việc có những thuộc tính, func lớp cha thì lớp con còn có thể định nghĩa lại (override), mở rộng các thành phần, bổ sung thành phần mới.

### Tính đa hình - Polymorphism : 
- Là một đối tượng thuộc các lớp khác nhau có thể hiểu cùng 1 thông điệp theo các cách khác nhau. VD 2 đối tượng Chó và Mèo nhận được func "Kêu lên" thì chó kêu "Gâu gâu" còn mèo kêu "Meo meo". Tính đa hình gồm 2 cách thức chính là : 
* Method Overloading : Cùng 1 Function nhưng với các tham số khác nhau hoặc kiểu tham số truyền vào khác nhau, chúng ta có được những function khác nhau. Method Overloading : là cách nạp chồng các method có cùng tên nhưng khác tham số. 
- VD : function SetAccount(int phone) và SetAccount(string email) : Cùng tên 1 function nhưng nếu param truyền vào là số thì sẽ xác định là phone, còn nếu là string thì là email.

* Method Overriding:  Đây là một phương pháp được ghi đè lại các method ảo của một lớp cha nào đó(được khai báo bằng từ khóa virtual).
- Để thể hiện phương pháp này cần dùng 2 từ khóa:
+ virtual :từ khoá dùng để khai báo 1 phương thức ảo (có thể ghi đè được).
+ override: từ khoá dùng để đánh dấu phương thức ghi đè lên phương thức của lớp cha.
- Phương thức con kế thừa phương thức cha. Nếu muốn định nghĩa lại hoặc phát triển từ func của cha, ta sử dụng override. Nếu muốn vẫn sử dụng cả phương thức cha thì sử dụng base.{name_function} để thực hiện function của cha. 


### Tính trừu tượng - Abstraction : 
- Là định nghĩa các tính chất hành vi 1 cách trừu tượng. 
- khi khai báo một lớp có từ khóa abstract thì nó là lớp trừu tượng. Đã là lớp trừu tượng thì nó không được dùng để khởi tạo đối tượng trực tiếp mà nó chỉ làm lớp cơ sở kế thừa bởi lớp khác.
- Trong lớp trừu tượng, còn có thể khai báo phương thức trừu tượng với từ khóa abstract, phương thức này không có thân (chỉ có tên - tham số), nó yêu cầu lớp kế thừa bắt buộc phải nạp chồng (overrid)

* Abstract : 
- Abstract class : trong abstract class có 2 loại method:
 + abstract method (là method rỗng không thực hiện gì) protected abstract void Work();
 + method thường (là vẫn có logic trả về data hoặc thực thi hành động nào đó, nó được sử dụng cho mục đích dùng chung)
- Các lớp chỉ có thể kế thừa một Abstract class.
- Hướng đến tính năng và những tính năng có thực thi được sử dụng làm hàm chung cho các class extend.

* Interface : 
 - Khá giống với abstract class nhưng interface không phải là class, trong interface chỉ  có khai báo những method/properties trống không có thực thi, thực thi sẽ được thể hiện trong các lớp kế thừa, interface giống như một cái khung mẫu để các lớp implement và follow.
 - Là một contract, các class implement phải triển khai các method theo như interface đã định nghĩa.
 - Các lớp có thể implements nhiều interface.

* So sánh Interface với Abstract :  
- Ưu điểm:
 - Interface
    + Có thể kế thừa nhiều interface(tính đa hình).
    + Xây dựng được bộ khung mẫu mà các lớp phải follow theo.
    + Giúp quản lý tốt, nắm bắt được các chức năng phải có cho một đối tượng nào đó.
 - Abstract class
    + Có thể linh động các method. giống như một class thông thường.
    + Các class extend có thể override hoặc không override các method thường.
- Nhược điểm:
 - Interface:
    + Mỗi khi định nghĩa thêm tính năng, các class impelement nó đồng lọat phải thêm tính năng đó, khả năng cao sẽ không có xử lý gì.
 - Abstract class
    + Không thể extend nhiều abstract class.

* Link tham khảo VD : https://viblo.asia/p/khac-nhau-giua-abstract-class-va-interface-khi-nao-dung-chung-ORNZq9YrZ0n 
