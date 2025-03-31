#!/bin/bash

# Dừng và xóa container nếu đã tồn tại
echo "==> Dừng và xóa container cũ (nếu có)..."
docker-compose down

# Khởi động các container
echo "==> Khởi động MongoDB replica set..."
docker-compose up -d

# Đợi các container khởi động
echo "==> Đợi các container khởi động hoàn tất..."
sleep 10

# Khởi tạo replica set
echo "==> Khởi tạo replica set và tạo user admin..."
docker exec -it mongo1 mongosh --file /scripts/setup-mongo.js

# Kiểm tra trạng thái replica set
echo "==> Kiểm tra trạng thái replica set..."
docker exec -it mongo1 mongosh --eval "rs.status()"

echo "==> MongoDB replica set đã sẵn sàng!"
echo "==> Kết nối với URI: mongodb://admin:abcd1234@localhost:27017,localhost:27018,localhost:27019/?replicaSet=rs0&authSource=admin" 