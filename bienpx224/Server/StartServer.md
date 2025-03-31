

## Khởi tạo mua VPS Server trên Vietnix : 
- SSH vào VPS 
- VPS mới mua về sẽ trống rỗng, không có gì, chưa cài đặt gì. 
### Cần cài đặt nodejs và các lib cơ bản: 
- Cập nhật lại các package trên VPS : 
```sudo apt update```
```sudo apt upgrade -y```
- Thêm NodeSource repository
```curl -fsSL https://deb.nodesource.com/setup_22.x | sudo -E bash -```
- Cài đặt Node.js
```sudo apt install -y nodejs```
- Kiểm tra phiên bản
```node --version```
```npm --version```
- Nếu nodejs version chưa hiện đúng 22 thì thực hiện cài thông qua nvm : 
```curl -o- https://raw.githubusercontent.com/nvm-sh/nvm/v0.39.7/install.sh | bash```

```export NVM_DIR="$HOME/.nvm"```
```[ -s "$NVM_DIR/nvm.sh" ] && \. "$NVM_DIR/nvm.sh"```

```nvm install 20```

```nvm alias default 20```

```node -v```
- Cài đặt PM2 để quản lý process : 
```sudo npm install -g pm2```
- Cấu hình PM2 tự động khởi động cùng hệ thống:
```pm2 startup```

### Cài đặt Nginx : 

```sh
sudo apt upgrade -y

sudo apt install nginx -y

# Nếu bạn đang sử dụng UFW (Uncomplicated Firewall), hãy cho phép lưu lượng HTTP và HTTPS:
sudo ufw allow 'Nginx HTTP'
sudo ufw allow 'Nginx HTTPS'

# Kiểm tra trạng thái UFW:
sudo ufw status

# Sau khi cài đặt, Nginx sẽ tự động bắt đầu. Kiểm tra trạng thái:
sudo systemctl status nginx

# Kiểm tra cấu hình lỗi không : 
sudo nginx -t

# Khởi động nginx : 
sudo systemctl start nginx


```

### Cài đặt Certbot : 
```sh
# Cài đặt Certbot : 
sudo apt install certbot python3-certbot-nginx -y
```


### TH Lỗi vào server luôn bị chuyển về node v12, dù đã cài và chọn v22 trước đó : 
- Thường server ubuntu của ZenAI hiện tại khi ssh vào thường bị node version là 12. Cần chuyển sang v22. 
- Khởi động NVM
source ~/.bashrc
- Chuyển sang sử dụng Node.js v22
nvm use 22


## Hướng dẫn chạy dự án trên port mới : 
- Clone dự án về, npm i, điều chỉnh để dự án sẵn sàng chạy. 
- Chỉnh để dự án chạy ở port xxxx : Nếu pm2 thì chỉnh trong ecosystem hoặc file config pm2.. 
- Sau khi chỉnh xong, thực hiện tiến hành cho dự án chạy trên port đó như bình thường. 
- Thực hiện mở port bằng UFW : 
```sh
## Port ssh rất quan trọng cần mở đầu tiên trước khi enable firewall, nếu không sẽ không ssh vào dc. 
sudo ufw allow 22 
# Kích hoạt Firewall : 
sudo ufw enable

# Mở các port cần sử dụng trong hệ thống

sudo ufw allow 27017/tcp
sudo ufw allow 4xxx/tcp

# Kiểm tra firewall và các port đang mở : 
sudo ufw status
```

## Trỏ domain mới vào nginx cho dự án chạy trên Server : 
- Giả sử đã chạy được dự án đó trên Server ở port 4001 bằng pm2 
- Giả sử trước đó đã có config cho domain khác trỏ vào tương tự : VD game.gamingcenter.top
- Các bước cần thực hiện tiếp là : 
- Kiểm tra xem site đang được enabled : ```ls -l /etc/nginx/sites-enabled/```
- Coppy ND file nginx domain đã có sang domain mới : 
```cp /etc/nginx/sites-available/game.gamingcenter.top /etc/nginx/sites-available/wave.gamingcenter.top```
- Thực hiện sửa nội dung trong file mới đó : Port, domain : ```nano /etc/nginx/sites-available/wave.gamingcenter.top```
Khi sửa lưu ý comment đoạn cert ssl lại. Tý mở ra sau.
- Thực hiện enable site vừa tạo mới đó : 
```sudo ln -s /etc/nginx/sites-available/wave.gamingcenter.top /etc/nginx/sites-enabled/```
- Thực hiện ```sudo nginx -t``` sau đó reload lại nginx : ```sudo systemctl reload nginx```
- Thực hiện tạo ssl cert key/pem để config https cho domain mới đó : 
```sudo certbot --nginx -d wave.gamingcenter.top```
- Sau đó quay lại file site-available để mở comment config ssl ra.
```nano /etc/nginx/sites-available/wave.gamingcenter.top```
- Thực hiện kiểm tra nginx lỗi ko, reload, khởi động lại nginx : 
```sudo nginx -t```  
```sudo systemctl reload nginx```
```sudo systemctl restart nginx```
- Kiểm tra trạng thái nginx : ```sudo systemctl status nginx```
- Kiểm tra lại các site enable xem đã có domain mới chưa : ```ls -l /etc/nginx/sites-enabled/```

## Thủ tục cài đặt Docker trên vps ubuntu : 

### 1. Cập nhật hệ thống
### 2. Cài đặt các package cần thiết
```bash
# Cài các package để cho phép apt sử dụng HTTPS
sudo apt install -y ca-certificates curl gnupg lsb-release
```

### 3. Thêm GPG key của Docker
```bash
# Tạo thư mục cho keyrings
sudo mkdir -p /etc/apt/keyrings

# Tải và thêm Docker GPG key
curl -fsSL https://download.docker.com/linux/ubuntu/gpg | sudo gpg --dearmor -o /etc/apt/keyrings/docker.gpg

# Cấp quyền đọc key
sudo chmod a+r /etc/apt/keyrings/docker.gpg
```

### 4. Thiết lập Docker repository
```bash
# Thêm Docker repository vào apt sources
echo "deb [arch=$(dpkg --print-architecture) signed-by=/etc/apt/keyrings/docker.gpg] https://download.docker.com/linux/ubuntu $(lsb_release -cs) stable" | sudo tee /etc/apt/sources.list.d/docker.list > /dev/null
```

### 5. Cài đặt Docker Engine
```bash
# Cập nhật apt package index với repository mới
sudo apt update

# Cài đặt Docker Engine, CLI, containerd, và Docker Compose
sudo apt install -y docker-ce docker-ce-cli containerd.io docker-compose-plugin docker-compose
```

### 6. Thiết lập để sử dụng Docker không cần sudo
```bash
# Thêm user hiện tại vào group docker
sudo usermod -aG docker $USER

# Áp dụng thay đổi group (hoặc đăng xuất và đăng nhập lại)
newgrp docker
```

### 7. Kiểm tra cài đặt
```bash
# Kiểm tra phiên bản Docker
docker --version

# Chạy container hello-world để xác nhận Docker hoạt động đúng
docker run hello-world

# Kiểm tra Docker Compose
docker-compose --version
```

### 8. Cấu hình Docker để start khi khởi động
```bash
# Đảm bảo Docker service khởi động cùng hệ thống
sudo systemctl enable docker
```

### 9. Cấu hình cơ bản
```bash
# Tạo thư mục cho docker configurations
mkdir -p ~/.docker

# Tạo file config.json cơ bản (tùy chọn)
echo '{ "experimental": false }' > ~/.docker/config.json
```

### 10. Xử lý các vấn đề phổ biến
```bash
# Nếu gặp lỗi "permission denied", thử restart Docker
sudo systemctl restart docker

# Kiểm tra trạng thái Docker
sudo systemctl status docker
```

## Thủ tục tạo Database MongoDB mới : 
- Cài MongoDB vào server Ubuntu : 
```sh
# Thêm user hiện tại vào group docker (không cần sudo khi chạy docker)
sudo usermod -aG docker $USER

# Tạo thư mục để persistent data
mkdir -p ~/mongodb/data

# Tạo file docker-compose.yml, nội dung file ở bên dưới
nano ~/mongodb/docker-compose.yml

# Điền nội dung file docker-compose.yml ở bên dưới vào rồi lưu lại 
```
- Nội dung file docker-compose.yml : 
```yaml 
version: '3.8'

services:
  mongodb:
    image: mongo:latest
    container_name: mongodb
    restart: always
    environment:
      MONGO_INITDB_ROOT_USERNAME: admin           # Thay đổi username
      MONGO_INITDB_ROOT_PASSWORD: abcd1234   # Thay đổi password
    ports:
      - "27017:27017"
    volumes:
      - ./data:/data/db
    networks:
      - mongo-network

  # (Tùy chọn) MongoDB Express - GUI để quản lý MongoDB
  # mongo-express:
  #   image: mongo-express:latest
  #   container_name: mongo-express
  #   restart: always
  #   environment:
  #     ME_CONFIG_MONGODB_ADMINUSERNAME: admin          # Phải giống username ở trên
  #     ME_CONFIG_MONGODB_ADMINPASSWORD: abcd1234  # Phải giống password ở trên
  #     ME_CONFIG_MONGODB_SERVER: mongodb
  #     ME_CONFIG_BASICAUTH_USERNAME: admin             # Username để login vào GUI
  #     ME_CONFIG_BASICAUTH_PASSWORD: abcd1234  # Password để login vào GUI
  #   ports:
  #     - "8081:8081"
  #   networks:
  #     - mongo-network
  #   depends_on:
  #     - mongodb

networks:
  mongo-network:
    driver: bridge
```

```sh
# CD vào folder /mongodb đã tạo file bên trên. Khởi chạy mongo
cd ~/mongodb
docker compose up -d 
```

- Cú pháp Connection String để vào Mongo Compass : 
```sh
# mongo compass 
mongodb://[user]:[pass]@[ip]:27017/?authSource=admin

# mongo connect nestjs 
MONGODB_URI=mongodb://admin:pass@127.0.0.1:27017/tên_database?authSource=admin
```

