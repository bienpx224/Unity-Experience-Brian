#!/bin/bash

echo "==> Thử kết nối tới MongoDB Replica Set..."

# Kiểm tra xem docker đang chạy không
if ! command -v docker &> /dev/null; then
  echo "Lỗi: Docker không được cài đặt hoặc không hoạt động."
  exit 1
fi

# Kiểm tra xem các container có đang chạy không
if ! docker ps | grep -q "mongo1"; then
  echo "Lỗi: Không tìm thấy container mongo1. Vui lòng chạy docker-compose up -d trước."
  exit 1
fi

echo -e "\n=== PHẦN 1: TEST TỪ BÊN TRONG CONTAINER ==="

# Test kết nối không xác thực từ container
echo "1. Thử kết nối không xác thực từ container..."
if docker exec -it mongo1 mongosh --eval "db.runCommand({ping:1})"; then
  echo "Kết nối không xác thực thành công!"
else
  echo "Kết nối không xác thực thất bại (dự kiến nếu đã kích hoạt xác thực)."
fi

# Test kết nối với xác thực admin từ container
echo -e "\n2. Thử kết nối với xác thực admin từ container..."
if docker exec -it mongo1 mongosh "mongodb://admin:abcd1234@localhost:27017/?authSource=admin" --eval "db.runCommand({ping:1})"; then
  echo "Kết nối có xác thực từ container thành công!"
else
  echo "Kết nối có xác thực từ container thất bại."
fi

# Test kết nối replica set từ container (sử dụng tên container trong mạng Docker)
echo -e "\n3. Thử kết nối replica set từ container..."
if docker exec -it mongo1 mongosh "mongodb://admin:abcd1234@mongo1:27017,mongo2:27017,mongo3:27017/?replicaSet=rs0&authSource=admin" --eval "rs.status()"; then
  echo "Kết nối replica set từ container thành công!"
else
  echo "Kết nối replica set từ container thất bại."
fi

echo -e "\n=== PHẦN 2: TEST TỪ HOST (Yêu cầu mongosh được cài đặt) ==="

# Kiểm tra xem mongosh có được cài đặt không
if command -v mongosh &> /dev/null; then
  # Test kết nối replica set từ host
  echo -e "4. Thử kết nối replica set từ host..."
  if mongosh "mongodb://admin:abcd1234@localhost:27017,localhost:27018,localhost:27019/?replicaSet=rs0&authSource=admin" --eval "rs.status()"; then
    echo "Kết nối replica set từ host thành công!"
  else
    echo "Kết nối replica set từ host thất bại."
  fi
else
  echo "mongosh không được cài đặt trên host. Bỏ qua test từ host."
  echo "Để cài đặt mongosh, chạy các lệnh sau:"
  echo "  wget -qO - https://www.mongodb.org/static/pgp/server-7.0.asc | sudo apt-key add -"
  echo "  echo \"deb [ arch=amd64,arm64 ] https://repo.mongodb.org/apt/ubuntu \$(lsb_release -cs)/mongodb-org/7.0 multiverse\" | sudo tee /etc/apt/sources.list.d/mongodb-org-7.0.list"
  echo "  sudo apt update"
  echo "  sudo apt install -y mongodb-mongosh"
fi

echo -e "\n=== THÔNG TIN KẾT NỐI CHO ỨNG DỤNG ==="
echo "Connection String cho MongoDB Compass hoặc ứng dụng từ bên ngoài:"
echo "mongodb://admin:abcd1234@194.62.166.157:27017,194.62.166.157:27018,194.62.166.157:27019/?replicaSet=rs0&authSource=admin"

echo -e "\nNếu có lỗi, hãy thực hiện các bước sau:"
echo "1. Kiểm tra xem các container có đang chạy không (docker ps)"
echo "2. Kiểm tra replica set đã được khởi tạo chưa (./init-replica.sh)"
echo "3. Kiểm tra firewall đã mở các port 27017, 27018, 27019 chưa (sudo ufw status)" 