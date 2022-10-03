# Tổng hợp những kinh nghiệm Sprite : 

## Chia sẻ : 

Sprite Atlas là công cụ rất tuyệt vời nếu biết dùng đúng cách: Giúp tối ưu về CPU, Memory và Build Size.
1. Cần pack atlas theo ngữ cảnh + đảm bảo tỉ lệ fill rate: tránh load thừa, hay pack trống quá nhiều
2. Trường hợp 1 ảnh dùng ở 2 hay nhiều chỗ khác nhau cần cân nhắc, tránh cố gắng tận dụng dùng chung atlas, đôi khi double ảnh và pack thành 2 atlas ở 2 nơi sẽ tốt hơn (vẫn là câu chuyện tối ưu draw call và Memory)
3. Hiện tại bên mình đang dùng format ASTC 8x8 là chủ yếu (vừa đủ đẹp), thỉnh thoảng sẽ sử dụng crunched ETC2 để tối ưu hơn về build size nếu cần. Với các sản phẩm bên mình thì ưu tiên về tối ưu memory hơn.

Đầu tiên là lợi ích của Sprite Atlas:
- Lợi ích đầu tiên là giảm draw call. Mỗi draw call sẽ chứa texture, material, shader, mesh vertices,… để gửi cho GPU render. Khi pack nhiều texture về 1 texture, dùng chung material hoặc khác material nhưng shader support GPU instancing, chung mesh (thông qua batching) thì có thể render nhiều object với chỉ 1 draw call. Giảm draw call giúp giảm gánh nặng cho CPU (gom data từ ram về CPU rất chậm nên nếu chỉ cẩn lấy 1 texture thì nhanh hơn nhiều lần 10 texture nằm rải rác trong ram, có thể tối ưu cache miss cho CPU). Giảm gánh nặng cho GPU(vẽ 1 bức to nhanh hơn vẽ 10 bức nhỏ nội dung khác nhau)
- Lợi ích thứ 2 là gom những texture non-power of two lại thành một tấm atlas thoả mãn power of two. Texture có size không thoả mã power of two khi được gửi đến GPU vẫn sẽ được padding thành power of two. Vd size 15x13 sẽ padding thành 16x16 gây lãng phí GPU memory. GPU được thiết kế để tối ưu parallel processing và sử dụng nhiều phép toán matrix, vector. Các phép toán này khi sử dụng ma trận vuông thì tính toán cực kỳ nhanh. Texture thoả mãn Power of 2 luôn có thể chia ra thành nhiều square block nên sẽ rất nhanh cho GPU xử lý.
- Lợi ích thứ 3 là có thể apply compression, xử lý ảnh trên một tấm atlas thay vì phải làm trên nhiều tấm nhỏ. Điều này giúp tối ưu pipeline đặc biệt khi dùng Devops.
- Mh chia sẻ một trick để tạo nhanh các cấu hình texture cho device từ low đến high-end. Ae có thể dùng Quality scale của SpriteAtlas để tạo ra nhiều phiên bản. Vd máy xịn để scale là 1, máy yếu để scale 0.5. Từ đó với một bộ asset, ta có thể dynamic load SpriteAtlas phù hợp với cấu hình máy.
Tiếp theo là lưu ý khi dùng Sprite Atlas:
- Chỉ nên gom vào cùng một atlas những texture xuất hiện cùng một lúc hoặc rất gần nhau trong flow của chương trình. Lý do là khi load 1 texture trong atlas thực chất đã load cả tấm atlas vào ram. Nếu các texture k được dùng cùng lúc sẽ gây lãng phí bộ nhớ. Thậm chí gây memory leak nếu liên tục clone texture con. Vì khi sử dụng API GetSprite thì Unity trả về clone texture. Bản gốc vẫn nằm trong tấm atlas. Cần linh hoạt quản lý để load, unload atlas.
- Cần giảm tối đa khoảng trắng trong atlas. Fill càng kín càng tốt. Cá nhân mh thường yêu cầu fill > 80% diện tích của atlas.