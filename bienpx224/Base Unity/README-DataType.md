# Tổng hợp kiến thức cơ bản nhưng ít người để ý về các kiểu dữ liệu, bộ nhớ : 

- So sánh các kiểu dữ liệu trong java : https://viettuts.vn/interview/list-cau-hoi-phong-van-java-collection 
- PV Java : https://viettuts.vn/interview/list-cau-hoi-phong-van-java-core#goto-h3-2 

# Static : 

- Biến static có thể được sử dụng để tham chiếu thuộc tính chung của tất cả đối tượng (mà không là duy nhất cho mỗi đối tượng), ví dụ như tên công ty của nhân viên, tên trường học của các sinh viên, …
- Biến static lấy bộ nhớ chỉ một lần trong Class Area tại thời gian tải lớp đó.
- Stack là một vùng nhớ được sử dụng để lưu trữ các tham số và các biến local của phương thức mỗi khi một phương thức được gọi ra.

- Heap là một vùng nhớ trong bộ nhớ được sử dụng để lưu trữ các đối tượng khi từ khóa new được gọi ra, các biến static và các biến toàn cục (biến instance).

- Một phương thức static thuộc lớp chứ không phải đối tượng của lớp.
- Một phương thức static gọi mà không cần tạo một instance của một lớp.
- Phương thức static có thể truy cập biến static và có thể thay đổi giá trị của nó.

 * Có hai hạn chế chính đối với phương thức static. Đó là:

- Phương thức static không thể sử dụng biến non-static hoặc gọi trực tiếp phương thức non-static.
- Từ khóa this và super không thể được sử dụng trong ngữ cảnh static.

* So sánh Phương thức static và	Phương thức instance : 
1)Một phương thức được khai báo với từ khóa static được gọi là phương thức static.	Một phương thức không được khai báo với từ khóa static được gọi là phương thức instance.
2)Không cần tạo đối tượng cũng gọi được phương thức static thông qua class.	Phải tạo đối tượng để gọi phương thức instance.
3)Biến non-static không được truy cập trực tiếp trong phương thức static (hoặc khối static).	Biến static và non-static được truy cập trực tiếp trong phương thức instance.
4)Ví dụ: public static int cube(int n){ return n*n*n;}	Ví dụ: public void msg(){...}.

- Tham khảo : https://viettuts.vn/java/tu-khoa-static-trong-java  

# Các Kiểu dữ liệu trong lập trình : 
- Sử dụng các kiểu uint6 ,8 16, 32 trước để tiết kiệm bộ nho trong stack.
- Cached lại các biển toàn cục kiểu dic, array để sử dụng trong function đó, ví dụ để for các giá trị trong dic đó. 

- Một số kiểu dữ liệu thuộc kiểu giá trị: bool, byte, char, decimal, double, enum, float, int, long, sbyte, short, struct, uint, ulong, ushort.
- Một số kiểu dữ liệu thuộc kiểu tham chiếu: object, dynamic, string và tất cả các kiểu dữ liệu do người dùng định nghĩa.

* Bộ nhớ Stack và bộ nhớ Heap: Cả hai đều là bộ nhớ trên RAM, là vùng nhớ được ra khi chương trình được thực thi nhưng cách tổ chức, quản lý dữ liệu cũng như sử dụng thì rất khác nhau:
- Stack:
+ Vùng nhớ được cấp phát khi chương trình biên dịch.
+ Được sử dụng cho việc thực thi thread (khái niệm về thread sẽ được trình bày trong bài THREAD TRONG C# ), khi gọi hàm (khái niệm về hàm sẽ được trình bày trong bài CẤU TRÚC HÀM CƠ BẢN TRONG C#), các biến cục bộ kiểu giá trị và tự động giải phóng khi không còn sử dụng nữa.
+ Kích thước vùng nhớ Stack là cố định và chúng ta không thể thay đổi.
+ Khi vùng nhớ này không còn đủ dùng thì sẽ gây ra hiện tượng tràn bộ nhớ (stack overflow). Hiện tượng này xảy ra khi nhiều hàm lồng vào nhau hoặc gọi đệ quy nhiều lần dẫn đến không đủ vùng nhớ (khái niệm đệ quy sẽ được trình bày trong bài CẤU TRÚC HÀM CƠ BẢN TRONG C#).
- Heap:
+ Vùng nhớ được cấp phát khi chạy chương trình.
+ Vùng nhớ Heap được dùng cho cấp phát bộ nhớ động (cấp phát thông qua toán tử new, sẽ được trình bày trong bài TOÁN TỬ TRONG C#)
+ Bình thường vùng nhớ trong Heap do người dùng tự giải phóng nhưng trong C# điều này được hỗ trợ mạnh mẽ bởi bộ tự động thu gom rác (Garbage Collection). Vì thế việc giải phóng vùng nhớ sẽ được thực hiện tự động.
+ Kích thước vùng nhớ Heap có thể thay đổi được. Khi không đủ vùng nhớ để cấp phát thì hệ điều hành sẽ tự động tăng kích thước vùng nhớ Heap lên.

- So sánh Stack và Heap : https://viblo.asia/p/su-khac-nhau-giua-bo-nho-heap-va-bo-nho-stack-trong-lap-trinh-E375zQb1lGW 
