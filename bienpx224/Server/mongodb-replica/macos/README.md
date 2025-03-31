# MongoDB Replica Set

Hướng dẫn thiết lập MongoDB Replica Set với 3 node và xác thực.

## Yêu cầu

- Docker và Docker Compose
- Quyền thực thi các script shell

## Cách sử dụng nhanh (Khuyến nghị)

0. Stop và Remove container mongo cũ trong docker nếu có cho chắc : 
```sh
# Liệt kê các container đang chạy
docker ps

# Dừng container MongoDB standalone
docker stop <container_id_hoặc_tên>

# Xóa container MongoDB standalone
docker rm <container_id_hoặc_tên>
```

1. Chạy lệnh sau để khởi động MongoDB Replica Set và tạo user admin:

```bash
cd mongodb-replica
sudo chmod +x init-replica.sh
./init-replica.sh

# Thực hiện mở port : 
sudo ufw allow 27017/tcp
sudo ufw allow 27018/tcp
sudo ufw allow 27019/tcp

# Cài thêm mongosh vào hệ thống : 
# Thêm kho lưu trữ MongoDB
wget -qO - https://www.mongodb.org/static/pgp/server-7.0.asc | sudo apt-key add -
echo "deb [ arch=amd64,arm64 ] https://repo.mongodb.org/apt/ubuntu $(lsb_release -cs)/mongodb-org/7.0 multiverse" | sudo tee /etc/apt/sources.list.d/mongodb-org-7.0.list

# Cập nhật và cài đặt mongosh
sudo apt update
sudo apt install -y mongodb-mongosh

# Kiểm tra cài đặt
mongosh --version
```

2. Replica Set sẽ được thiết lập với:
   - 3 node: mongo1, mongo2, mongo3
   - User admin được tạo với thông tin:
     - Username: admin
     - Password: abcd1234

3. Kết nối tới MongoDB Replica Set:

```
mongodb://admin:abcd1234@localhost:27017,localhost:27018,localhost:27019/?replicaSet=rs0&authSource=admin
```

## Kiểm tra kết nối

Để kiểm tra kết nối từ máy host:

```bash
sudo chmod +x test-connection-uri.sh
./test-connection-uri.sh
```

Script này sẽ thử kết nối tới MongoDB Replica Set với 3 cách khác nhau:
1. Không xác thực (sẽ thất bại nếu xác thực đã được bật)
2. Kết nối với xác thực vào 1 node
3. Kết nối vào replica set đầy đủ

## Thực hiện thủ công

Nếu muốn thực hiện từng bước thủ công:

0. Kiểm tra xoá các mongo container trong docker : 
- Trong trường hợp trước đó tôi có tạo mongo chạy trên docker (bản standardalone) : 

1. Khởi động các container:
```bash
cd mongodb-replica
docker-compose up -d
```

2. Khởi tạo Replica Set và tạo user admin:
```bash
docker exec -it mongo1 mongosh --file /scripts/setup-mongo.js
```

3. Kiểm tra trạng thái Replica Set:
```bash
docker exec -it mongo1 mongosh --eval "rs.status()"
```

4. Kiểm tra user admin đã được tạo:
```bash
docker exec -it mongo1 mongosh --eval "db.getSiblingDB('admin').getUsers()"
```

## Dừng và xóa Replica Set

```bash
cd mongodb-replica
docker-compose down
```

## Khắc phục lỗi kết nối

Nếu bạn gặp lỗi "getaddrinfo ENOTFOUND mongo1" khi kết nối từ Mongo Compass hoặc các ứng dụng khác, hãy sử dụng connection string với localhost:

```
mongodb://admin:abcd1234@localhost:27017,localhost:27018,localhost:27019/?replicaSet=rs0&authSource=admin
```

## Lỗi "Authentication failed"

Nếu bạn gặp lỗi "Authentication failed" khi kết nối bằng Mongo Compass, có thể do:

1. User admin chưa được tạo thành công. Kiểm tra bằng lệnh:
```bash
docker exec -it mongo1 mongosh --eval "db.getSiblingDB('admin').getUsers()"
```

2. Chạy lại toàn bộ quá trình:
```bash
./init-replica.sh
``` 