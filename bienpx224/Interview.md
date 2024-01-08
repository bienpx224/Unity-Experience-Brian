# About Interview 

## Unity Interview Base , Normal question : 
- Chơi game g




## Sotatek Test Algorithm : 

Test Algorithm Sotatek

(Bài test bao gồm 4 câu. Thời gian làm bài: 45 phút)
Họ tên:        
1. Giai thừa của 1 số nguyên dương n là tích của tất cả các số nguyên dương nhỏ hơn hoặc bằng n. Công thức như sau: 
￼
Viết mã giả mô tả thuật toán tính giai thừa của một số n cho trước.

```java
public static long factorialCalculation(int n){
	long rs = 1;
if(n == 0 || n == 1){
		return rs;
	}else{
		for( int i = 2; i<= n; i++){
			rs *= i;
		}
return	rs;
}
}
```

2.  Cho định nghĩa sau: số hoàn hảo là số có giá trị bằng tổng các ước nhỏ hơn nó.
Ví dụ: 6=1+2+3; 28=1+2+4+7+14.
Viết mã giả mô tả thuật toán kiểm tra 1 số n cho trước có phải là số hoàn hảo hay không.

* Sử dụng vòng lặp for để tính tổng các ước số của số cho trước. Thực hiện từ 1 đến n/2 vì các ước số của n là nhỏ hơn hoặc bằng n/2. 

```java
function boolean perfectNumber(int n){
	int sum = 0;
	for(int i = 0; i<= n/2; i++){
		if( n%i == 0){
			sum+= i;  // Tính tổng các ước số 
		}
}
// Kiểm tra nếu tổng các ước số sum = n thì n là số hoàn hảo 
if(sum == n){
	return true;
}else{
	return false;
		}
}
```


3. Với chuỗi cho trước có độ dài lớn hơn 1, viết mã giả thể hiện thuật toán xác định vị trí của 1 kí tự trong chuỗi sao cho khi xoá kí tự đó đi thì chuỗi còn lại là nhỏ nhất có thể.
Ví dụ: chuỗi `231` sau khi xoá đi 1 kí tự có thể trở thành `31`, `21`, `23`. Chuỗi nhỏ nhất là `21`, đáp án đúng là xoá đi kí tự `3` trong chuỗi ban đầu.
	
* Tạo một int min = 0; // để lưu lại chuỗi nhỏ nhất 
* Tạo int minIndex = 0; // để lưu lại vị trí kí tự mà xóa đi sẽ được chuỗi nhỏ nhất  
* Duyệt từng phần tử ở chuỗi đã cho: for(int i=0 ; i<= chuoi.length()-1; i++) 
* Tạo 1 chuỗi mới bằng chuỗi cũ nhưng remove vị trí thứ i : String tempStr = chuoi.removeAt(i) 
* Chuyển chuỗi mới đó thành int rồi so sánh với min, nếu chuỗi mới đó nhỏ hơn min thì gắn min = chuỗi đó và gắn minIndex = vị trí i . 
* Cứ như vậy đến hết vòng lặp, ta sẽ được vị trí của kí tự mà khi xóa đi chuỗi có giá trị nhỏ nhất đó là minIndex. 
	

4. Bob cầm n đồng đi mua kẹo. Giá của 1 viên kẹo là c đồng. Cửa hàng lại có chương trình khuyến mại cứ m tờ giấy gói kẹo thì đổi được 1 viên kẹo mới. Viết mã giả thể hiện thuật toán xác định tổng số kẹo Bob có thể mua được từ các tham số n, c, m như đã mô tả ở trên.
Ví dụ:
* n=10, c=2, m=5: Bob mua được 5 viên kẹo, sau đó đổi 5 tờ giấy gói lấy 1 viên kẹo nữa, tổng cộng đáp án là 6. 
* n=12, c=4, m=4: Bob mua được 3 viên kẹo, số giấy gói kẹo không đủ đổi thêm nữa, đáp án cuối cùng là 3. 
* n=6, c=2, m=2: Bob mua được 3 viên kẹo, lấy 2 trong 3 tờ giấy gói kẹo đổi được thêm 1 viên. Sau đó dùng tiếp 1 tờ giấy gói dư ở lần đổi thứ nhất + tờ giấy gói của viên kẹo ở lần đổi thứ 2, đổi được thêm 1 viên kẹo nữa. Đáp án tổng cộng là 5. 
```java
// TH chỉ được đổi khuyến mại 1 lần. 
function int calculatorCandyCanBuy(int n, int c, int m){
	int amoutCandy = 0;
	if(n>0 && n >= c){
		amoutCandy += n/c;  // Số viên kẹo mua được bằng n đồng 
	}
	if(amountCandy > 0 && amountCandy >= m){
int surplus = amountCandy % m;      // Số vỏ kẹo còn dư sau khi đổi được vỏ kẹo
int promotionCandy = 0;
		promotionCandy += amountCandy / m; // Số kẹo sẽ đổi được bằng vỏ kẹo với số kẹo đã mua ở trên.
		if( surplus + promotionCandy >= m ){
			promotionCandy += (surplus + promotionCandy) / m // Số kẹo đổi ở lần thứ  2 từ vỏ kẹo còn dư và vỏ kẹo được nhận từ khuyến mại.
		}
		amountCandy += promotionCandy;
}
return amountCandy;
}
// Trường hợp đệ quy cứ được khuyến mại nhiều kẹo và lại thêm vỏ, lại khuyến mại, ta tính phần thưởng kẹo khuyến mại thêm trong hàm đệ quy, đặc biệt TH tham số m = 1 thì sẽ lặp mãi mãi.
function int promotionCandyRecursive(int amountCandy, int m){
	if(amountCandy >= m){
	int surplus = amountCandy % m ;  // Số vỏ kẹo dư 
	int promotionCandy = 0;  // Số kẹo sẽ được nhận thêm
promotionCandy += promotionCandyRecursive( (amountCandy + surplus) / m , m );
	return promotionCandy;
	}else{
		return 0;
	}
}

function int calculatorCandyCanBuyRecursive(int n, int c, int m){
	int amoutCandy = 0;
	if(n>0 && n >= c){
		amoutCandy += n/c;  // Số viên kẹo mua được bằng n đồng 

		amountCandy += promotionCandyRecursive(amoutCandy, m);
		return amountCandy;
	}else{
		return 0;
}
}
```

	

		

	
