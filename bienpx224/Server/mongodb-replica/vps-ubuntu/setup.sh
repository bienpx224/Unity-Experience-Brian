#!/bin/bash

echo "🚀 Bắt đầu thiết lập MongoDB Replica Set..."

# Kiểm tra các công cụ cần thiết
if ! command -v docker &> /dev/null; then
    echo "❌ Docker chưa được cài đặt. Vui lòng cài đặt Docker trước khi chạy script."
    exit 1
fi

if ! command -v docker-compose &> /dev/null; then
    echo "❌ Docker Compose chưa được cài đặt. Vui lòng cài đặt Docker Compose trước khi chạy script."
    exit 1
fi

# Tạo cấu trúc thư mục
echo "📁 Đang tạo cấu trúc thư mục..."
mkdir -p data/mongo{1,2,3} keyfile scripts

# Kiểm tra và đảm bảo các script JS có sẵn trong thư mục scripts
echo "📋 Đang kiểm tra các script..."
if [ ! -f "scripts/init-replica.js" ] || [ ! -f "scripts/auth-setup.js" ]; then
    echo "❌ Không tìm thấy file scripts. Đảm bảo rằng init-replica.js và auth-setup.js nằm trong thư mục scripts."
    exit 1
fi

# Tạo keyFile cho xác thực
echo "🔑 Đang tạo keyFile cho xác thực nội bộ..."
mkdir -p keyfile
openssl rand -base64 756 > keyfile/mongo-keyfile
chmod 400 keyfile/mongo-keyfile
chown 999:999 keyfile/mongo-keyfile 2>/dev/null || chmod 444 keyfile/mongo-keyfile

# Xóa các container cũ nếu có
echo "🧹 Đang dọn dẹp các container cũ..."
docker-compose down -v 2>/dev/null || true
docker rm -f mongo1 mongo2 mongo3 2>/dev/null || true

# Khởi động MongoDB không có xác thực
echo "🔄 Đang khởi động MongoDB (không có xác thực)..."
docker-compose up -d

# Đợi cho các container khởi động
echo "⏳ Đang đợi các container khởi động..."
sleep 20

# Kiểm tra xem các container đã sẵn sàng chưa
echo "🔍 Kiểm tra trạng thái các container..."
if [ "$(docker ps -q -f name=mongo1)" = "" ] || [ "$(docker ps -q -f name=mongo2)" = "" ] || [ "$(docker ps -q -f name=mongo3)" = "" ]; then
  echo "❌ Không tìm thấy ít nhất một container MongoDB đang chạy!"
  echo "📋 Kiểm tra log Docker: docker-compose logs"
  docker-compose logs
  exit 1
fi

# Kiểm tra kết nối
echo "🔍 Kiểm tra kết nối tới MongoDB..."
MAX_ATTEMPTS=30
ATTEMPTS=0
MONGO_READY=false

while [ $ATTEMPTS -lt $MAX_ATTEMPTS ] && [ "$MONGO_READY" = false ]; do
  ATTEMPTS=$((ATTEMPTS+1))
  echo "⏳ Kiểm tra kết nối lần $ATTEMPTS/$MAX_ATTEMPTS..."
  
  if docker exec mongo1 mongosh --quiet --eval "db.adminCommand('ping').ok" | grep -q "1"; then
    echo "✅ Kết nối thành công đến MongoDB!"
    MONGO_READY=true
  else
    echo "⏳ Chưa thể kết nối tới MongoDB, đợi thêm 5 giây..."
    sleep 5
  fi
done

if [ "$MONGO_READY" = false ]; then
  echo "❌ MongoDB không sẵn sàng sau $MAX_ATTEMPTS lần thử. Đang kiểm tra logs..."
  docker-compose logs
  exit 1
fi

# Khởi tạo Replica Set
echo "🔧 Đang khởi tạo Replica Set..."
docker exec mongo1 mongosh --file /scripts/init-replica.js
INIT_EXIT_CODE=$?

if [ $INIT_EXIT_CODE -ne 0 ]; then
  echo "❌ Lỗi khi khởi tạo Replica Set. Vui lòng kiểm tra logs."
  exit 1
fi

echo "✅ Replica Set đã được khởi tạo thành công!"

# Dừng các container để khởi động lại với xác thực
echo "🔄 Đang dừng các container để khởi động lại với xác thực..."
docker-compose down

# Khởi động lại với xác thực bằng keyFile
echo "🔄 Đang khởi động lại với xác thực keyFile..."
docker-compose -f docker-compose-secure.yml up -d

# Đợi các container khởi động lại
echo "⏳ Đang đợi các container khởi động lại..."
sleep 30

# Kiểm tra xác thực
echo "🔍 Kiểm tra kết nối với xác thực..."
docker exec mongo1 mongosh -u bienpx -p 3MCr09mTRq9NOWdhdwgf --authenticationDatabase admin --file /scripts/auth-setup.js

# Thiết lập tường lửa (UFW)
echo "🔒 Đang cấu hình tường lửa (UFW)..."

# Đảm bảo SSH port 22 được mở trước
sudo ufw allow 22/tcp comment 'SSH access'

# Cho phép truy cập từ localhost
sudo ufw allow from 127.0.0.1 to any port 27017 proto tcp
sudo ufw allow from 127.0.0.1 to any port 27018 proto tcp
sudo ufw allow from 127.0.0.1 to any port 27019 proto tcp

# Lấy địa chỉ IP của máy chủ
SERVER_IP=$(hostname -I | awk '{print $1}')

# Cho phép truy cập từ chính máy chủ
sudo ufw allow from $SERVER_IP to any port 27017 proto tcp
sudo ufw allow from $SERVER_IP to any port 27018 proto tcp
sudo ufw allow from $SERVER_IP to any port 27019 proto tcp

# Từ chối tất cả các kết nối khác đến MongoDB
sudo ufw deny 27017/tcp
sudo ufw deny 27018/tcp
sudo ufw deny 27019/tcp

# Kích hoạt UFW nếu chưa bật
if sudo ufw status | grep -q "Status: inactive"; then
  echo "🔒 Kích hoạt UFW..."
  echo "y" | sudo ufw enable
fi

echo "✨ Thiết lập MongoDB Replica Set hoàn tất!"
echo "📌 Thông tin kết nối:"
echo "🔸 Host: $SERVER_IP"
echo "🔸 Ports: 27017, 27018, 27019"
echo "🔸 Username: remote_user"
echo "🔸 Password: 3MCr09mTRq9NOWdhdwgf"
echo "🔸 Connection String: mongodb://remote_user:3MCr09mTRq9NOWdhdwgf@$SERVER_IP:27017,$SERVER_IP:27018,$SERVER_IP:27019/?replicaSet=rs0&authSource=admin"
echo "✨ Bạn có thể sử dụng MongoDB Compass với connection string trên để kết nối." 