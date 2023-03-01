# Tổng hợp những kinh nghiệm IAP tích hợp : 

## Các bước triển khai : 

### Android : 
- Trong Google Play Console, thêm Payment Profile. Tiếp đến là tạo sẵn các Template, các gói sản phẩm sẵn.
- Vào trong app cụ thể, ở mục Monetize > Products > In-app products : Tạo các IAP Product mà sẽ sử dụng trong app. 
- Ở mục Monetization Setup > License : Coppy mã code để tý setup nó ở trong project trên trang Unity. 

### IOS : 
- Lưu ý: Kiểm tra phần seting tài khoản Apple : Mục Banking, Tax và Agreements xem đã setup chưa. 
- Vào trang applestoreconnect > Chọn Game > Chọn In App Purchases : Tạo 1 IAP 
- TIếp tục vào trong IAP đó, cập nhật đủ các thông tin khác : Price Schedule, App Store Localization (English), Add Review Info ( Ảnh về button đó trong game của bạn - Ảnh cần kích thước là 2436x1125, và review là chỉ rõ vị trí button đó ở đâu, chỗ nào, ấn vào thì để làm gì - VD : It's a top button. Click it to buy this IAP, when buy success, user will get 1000 gems ). -> Trạng thái sẽ được chuyển sang Ready To Submit. 