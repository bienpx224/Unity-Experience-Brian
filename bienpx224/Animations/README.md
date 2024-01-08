# Unity-Experience-Brian
Base experience about Animation

## Animator and Animations for movements: 
- Vào Object Player, tạo Animations và Animator: 
- Kéo Sprites vào khung animations, chỉnh sửa timing cho phù hợp. VD cho anim Run
- Vào Animator chỉnh transactions. Tạo transaction từ AnyState sang Idle, từ Idle sang Run. 
- Trong mũi tên từ Idle > Run : Tắt has exit time và chỉnh Transition Duration trong Setting = 0.
- Tạo Param để làm điều kiện chuyển đổi anim. VD tạo biến bool isRun. Sau đó trong mũi tên từ Idle > Run ta thêm điều kiện isRun = true.

## Make Character walking 4 direction with animation specific: 
- Prepare 4 animation walking for 4 directions.
- Create animation controller, create blendtree and reference below tutorial: 
- Tutorial example : https://www.youtube.com/watch?v=uZlSOAP76-A&t=1174s 
