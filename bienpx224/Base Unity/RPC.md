# Chia sẻ về RPC : 

Bạn có thể tham khảo mô hình của Valorant ở đây: https://technology.riotgames.com/news/peeking-valorants-netcode?fbclid=IwAR1VV0YPAAw_MWPtOK53AROttaoRal9m3HDPKThsTcXtULDNkszL63YJMic
Họ áp dụng 1 số kỹ thuật như:
.
NHÓM CƠ SỞ HẠ TẦNG:
- Tự xây dựng hệ thống "internet" với tên gọi Riot Direct để giảm tần suất thất lạc gói tin UDP và giảm delay: https://technology.riotgames.com/.../fixing-internet-real...
- Đặt server ở nhiều nơi trên Thế giới dựa vào dữ liệu thu thập được, mục tiêu để giúp 70% player có mức ping 35ms
.
NHÓM BACKEND TECHNIQUES:
- Tối ưu server để update 128 lần / giây, giảm thời gian round-trip của gói tin: https://technology.riotgames.com/.../valorants-128-tick...
- Đồng bộ hóa đồng hồ ở server và client, server có thể lội frame history để validate hit
- Buffer tối thiểu (mục tiêu chỉ 1/2 frame trễ ở server và chỉ 1 frame trễ ở remote-client)
- Áp dụng Line-of-sight ở server để đảm bảo player nhìn thấy nhau thật thì mới cho bắn: https://technology.riotgames.com/.../demolishing...
- Mở rộng bounding-box THEO HƯỚNG DI CHUYỂN để dự đoán trước vị trí có thể dính đạn của player trên server: https://technology.riotgames.com/.../demolishing...
- Áp dụng Occlusion Culling (theo PVS Grid) trên server để quick-check xem player có thấy nhau thật không: https://technology.riotgames.com/.../demolishing...
- Lưu vị trí của các collider theo animation pose trong N frame gần nhất để nội suy xem tại thời điểm "trúng đạn", có đúng là collider bị hit hay không: https://technology.riotgames.com/.../valorants-128-tick...
.
NHÓM GAME DESIGN:
- Design level sao cho giảm thiểu các vị trí camping tầm xa thuận lợi cho camper
- Súng bắn sẽ giảm độ chính xác khi di chuyển, buộc peeker phải dừng lại, đồng thời tăng thời gian phản ứng cho đối phương
- Khi trúng đạn không vào chỗ hiểm, mỗi step di chuyển sẽ nhỏ hơn, giúp player đó dễ nhắm bắn shooter ở không gian mở (còn camper thì chịu)
