Tôi sẽ hướng dẫn bạn các bước chi tiết để triển khai selfhost n8n sử dụng Docker trên VPS Ubuntu 22.04.
Các bước triển khai n8n với Docker trên Ubuntu 22.04
1. Chuẩn bị hệ thống
bashCopy# Cập nhật hệ thống
sudo apt update
sudo apt upgrade -y

# Cài đặt các gói cần thiết
sudo apt install -y apt-transport-https ca-certificates curl software-properties-common
2. Cài đặt Docker
bashCopy# Thêm GPG key của Docker
curl -fsSL https://download.docker.com/linux/ubuntu/gpg | sudo gpg --dearmor -o /usr/share/keyrings/docker-archive-keyring.gpg

# Thêm repository của Docker
echo "deb [arch=$(dpkg --print-architecture) signed-by=/usr/share/keyrings/docker-archive-keyring.gpg] https://download.docker.com/linux/ubuntu $(lsb_release -cs) stable" | sudo tee /etc/apt/sources.list.d/docker.list > /dev/null

# Cập nhật lại danh sách gói
sudo apt update

# Cài đặt Docker
sudo apt install -y docker-ce docker-ce-cli containerd.io

# Kiểm tra Docker đã cài đặt thành công
sudo docker --version
3. Cài đặt Docker Compose
bashCopy# Tải Docker Compose
sudo curl -L "https://github.com/docker/compose/releases/latest/download/docker-compose-$(uname -s)-$(uname -m)" -o /usr/local/bin/docker-compose

# Phân quyền thực thi
sudo chmod +x /usr/local/bin/docker-compose

# Kiểm tra Docker Compose đã cài đặt thành công
docker-compose --version
4. Tạo thư mục và file Docker Compose cho n8n
bashCopy# Tạo thư mục cho n8n
mkdir -p ~/n8n
cd ~/n8n

# Tạo file docker-compose.yml
nano docker-compose.yml
Dán nội dung sau vào file docker-compose.yml:
yamlCopyversion: '3'

services:
  n8n:
    image: n8nio/n8n
    restart: always
    ports:
      - "5678:5678"
    environment:
      - N8N_BASIC_AUTH_ACTIVE=true
      - N8N_BASIC_AUTH_USER=admin
      - N8N_BASIC_AUTH_PASSWORD=password
      - NODE_ENV=production
      - N8N_ENCRYPTION_KEY=your-secure-encryption-key-here
      - WEBHOOK_URL=https://your-domain.com
    volumes:
      - ~/.n8n:/home/node/.n8n
Lưu ý: Hãy thay đổi các giá trị sau:

admin và password: Thông tin đăng nhập của bạn
your-secure-encryption-key-here: Một chuỗi bất kỳ làm khóa mã hóa
https://your-domain.com: URL của bạn nếu bạn đã có domain

5. Khởi chạy n8n
bashCopy# Khởi chạy n8n
docker-compose up -d

# Kiểm tra trạng thái
docker-compose ps
6. Thiết lập Nginx làm reverse proxy (tùy chọn, nếu bạn muốn sử dụng domain)
bashCopy# Cài đặt Nginx
sudo apt install -y nginx

# Tạo file cấu hình cho site
sudo nano /etc/nginx/sites-available/n8n
Dán nội dung sau:
nginxCopyserver {
    listen 80;
    server_name your-domain.com;

    location / {
        proxy_pass http://localhost:5678;
        proxy_set_header Host $host;
        proxy_set_header X-Real-IP $remote_addr;
        proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header X-Forwarded-Proto $scheme;
    }
}
Thay your-domain.com bằng tên miền của bạn.
bashCopy# Kích hoạt site
sudo ln -s /etc/nginx/sites-available/n8n /etc/nginx/sites-enabled/

# Kiểm tra cấu hình Nginx
sudo nginx -t

# Khởi động lại Nginx
sudo systemctl restart nginx
7. Cài đặt SSL với Certbot (tùy chọn)
bashCopy# Cài đặt Certbot
sudo apt install -y certbot python3-certbot-nginx

# Lấy chứng chỉ SSL
sudo certbot --nginx -d your-domain.com
8. Truy cập n8n
Sau khi hoàn tất, bạn có thể truy cập n8n qua:

URL: http://your-ip:5678 (nếu không sử dụng domain)
Hoặc: https://your-domain.com (nếu đã cấu hình domain và SSL)

Đăng nhập với thông tin bạn đã cấu hình trong docker-compose.yml



## Fix lỗi docker-compose lỗi : 

- Cấp quyền trực tiếp cho thư mục:
sudo chown -R 1000:1000 ~/.n8n

- Dừng container hiện tại : 
docker-compose down

- Khởi động lại container : 
docker-compose up -d

- Kiểm tra logs container : 
docker-compose logs -f