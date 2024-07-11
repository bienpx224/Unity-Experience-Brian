# Publish Node project run in server : 

## Pull source code : 
- Đẩy source code lên git
- Truy cập vào server bằng terminal, thực hiện clone project về như dưới local. 
- Nếu có .env thì tạo file hoặc cp .env.example .env 
- Thực hiện chạy npm i, npm start xem chạy thử trên server ok chưa. 

## Run Project Node in server : 
- Muốn chạy dự án nodejs trên server, cần cài đặt pm2 : 
npm i -g pm2 
- Thực hiện chạy pm2 cho dự án : 
pm2 start index.js --name "name-project"

## Kiểm tra port chạy dự án đã ready chưa, đã được mở chưa, đã được sử dụng ở chỗ khác chưa. 

### TH PORT chưa được mở : 
- Kiểm tra các cổng đã mở bằng câu lệnh : 
`sudo firewall-cmd --list-ports`
- VD Thực hiện mở cổng 5002 trên server có cấu hình là : CentOS Linux release 7.9.2009 (Core) 

Kiểm Tra firewalld Đang Chạy
Trước tiên, đảm bảo rằng firewalld đang chạy:

bash
Copy code
sudo systemctl status firewalld
Nếu firewalld không chạy, bạn có thể kích hoạt và khởi động nó:

bash
Copy code
sudo systemctl enable firewalld
sudo systemctl start firewalld
Mở Cổng 5002
Thêm quy tắc để mở cổng 5002:

bash
Copy code
sudo firewall-cmd --zone=public --add-port=5002/tcp --permanent
Tải lại cấu hình firewalld để áp dụng thay đổi:

bash
Copy code
sudo firewall-cmd --reload
Xác minh rằng cổng đã mở:

bash
Copy code
sudo firewall-cmd --list-ports


