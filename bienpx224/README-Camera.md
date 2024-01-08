# Experiences about Camera, Setup Camera in game. 

## Hướng dẫn setup 2 Camera : Main Camera và Overlay camera 
- Ví dụ : Trong game platform 2D : Ngoài 1 Main camera follow theo Player, nhìn vào World Space thì cần 1 Camera khác để show các button, UI mà luôn hiện trong khu vực màn hình điện thoại. 
- Đang dùng bản 2021.3.13f1, ở các bản khác có thể có các settings khác nhau, ko giống như bản này. Tuy nhiên cơ bản cũng sẽ hơi giống nhau. 
- Để setup : Ta tạo 2 Camera : 
     + Main Camera : Render Type (Base), Projection (orthographic)
     + Overlay Cam : Render Type (Overlay), Projection Orthographic 
- Trong Main Cam:
    + Trong setup Stack ta thêm Overlay Camera vào trong List. 
    + Trong Environment chọn Background Type là Sky Box. Nếu ko chọn thì khi build ra game sẽ bị kiểu Screen Tearing hay gì đó, nói chung có nhiều vệt trên màn hình chơi. 
    + Trong Cilling Mask : Chọn các Layer mà sẽ được hiển thị trên Camera này : VD : Player, Enemy, Environment, Background,...
    + Có thể Thêm CameraFollowPlayer.cs vào đây để Main cam này sẽ luôn di chuyển theo Player. Còn Overlay cam thì ko cần thêm script này, vì nó luôn có định theo Screen Device. 
- Trong Overlay Cam : 
    + Ở Rendering > Culling Mask : Chọn các Layer sẽ được hiển thị trên Overlay Cam. VD : UI, Popup, Overlay. 

=> Đã setup xong Camera, giờ chuyển sang setup Canvas để hiển thị. 
- Ở project tôi thường dùng 2 Cavas, 1 là Main Canvas để hiển thị UI trên screen, 2 là Popup Canvas, để hiển thị các Popup được Spawn ra trên UI. 
- Các Canvas này đều có chung Setup đó là : 
    + Canvas > Render mode : Screen Space - Camera 
    + Render Camera : Overlay Camera 
    + Sorting Layer thì thường để là UI. sorting layer như nào là do tuỳ các bạn. 
    + Component Canvas Scaler : Có UI Scale Mode (Scale With Screen Size), Resolution đang expect trên IPX ( 2436x1125 ), Match Width Or Height. 
    + Component Graphic Rayscaler.  
    + Canvas Main thì để layer là UI, Canvas Popup thì để layer là Popup, để khi hiển thị thì các layer sẽ đè và che nhau đúng với mong muốn. 

=> Cơ bản xong rồi. Ko biết thiếu sót, cần bổ sung gì ko. Khi làm gặp thì bắt tay xử sau. 
