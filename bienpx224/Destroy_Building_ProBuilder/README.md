# Unity-Experience-Brian
Base experience about Unity : 


## Hướng dẫn Destroy Building : ProBuilder : CodeMonkey : 
- Link download project example : https://unitycodemonkey.com/downloadpage.php?yid=tPWMZ4Ic7PA
- Video Tutorial : https://www.youtube.com/watch?v=tPWMZ4Ic7PA 
- Lưu ý khi cài : Sau khi import Package vào project, ta cần Install các package như theo link download hướng dẫn và thêm cả package Animation Rigging, Cinemachine, Sprite 2D, Universal RP, ProBuilder.
-> Đang bị lỗi. 
* DÙng Probuilder luôn, ko cần Project Example nữa.
- Link Tutorial : https://www.youtube.com/watch?v=InpKZloVk0w 
- Trong Package Manager cài đặt Package ProBuilder 
- Chọn Model cần destroy, chọn Mesh cần destroy, vào ProBuilder, chọn phần ProBuilderize để phân tách mesh đó thành các mảnh. 
- Vào File > Preferences > ProBuilder > TÍch chọn Experience features enable để sử dụng thêm tính năng của ProBuilder.
- Để CUT/SLICE Object thì ta cần tạo ra 1 3D object khác, ví dụ như Cube, để chèn vào vị trí cần cần cắt rồi 

- Muốn dùng các tính chất vật lí thì đừng quên thêm Mesh Collider (sẽ có sẵn mesh collider phù hợp với shape của Object) và Rigidbody.

## Optimize : 
- Khi dungf ProBuilder (ProBuilizable) hoặc những tính năng khác có thể sẽ sinh ra nhiều verts, polygon, triangles... -> sẽ làm mesh phức tạp hơn, việc vẽ sẽ khó hơn, sinh ra nhiều Batchs hơn.. Vậy nên có thể sẽ gây ra hiện tượng giật lag, ảnh hưởng tới FPS. 
-> Vào các Object đó, xoá Mesh Collider đi, ko dùng Collider đó nữa, mà thêm Component Box Collider (hoặc Circle collider gì đó tuỳ vào shape của object) vào để sử dụng thay cho Mesh Collider sẽ giúp tối ưu hơn.
