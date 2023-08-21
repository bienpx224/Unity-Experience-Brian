# Tổng hợp những kinh nghiệm IAP tích hợp : 

## Các bước triển khai : 
- Tạo trước các template price. VD : 1.99 usd (24000 VND)... để có thể sử dụng chung cho các project.
### Android : 
- Trong Google Play Console, thêm Payment Profile. Tiếp đến là tạo sẵn các Template, các gói sản phẩm sẵn.
- Vào trong app cụ thể, ở mục Monetize > Products > In-app products : Tạo các IAP Product mà sẽ sử dụng trong app. 
- Ở mục Monetization Setup > License : Coppy mã code để tý setup nó ở trong project trên trang Unity. 

### IOS : 
- Lưu ý: Kiểm tra phần seting tài khoản Apple : Mục Banking, Tax và Agreements xem đã setup chưa. 
- Vào trang applestoreconnect > Chọn Game > Chọn In App Purchases : Tạo 1 IAP 
- TIếp tục vào trong IAP đó, cập nhật đủ các thông tin khác : Price Schedule, App Store Localization (English), Add Review Info ( Ảnh về button đó trong game của bạn - Ảnh cần kích thước là 2436x1125, và review là chỉ rõ vị trí button đó ở đâu, chỗ nào, ấn vào thì để làm gì - VD : It's a top button. Click it to buy this IAP, when buy success, user will get 1000 gems ). -> Trạng thái sẽ được chuyển sang Ready To Submit. 

### Trong Unity : 
- Vào Unity : Services > IAP > Configure > sẽ ra trong Project Settings > Chọn Services > IAP : Ấn ON để bật IAP.
- Trong Services > IAP > IAP Catalog : Thiết lập thông số các IAP ID mà ta sẽ sử dụng. Thông số giống như đã thiết lập ở trên tài khoản Google Play Console hoặc Apple Developer. 
- Tạo các Button để mua IAP : Có thể tự tạo button bình thường rồi gắn thêm component IAP Button hoặc vào Services > IAP > Create IAP Button. 
- Thiết lập id, thông số cho IAP Button đó. Trong file Shop Manager chẳng hạn, tạo các func để lắng nghe sự kiện OnPurchaseComplete và OnPurchaseFailed : 
```
public void OnPurchaseComplete(Product product)
    {
        string productId = product.definition.id;
        string transactionID = product.transactionID;
        string msg = string.Format("OnPurchased: Success buy {0} with tracsactionID: {1}",productId, transactionID);
        CallPaymentToServer(transactionID, productId);
        SendTrackingEvent(TrackingEventType.BuyIAP.ToString(), msg);
    }

public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        string productId = product.definition.id;
        string transactionID = product.transactionID;

        string msg = string.Format("OnPurchaseFailed: FAIL. storeSpecificId: {0}. PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason);
        // string msg = string.Format("Transaction has been cancelled");

        // Debug.Log(failureReason);
        // Debug.Log(string.Format("ProductID : {0} / TransactionID : {1}", productId, transactionID));
        // Debug.Log(msg);

        SendTrackingEvent(TrackingEventType.BuyIAP.ToString(), msg);
    }
```