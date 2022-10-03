# Layer, Mask, CullingMask : 

- TÌm hiểu về LayerMask, tính toán Bit Mask :  https://thoxaylamcoder.wordpress.com/2016/05/15/unity3d-layermask-y-nghia-va-cach-su-dung/
- Unity sử dụng một biến integer để lưu trữ layer cho từng object. Biến int này sẽ được dùng như một BitMask. Biến integer, kích thước 4 byte, mỗi byte có 8 bit , tổng cộng 32 bit
- Object của mình ở layer Default, thì LayerMask của nó sẽ là:
00000000  00000000 00000000 00000001 , bit “1” ở vị trí đầu tiên
- Con Player của mình nằm ở layer Character, layerMask sẽ là :
00000000  00000000 00000001 00000000, bit “1” ở vị trí thứ 8
- Cái MainCamera nó Render các object trong các layers : Default, Character, Enemy -> thì cái CullingMask của camera sẽ là :
00000000  00000000 00000011 00000001
- Còn tất nhiên nếu ko config gì hết (render Everything) thì CullingMask sẽ là (-1) :
11111111 11111111 11111111 11111111

- VD trong TH kiểm tram Raycast có trúng mục tiêu là layer Enemy hay ko : 
``` RaycastHit2D hit = Physics2D.Linecast(lineCastFrom, lineCastTo, 1 << LayerMask.NameToLayer("Enemy")); ``` 
Ở đây, hàm LayerMask.NameToLayer("Enemy") sẽ trả về giá trị integer trong khoản [0..31], nhưng cái ta cần truyền vào là bitmask. Vậy nên ta cần chuyển gtri integer đó thành bitmask tương ứng. => 1 << 8 sẽ thành 1 0000 0000
=> 1 0000 0000 là giá trị bitmask của Layer Enemy.  => 2^8 = 768 => 768 là giá trị của layer. 
- Chốt : ta có 3 giá trị ở Layer để lưu ý là : Gía trị Layer Maks kiểu integer [0.31], Giá trị bit mask : 0000 0000... (bao gồm 32 bit) và giá trị Layer là giá trị của layer tính ra từ bitmask (2^x)