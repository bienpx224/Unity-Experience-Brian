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

## 
- Sử dụng các kiểu uint6 ,8 16, 32 trước để tiết kiệm bộ nho trong stack.
- Cached lại các biển toàn cục kiểu dic, array để sử dụng trong function đó, ví dụ để for các giá trị trong dic đó. 