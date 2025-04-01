# MongoDB Replica Set (Cluster) trên Docker

Hướng dẫn thiết lập MongoDB Replica Set sử dụng Docker để triển khai trên môi trường VPS hoặc máy tính cá nhân.

## Tổng quan

Dự án này tạo một MongoDB Replica Set gồm 3 node với khả năng chịu lỗi cao. Cấu hình bao gồm:

- 3 container MongoDB tạo thành Replica Set
- Xác thực nội bộ giữa các node với keyFile
- Xác thực người dùng với tài khoản admin
- Tường lửa UFW để bảo vệ các cổng MongoDB

## Yêu cầu trước khi cài đặt

- Docker và Docker Compose đã được cài đặt
- Ubuntu hoặc hệ điều hành Linux tương tự
- Quyền sudo để cấu hình tường lửa
- Các cổng 27017, 27018, 27019 chưa được sử dụng

## Cấu trúc thư mục

```
.
├── docker-compose.yml          # Cấu hình Docker Compose không có xác thực
├── docker-compose-secure.yml   # Cấu hình Docker Compose với xác thực
├── setup.sh                    # Script thiết lập tự động
├── data/                       # Thư mục chứa dữ liệu MongoDB
│   ├── mongo1/
│   ├── mongo2/
│   └── mongo3/
├── keyfile/                    # Chứa keyfile cho xác thực nội bộ
└── scripts/                    # Scripts để khởi tạo replica set
    ├── init-replica.js         # Script khởi tạo replica set
    └── auth-setup.js           # Script kiểm tra xác thực
```

## Hướng dẫn cài đặt

1. Clone repository này vào máy chủ hoặc VPS của bạn
2. Cấp quyền thực thi cho file setup.sh:

   ```bash
   chmod +x setup.sh
   ```

3. Chạy script cài đặt:

   ```bash
   sudo ./setup.sh
   ```

4. Script sẽ tự động:
   - Tạo cấu trúc thư mục cần thiết
   - Tạo keyFile cho xác thực nội bộ
   - Khởi động các container MongoDB
   - Khởi tạo replica set
   - Tạo các tài khoản người dùng
   - Khởi động lại với xác thực
   - Cấu hình tường lửa UFW

## Sử dụng

### Kết nối từ ứng dụng

Sử dụng connection string sau để kết nối từ ứng dụng của bạn:

```
mongodb://remote_user:3MCr09mTRq9NOWdhdwgf@SERVER_IP:27017,SERVER_IP:27018,SERVER_IP:27019/?replicaSet=rs0&authSource=admin
```

Thay `SERVER_IP` bằng địa chỉ IP thực của máy chủ.

### Kết nối qua MongoDB Compass

1. Mở MongoDB Compass
2. Dán connection string sau:

```
mongodb://remote_user:3MCr09mTRq9NOWdhdwgf@SERVER_IP:27017,SERVER_IP:27018,SERVER_IP:27019/?replicaSet=rs0&authSource=admin
```

Thay `SERVER_IP` bằng địa chỉ IP thực của máy chủ.

### Truy cập qua CLI

```bash
# Truy cập vào MongoDB shell
docker exec -it mongo1 mongosh -u bienpx -p 3MCr09mTRq9NOWdhdwgf --authenticationDatabase admin

# Kiểm tra trạng thái replica set
rs.status()

# Hiển thị danh sách database
show dbs
```

## Bảo mật

### Thông tin xác thực mặc định

- Admin User: `bienpx`
- Admin Password: `3MCr09mTRq9NOWdhdwgf`
- Remote User: `remote_user`
- Remote Password: `3MCr09mTRq9NOWdhdwgf`

**Lưu ý**: Trong môi trường sản xuất, bạn nên thay đổi mật khẩu mặc định.

### Tường lửa UFW

Script tự động cấu hình tường lửa UFW để chỉ cho phép:
- Truy cập SSH (cổng 22)
- Truy cập localhost tới MongoDB (127.0.0.1 → cổng 27017, 27018, 27019)
- Truy cập từ IP của máy chủ tới các cổng MongoDB

## Quản lý

### Khởi động/dừng cluster

```bash
# Dừng cluster
docker-compose -f docker-compose-secure.yml down

# Khởi động lại cluster
docker-compose -f docker-compose-secure.yml up -d
```

### Kiểm tra logs

```bash
# Xem logs của tất cả các container
docker-compose -f docker-compose-secure.yml logs

# Xem logs của một container cụ thể
docker logs mongo1
```

### Sao lưu dữ liệu

Hệ thống đã được tích hợp tính năng sao lưu tự động với chu kỳ lưu trữ 7 ngày gần nhất.

#### Thiết lập backup tự động

Chạy script sau để thiết lập cron job backup tự động hàng ngày:

```bash
chmod +x setup-backup-cron.sh scripts/backup.sh scripts/restore.sh
sudo ./setup-backup-cron.sh
```

Sau khi thiết lập, MongoDB sẽ được sao lưu vào lúc 1:00 AM mỗi ngày.

#### Chạy sao lưu thủ công

```bash
./scripts/backup.sh
```

#### Khôi phục dữ liệu từ bản sao lưu

```bash
./scripts/restore.sh <tên_thư_mục_backup>
```

Ví dụ:
```bash
./scripts/restore.sh mongo-backup-20230401-120000
```

#### Kiểm tra các bản sao lưu hiện có

```bash
ls -lt backup | grep mongo-backup
```

#### Xem nhật ký sao lưu

```bash
tail -f backup/backup.log
```

Dữ liệu MongoDB cũng được lưu trong thư mục `./data`. Bạn có thể thủ công sao lưu thư mục này để bảo vệ dữ liệu của mình.

## Tham khảo

Dựa trên hướng dẫn chính thức từ MongoDB:
[Deploying a MongoDB Cluster with Docker](https://www.mongodb.com/resources/products/compatibilities/deploying-a-mongodb-cluster-with-docker) 