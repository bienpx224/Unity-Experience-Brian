# Tổng hợp những kinh nghiệm Chia sẻ kĩ thuật, Tối ưu hiệu năng (Optimize) :

## Unity WebGL Knowledge : 
- Document hay về việc triển khai Unity WebGL : bao gồm create, build, interact, custom .. : https://unity.com/how-to/profile-optimize-webgl-build#create-build

- React and Unity Interacting : NPM react-unity-webgl docs : https://react-unity-webgl.dev/docs/api/send-message

## Build giảm dung lượng : 
- Giảm Quality 
- Sử dụng Sprite Atlas, size ảnh chia hết cho 4, compress tất cả ảnh xuống chất lượng phù hợp 
- Build Release, disable tất cả log đi sẽ giảm dung lượng đi rất nhiều. 
- Kiểm tra Editor Log để xem thắt cổ chai ở đâu, dung lượng các file như nào. 
- Trên các thiết bị Mobile thì sử dụng ASTC 8 bit hoặc RGB Compress ATC2 8 bit, còn trên webGL thì sử dụng RGBA Crunched với quality 50. 
=> Dung lương bản build dev game Pacman đã giảm từ 60Mb xuống còn 8.7Mb => Tuyệt vời. 
- Vào trong manifest.json, xoá hết các dependencies mặc định hoặc ko dùng tới đi. 
- Trong Player Setting khi build, chọn Code Optimization là Disk Size (Giảm size bản build). 

- 1 Số link tham khảo khác để bổ sung thêm : 
    + https://forum.unity.com/threads/webgl-builds-for-mobile.545877/page-2#post-9021013


## Tài liệu HỮU ÍCH : 
- The Game Design Partterns:  https://www.theseus.fi/bitstream/handle/10024/150922/Rautakopra_Anni.pdf?sequence=1&isAllowed=y
- Build BIG Game : https://www.youtube.com/watch?v=ECb31GwoSsM 

## Tài liệu Advance Unity Performance :
- The Gamedev Guru : https://www.youtube.com/c/TheGamedevGuru/playlists / https://www.performancetaskforce.com/ 

## Memory Management in Unity : 
- Ebook optimize your mobile game performance : https://images.response.unity3d.com/Web/Unity/%7Be61fa3cf-3cf0-4022-a5f5-df0087ea93f9%7D_JW-10207_Unity_eBook_OptimizeYourMobileGamePerformance_V6_May2021.pdf?utm_campaign=___&utm_content=2021-04-DG-optimize-mobile-game-eBook-TY&utm_medium=email&utm_source=Eloqua&elqTrackId=35e07ca1ec9540f395cec0d86b8edf0d&elq=d4addf5679e84e4c9cb32be48318b04f&elqaid=30112&elqat=1&elqCampaignId= 

- Basic : https://learn.unity.com/tutorial/memory-management-in-unity?fbclid=IwAR2852RGlsNeQWC9uwUMdRBoju4dDLP3n-zS0sS5ROvaUenxPYwdJZP8P-E# 

## Một số tài liệu Optimize sưu tầm dc : 
- Learn how to optimize your Unity project : https://www.habrador.com/tutorials/unity-optimization/?fbclid=IwAR3LwCvmH3Cz2Re__HtQGZz86NDSqxUIWftDhLYsB-c8jo3zveF5HOdGXi0 
- Optimize Your MObile Game Performance : [Link](https://images.response.unity3d.com/Web/Unity/%7Be61fa3cf-3cf0-4022-a5f5-df0087ea93f9%7D_JW-10207_Unity_eBook_OptimizeYourMobileGamePerformance_V6_May2021.pdf?utm_campaign=___&utm_content=2021-04-DG-optimize-mobile-game-eBook-TY&utm_medium=email&utm_source=Eloqua&elqTrackId=35e07ca1ec9540f395cec0d86b8edf0d&elq=d4addf5679e84e4c9cb32be48318b04f&elqaid=30112&elqat=1&elqCampaignId=)
- Unity Multiplayer Games.pdf :[Link](http://naneport.arg.in.th/books/ComputerIT/Unity%20Multiplayer%20Games.pdf)

- Unity Realtime Multipplayer Hackanoon : [Link](https://hackernoon.com/search?query=Unity+Realtime+Multiplayer)


## Collections by the time : 
- Tối ưu bộ nhớ + build size: Hiện game có thể chơi tốt trên thiết bị 1.5G RAM (Support > 90% số lượng thiết bị).
- Tối ưu ảnh: Nine patch image, patch atlas. Build 2 version bundle ứng với format ASTC (chủ yếu là 8x8) và ETC2 (cần có thống nhất với art + dev tự xây dựng tool để format ảnh). Trường hợp xấu nhất không support tự fallback về 16bit. Những ảnh effect, background ko cần rõ: DXT5...
- Âm thanh: Giảm số lượng, chất lượng âm thanh ko cần thiết. Là game idle user ko tập trung quá nhiều vào âm thanh => Không tối ưu được nhiều.
- Sử dụng pool: Cái này thì ai cũng làm rồi mình không giải thik nhiều
- Cài đặt giải thuật LRU để đưa ra quyết định giải phóng bộ nhớ: https://medium.com/.../my-first-blog-on-medium-583159139237
Mình dùng giải thuật cho 2 mục đích chính:
+ Unload các assetbundle lâu không được sử dụng
+ Delete các popup, object(kể cả trong pool) lâu không được gọi.

Khi cần thương mại hoá thì dùng bundle cho người chơi tải về sẽ
- Tiết kiệm được tiền quảng cáo (Cùng 1 game đó thì nhẹ hơn = rẻ hơn)
- Trông hấp dẫn hơn với người dùng, vì nhìn game 50 MB sẽ có cảm giác muốn tải hơn game 500 MB. Khi nào chuẩn bị cần tới bộ bundle nào thì tải nó về sau. Ví dụ ng chơi chơi tới màn 5 thì mới tải bundle cho màn 10 -> 20

## Optimize Sprite trong 2D game : 
OPTIMIZATION TIPS
 Enable Texture Compression for all sprites to minimize build size
 Use Crunch Compression for all sprites to minimize build size
 Use Sprite Mode > Mesh Type = Tight because all sprites are 512x512 px and are not cropped
 Set Sprite Mode > Extrude Edges = 2 or more if you have crop artefacts
 Use Packing Tags for sprite groups to improve performance (legacy feature, but preferred)
 Enable Legacy Sprite Packer from Editor settings
 Refer to Unity docs for details about Texture Compression and Packing Tags
 Consider to use Sprite Atlas (replacement for Legacy Sprite Packer)
 Read more about sprite packing in Sprite Atlas (Packing) section below
