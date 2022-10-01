# Tổng hợp những kinh nghiệm Chia sẻ kĩ thuật, Tối ưu hiệu năng (Optimize) :

## Tài liệu HỮU ÍCH : 
- The Game Design Partterns:  https://www.theseus.fi/bitstream/handle/10024/150922/Rautakopra_Anni.pdf?sequence=1&isAllowed=y
- Build BIG Game : https://www.youtube.com/watch?v=ECb31GwoSsM 

## Tài liệu Advance Unity Performance :
- The Gamedev Guru : https://www.youtube.com/c/TheGamedevGuru/playlists / https://www.performancetaskforce.com/ 

## Memory Management in Unity : 
- Ebook optimize your mobile game performance : https://images.response.unity3d.com/Web/Unity/%7Be61fa3cf-3cf0-4022-a5f5-df0087ea93f9%7D_JW-10207_Unity_eBook_OptimizeYourMobileGamePerformance_V6_May2021.pdf?utm_campaign=___&utm_content=2021-04-DG-optimize-mobile-game-eBook-TY&utm_medium=email&utm_source=Eloqua&elqTrackId=35e07ca1ec9540f395cec0d86b8edf0d&elq=d4addf5679e84e4c9cb32be48318b04f&elqaid=30112&elqat=1&elqCampaignId= 

- Basic : https://learn.unity.com/tutorial/memory-management-in-unity?fbclid=IwAR2852RGlsNeQWC9uwUMdRBoju4dDLP3n-zS0sS5ROvaUenxPYwdJZP8P-E# 

## Một số tài liệu Optimize sưu tầm dc : 
- Learn how to optimize Unity project : https://www.habrador.com/tutorials/unity-optimization/?fbclid=IwAR3LwCvmH3Cz2Re__HtQGZz86NDSqxUIWftDhLYsB-c8jo3zveF5HOdGXi0 

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