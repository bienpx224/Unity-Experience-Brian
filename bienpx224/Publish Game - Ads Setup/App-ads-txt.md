# How to integrate App-ads.txt : 

- Trong TH đang sử dụng Google Admob, trước khi tung ra bản release, bạn cần chắc chắn rằng đã verify app-ads.txt trong ứng dụng của bạn. 

## App-ads.txt là gì ? 
- Hiểu rõ hơn về nó, lợi ích cũng như cách kiểm tra, tham khảo [link](https://mikotech.vn/adstxt-la-gi/#:~:text=txt%20(hay%20Authorized%20Digital%20Sellers,c%C3%A1o%20tr%C3%AAn%20trang%20web%20%C4%91%C3%B3.)
- Ads.txt (hay Authorized Digital Sellers) là một tệp văn bản được đặt trên trang web của nhà xuất bản (publisher) để liệt kê các đối tác quảng cáo (distributor) được ủy quyền bán quảng cáo trên trang web đó.
- Với ads.txt, nhà xuất bản có thể xác định rõ những đối tác quảng cáo chính thức mà họ cho phép bán quảng cáo trên trang web của mình. Tệp văn bản này chứa thông tin về tên đối tác quảng cáo, ID của đối tác và trạng thái ủy quyền. Điều này giúp ngăn chặn việc sử dụng đối tác quảng cáo giả mạo hoặc không được ủy quyền bởi nhà xuất bản.


## Cách triển khai : 
- Doc của Google : [Link](https://support.google.com/admob/answer/9363762?hl=en&ref_topic=7384409&sjid=11273018501970942103-AP)

- Nếu ko có domain của riêng mình, có thể truy cập [link](https://app-ads-txt.com/) sau để generate ra file app-ads.txt và họ sẽ gen sẵn file của trong 1 link domain. 
- Nội dung nhập trong file app-ads.txt thì lấy từ trong Admob : Trang chủ admob > Apps > View All Apps > Setup app-ads.txt
- Sau khi có được domain gen file đó thì ta vào Play Store Console > Chọn app > Grow > Store presense > Store Settings : Nhập domain đó vào trong phần "website"

