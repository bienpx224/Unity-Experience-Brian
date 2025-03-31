#!/bin/bash

echo "==> Thử kết nối tới MongoDB Replica Set..."

# Test kết nối không xác thực
echo "1. Thử kết nối không xác thực..."
if mongosh --eval "db.runCommand({ping:1})" localhost:27017; then
  echo "Kết nối không xác thực thành công!"
else
  echo "Kết nối không xác thực thất bại (dự kiến nếu đã kích hoạt xác thực)."
fi

# Test kết nối với xác thực admin
echo -e "\n2. Thử kết nối với xác thực admin trên 1 node..."
if mongosh "mongodb://admin:abcd1234@localhost:27017/?authSource=admin" --eval "db.runCommand({ping:1})"; then
  echo "Kết nối có xác thực thành công!"
else
  echo "Kết nối có xác thực thất bại."
fi

# Test kết nối replica set
echo -e "\n3. Thử kết nối replica set..."
if mongosh "mongodb://admin:abcd1234@localhost:27017,localhost:27018,localhost:27019/?replicaSet=rs0&authSource=admin" --eval "rs.status()"; then
  echo "Kết nối replica set thành công!"
else
  echo "Kết nối replica set thất bại."
fi

echo -e "\nNếu có lỗi, hãy thực hiện các bước sau:"
echo "1. Kiểm tra xem các container có đang chạy không (docker ps)"
echo "2. Kiểm tra replica set đã được khởi tạo chưa (./init-replica.sh)" 