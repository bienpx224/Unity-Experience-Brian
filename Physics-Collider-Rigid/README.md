# Chia sẻ về Physic, Rigidbody, Collider...

## Rigibody và Character Controller : 
- Compare : https://viblo.asia/p/so-sanh-giua-charater-controller-va-rigid-body-trong-unity3d-phan-2-bWrZn0oY5xw 
- RigidBody sẽ phản ứng rất chính xác và thậm chí sử dụng những tính chất vật lý để tính toán chính xác nhất. Trong khi đó CharacterController sẽ ít chặt chẽ hơn, chúng ta có thể tự động leo qua những gờ dốc thấp (tùy theo tham số) mà không cần xử lý gì.
- Rigidbody cung cấp cho chúng ta rất nhiều tương tác vật lý mà Character Controller không có, vì vậy nếu dùng Character Controller thì chúng ta sẽ cần phải code nhiều hơn cho cùng một tính năng. => ít tính năng dùng Character Controller.
## Rigibody : 
- Mass – Khối lượng của vật, không nên đặt chỉ số này cao hơn hoặc thấp hơn 100 lần so với các rigidbody khác.
- Drag – Sức cản không khí sẽ ảnh hưởng đến object thế nào, 0 nghĩa là hoàn toàn không có sức cản, vô tận sẽ khiến cho object ngừng di chuyển.
- Angular Drag – Sức cản không khỉ khi vật quay, lưu ý là không thể khiến object ngừng quay với angular drag vô tận.
- Use Gravity – Nếu check, trọng lực sẽ được áp dụng lên object.
- Is Kinematic – Nếu check, object sẽ không được điều khiển bởi engine vật lý mà chỉ có thể điều khiển bởi transform.
- Interpolate – Dùng để điều chỉnh sự va chạm, độ va chạm có thể nhạy hơn tùy từng trường hợp.
- Collision Detection – Dùng để ngăn chặn các object di chuyển quá nhanh xuyên qua các object khác mà không bị va chạm, như khi viên đạn di chuyển nhanh quá, và vượt qua object khác trước khi va chạm được update.
- Ngoài ra RigidBody có thể được áp dụng lực trong code với function AddForce()

- 4 cách để tác dụng lực vào một Rigidbody: 
    + Force: liên tục và phụ thuộc vào khối lượng
    + Acceleration: liên tục và không phụ thuộc vào khối lượng
    + Impulse: tức thời và phụ thuộc vào khối lượng
    + VelocityChange: tức thời và không phụ thuộc vào khối lượng
- MovePosition() : Để thực hiện chuyển động theo input của user, chúng ta sẽ sử dụng hàm MovePosition. Hàm này sẽ cố di chuyển player đến một vị trí được chỉ định trong khi vẫn giữ những luật va chạm.Nó cũng không tác động đến tốc độ của RigidBody.


- isKenematic : Sẽ tắt các tính chất vật lý đi (VD Trọng lực, lực đẩy, ..) và sẽ dùng để mình tự custom viết di chuyển, tương tác vật lý bằng script. Tuy nhiên isKenematic vẫn có tính chất vật lý cơ bản của Collider, ngăn chặn các Object vật lý khác đi xuyên qua.
- Đối với những Object nào mà ko di chuyển, thì ta nên tích vào là Static sẽ giúp tối ưu hơn trong việc render, tối ưu Batchs.

## Collider : 
- Để nhận ra sự va chạm, không giống như các mesh, chúng nhận biết được mỗi khi va chạm với nhau.
- Unity cung cấp những API sau đâu để phát hiện sự va chạm của các collider.

void OnCollisionEnter(Collision collision) – Chạy 1 lần tại thời điểm va chạm giữa 2 vật.
void OnCollisionStay(Collision collision) – Chạy trong mỗi khung hình tại thời điểm 2 vật còn chạm vào nhau.
void OnCollisionExit(Collision collision) – Chạy tại khung hình cuối cùng khi 2 vật không còn chạm vào nhau nữa. Với class Collision chúng ta có thể lấy ra những thuộc tính như:
Contacts – điểm va chạm giữa 2 vât, tính bằng vector3.
GameObject – game object va chạm với object gốc.
RelativeVelocity – vận tốc tương đối.

- Collision : là loại va chạm mà 2 đối tượng sẽ không đi xuyên qua nhau, khi đối tượng này gặp đối tượng kia thì sẽ bị cản lại, bật lại tùy theo tính chất vật lý
*  IsTrigger : 
- Giúp GameObject thuộc tính collider sẽ không bị va chạm bởi bất kỳ object nào, nhưng bản thân sẽ được dùng để phát hiện những va chạm trên nó, tạo ra những event có thể điều khiển được trong code với những function sau đây:

void OnTriggerEnter(Collider other) – Chạy 1 lần tại thời điểm va chạm giữa 2 vật.
void OnTriggerStay(Collider other) – Chạy trong mỗi khung hình tại thời điểm 2 vật còn chạm vào nhau.
void OnTriggerExit(Collider other) – Chạy tại khung hình cuối cùng khi 2 vật không còn chạm vào nhau nữa.

- Các class này sẽ đươc gọi ra khi một Collider khác va chạm vào Collider có thuộc tính As Trigger, từ Collider có thể lấy ra thông tin về object bị va chạm, cụ thể có thể dùng nó để kiểm tra sự va chạm vào 1 vật mà không áp dụng lại lực tương đương, như ví dụ Mario mỗi khi chạm vào đồng tiền