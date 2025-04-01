#!/bin/bash

# Script thiết lập cron job cho backup MongoDB tự động

# Lấy đường dẫn hiện tại của script
SCRIPT_DIR="$(dirname "$(readlink -f "$0")")/scripts"
ROOT_DIR="$(dirname "$(readlink -f "$0")")"
BACKUP_DIR="$ROOT_DIR/backup"

# Đảm bảo thư mục backup tồn tại
mkdir -p "$BACKUP_DIR"

# Đảm bảo quyền thực thi cho các script backup
chmod +x "$SCRIPT_DIR/backup.sh"
chmod +x "$SCRIPT_DIR/restore.sh"

echo "🔧 Đang thiết lập cronjob backup tự động..."

# Tạo cron job chạy lúc 1 giờ sáng mỗi ngày
CRON_JOB="0 1 * * * $SCRIPT_DIR/backup.sh >> $BACKUP_DIR/backup.log 2>&1"

# Kiểm tra nếu cron job đã tồn tại
if crontab -l 2>/dev/null | grep -q "$SCRIPT_DIR/backup.sh"; then
  echo "⚠️ Cron job đã tồn tại. Không thực hiện thay đổi."
else
  # Thêm cron job mới
  (crontab -l 2>/dev/null; echo "$CRON_JOB") | crontab -
  
  if [ $? -eq 0 ]; then
    echo "✅ Cron job đã được thiết lập thành công!"
    echo "📋 MongoDB sẽ được backup tự động vào lúc 1:00 AM mỗi ngày."
    echo "📋 Log backup sẽ được lưu tại: $BACKUP_DIR/backup.log"
  else
    echo "❌ Thiết lập cron job thất bại. Vui lòng kiểm tra lỗi."
  fi
fi

# Hiển thị danh sách cron job hiện tại
echo "📊 Danh sách cron job hiện tại:"
crontab -l

echo "
✨ Thông tin hữu ích:
1. Xem log backup:        tail -f $BACKUP_DIR/backup.log
2. Chạy backup thủ công:  $SCRIPT_DIR/backup.sh
3. Khôi phục backup:      $SCRIPT_DIR/restore.sh <tên_thư_mục_backup>
4. Sửa lịch backup:       crontab -e
" 