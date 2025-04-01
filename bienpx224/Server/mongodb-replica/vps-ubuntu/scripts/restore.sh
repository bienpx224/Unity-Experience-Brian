#!/bin/bash

# Script khôi phục dữ liệu MongoDB từ bản backup

# Lấy đường dẫn hiện tại của script
SCRIPT_DIR="$(dirname "$(readlink -f "$0")")"
ROOT_DIR="$(dirname "$SCRIPT_DIR")"
BACKUP_DIR="$ROOT_DIR/backup"

# Kiểm tra tham số đầu vào
if [ -z "$1" ]; then
  echo "❌ Thiếu tham số: Vui lòng cung cấp tên thư mục backup cần khôi phục"
  echo "📋 Cách sử dụng: $0 <tên_thư_mục_backup>"
  echo "📋 Ví dụ: $0 mongo-backup-20230401-120000"
  
  echo "📊 Danh sách backup hiện có:"
  ls -lt "$BACKUP_DIR" | grep "mongo-backup-"
  exit 1
fi

# Cấu hình
BACKUP_NAME="$1"
MONGO_URI="mongodb://remote_user:3MCr09mTRq9NOWdhdwgf@localhost:27017,localhost:27018,localhost:27019/?replicaSet=rs0&authSource=admin"

# Kiểm tra sự tồn tại của backup
if [ ! -d "$BACKUP_DIR/$BACKUP_NAME" ]; then
  echo "❌ Không tìm thấy backup: $BACKUP_DIR/$BACKUP_NAME"
  echo "📊 Danh sách backup hiện có:"
  ls -lt "$BACKUP_DIR" | grep "mongo-backup-"
  exit 1
fi

echo "⚠️ CẢNH BÁO: Quá trình khôi phục sẽ ghi đè lên dữ liệu hiện tại!"
echo "📦 Bạn đang chuẩn bị khôi phục từ: $BACKUP_NAME"
read -p "Bạn có chắc chắn muốn tiếp tục? (y/n): " CONFIRM

if [ "$CONFIRM" != "y" ]; then
  echo "🛑 Đã hủy quá trình khôi phục."
  exit 0
fi

echo "🚀 Đang khôi phục dữ liệu từ $BACKUP_NAME..."

# Thực hiện khôi phục
docker run --rm --network host \
  -v "$BACKUP_DIR:/backup" \
  mongo:latest \
  mongorestore --uri="$MONGO_URI" --dir="/backup/$BACKUP_NAME" --gzip

# Kiểm tra kết quả
if [ $? -eq 0 ]; then
  echo "✅ Khôi phục dữ liệu thành công!"
else
  echo "❌ Khôi phục dữ liệu thất bại! Vui lòng kiểm tra lỗi."
fi 