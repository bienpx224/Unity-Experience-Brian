
# Cài đặt và sử dụng AI N8N : 

## Cài đặt : 
- [Cài đặt docker Tham khảo](https://123host.vn/tailieu/kb/vps/huong-dan-cai-dat-n8n-tren-ubuntu.html)

### Cài docker : 
```sh
sudo apt-get remove docker docker-engine docker.io containerd runc
sudo apt-get update
sudo apt-get install ca-certificates curl gnupg lsb-release
sudo mkdir -p /etc/apt/keyrings
curl -fsSL https://download.docker.com/linux/ubuntu/gpg | sudo gpg --dearmor -o /etc/apt/keyrings/docker.gpg
echo "deb [arch=$(dpkg --print-architecture) signed-by=/etc/apt/keyrings/docker.gpg] https://download.docker.com/linux/ubuntu $(lsb_release -cs) stable" | sudo tee /etc/apt/sources.list.d/docker.list > /dev/null

sudo apt-get update
sudo apt-get install docker-ce docker-ce-cli containerd.io
```
- Cài đặt Docker Compose : 
```
sudo apt-get install docker-compose-plugin
```

### Cài đặt Postgree DB : 

- Tiếp theo chúng ta sẽ tiến hành cài đặt PostgreSQL bằng cách thực thi lệnh sau:
```
apt install -y postgresql postgresql-contrib
```
- Để PostgreSQL để listen port cả trên local và IPv4 public chúng ta thực thi lệnh sau:
```
sed -i "s/#listen_addresses = 'localhost'/listen_addresses = '*'/g" /etc/postgresql/*/main/postgresql.conf
```
- Tiến hành kiểm tra IP của docker, để chúng ta có thể cấu hình cho phép dãy địa chỉ IP của docker kết nối đến database.
```
ip add | grep docker
```
- Sau khi có IP của docker chúng ta sẽ thực hiện cấu hình cho phép dãy địa chỉ IP của docker kết nối đến database PostgreSQL.
```
echo "host all all 172.18.0.0/16 md5" | tee -a /etc/postgresql/*/main/pg_hba.conf
```
- Sau khi đã thực hiện các bước cấu hình trên tiến hành khởi động lại PostgreSQL để áp dụng cấu hình mới. Và cấu hình cho PostgreSQL chạy cùng hệ thống khi khởi động lại VPS.
```
systemctl restart postgresql
systemctl enable postgresql
```
- Tiến hành kiểm tra lại trạng thái PostgreSQL:
```
systemctl status postgresql
```
- Tiến hành tạo User and Database để cấu hình kết nối cho N8N bằng cách thực thi lệnh sau:
```
sudo -i -u postgres
```
- Truy cập vào psql bằng lệnh sau:
```
psql
```
- Sau khi kết nối postgres chúng ta sẽ thực hiện tạo User and Database
```
CREATE DATABASE n8n;
CREATE USER n8n WITH PASSWORD 'n8n';
GRANT ALL PRIVILEGES ON DATABASE n8n TO n8n;
\q
```

- Sau khi tạo hoàn tất chúng ta có thể kiểm tra lại thông tin đã đúng chưa bằng cách thực thi lệnh sau : 
```
psql -h localhost -U n8n -d n8n
```
- Thực hiện exit để thoát khỏi user Postgre, chuyển về user root hệ thống như bình thường. 

### Tạo thư mục n8n docker : 
- Tạo thư mục docker n8n : 
mkdir n8n
- Di chuyển vào thư mục n8n.
cd n8n
- Tiến hành tạo file docker-compose.yml với nội dung như sau:
***File docker-compose.yml***
- Bạn cần thay IP VPS của bạn vào mục DB_POSTGRESDB_HOST.

- Sau đó chúng ta sẽ tạo file .env để khai báo các biến cho file docker-compose.yml với nội dung sau:
***File .env***

- Bạn cần thay đổi các giá trị trong [] cho phù hợp với khai báo của bạn.

- Sau đó tiến hành run docker-compose để N8N có thể hoạt động.
```
docker compose up -d
```

- Để kiểm tra container có run thành công không chúng ta thực thi lệnh sau:
```
docker ps
```

- ***Lưu ý:*** Nếu trong quá trình start container bị lỗi thì lỗi liên quan đến quyền của thư mục .n8n_storage, chúng ta sẽ tiến hành phân quyền lại thư mục này bằng thực thi lệnh sau:
chmod -R 755 ./.n8n_storage
- Sau đó tiến hành tắt container n8n bị lỗi trong quá trình build:
docker compose down
- Rồi tiến hành start lại container:
docker compose up -d

