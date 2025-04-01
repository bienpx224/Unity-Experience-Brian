#!/bin/bash

# Script tự động backup MongoDB Replica Set
# Lưu trữ 7 ngày gần nhất

# Lấy đường dẫn hiện tại của script
SCRIPT_DIR="$(dirname "$(readlink -f "$0")")"
ROOT_DIR="$(dirname "$SCRIPT_DIR")"

# Cấu hình
BACKUP_DIR="$ROOT_DIR/backup"
TIMESTAMP=$(date +"%Y%m%d-%H%M%S")
BACKUP_NAME="mongo-backup-$TIMESTAMP"
MONGO_URI="mongodb://remote_user:3MCr09mTRq9NOWdhdwgf@localhost:27017,localhost:27018,localhost:27019/?replicaSet=rs0&authSource=admin"
RETENTION_DAYS=7

# Đảm bảo thư mục backup tồn tại
mkdir -p "$BACKUP_DIR"

echo "🚀 Bắt đầu backup MongoDB vào $(date)"

# Thực hiện backup sử dụng mongodump
docker run --rm --network host \
  -v "$BACKUP_DIR:/backup" \
  mongo:latest \
  mongodump --uri="$MONGO_URI" --out="/backup/$BACKUP_NAME" --gzip

# Kiểm tra kết quả
if [ $? -eq 0 ]; then
  echo "✅ Backup thành công: $BACKUP_DIR/$BACKUP_NAME"
  
  # Tạo file chỉ báo thời gian backup thành công
  echo "Backup completed at $(date)" > "$BACKUP_DIR/$BACKUP_NAME/backup_info.txt"
  
  # Xóa các backup cũ (giữ lại 7 ngày gần nhất)
  echo "🧹 Đang xóa các backup cũ..."
  find "$BACKUP_DIR" -maxdepth 1 -name "mongo-backup-*" -type d -mtime +$RETENTION_DAYS -exec rm -rf {} \;
  
  echo "📊 Danh sách backup hiện tại (từ mới đến cũ):"
  ls -lt "$BACKUP_DIR" | grep "mongo-backup-" | head -n 10
  
  echo "✨ Quá trình backup hoàn tất!"
else
  echo "❌ Backup thất bại! Vui lòng kiểm tra lỗi."
fi 