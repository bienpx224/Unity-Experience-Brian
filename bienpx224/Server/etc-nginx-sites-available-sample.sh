## Bản ngắn gọn chỉ cần tạo sites-available lúc đầu như này 
server {
    server_name chatbot.zenai.ai.vn;
    location / {
        proxy_pass http://localhost:4301;
        proxy_http_version 1.1;
        proxy_set_header Upgrade $http_upgrade;
        proxy_set_header Connection 'upgrade';
        proxy_set_header Host $host;
        proxy_cache_bypass $http_upgrade;
        proxy_set_header X-Real-IP $remote_addr;
        proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header X-Forwarded-Proto $scheme;
    }
}

## Bản đầy đủ sau khi thêm ssl cert : 

server {
    server_name n8nn.gamingcenter.top;

    location / {
        proxy_pass http://localhost:5678;
        proxy_http_version 1.1;
        proxy_set_header Upgrade $http_upgrade;
        proxy_set_header Connection 'upgrade';
        proxy_set_header Host $host;
        proxy_cache_bypass $http_upgrade;
        proxy_set_header X-Real-IP $remote_addr;
        proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header X-Forwarded-Proto $scheme;
    }
    
    # listen 443 ssl; # managed by Certbot
    # ssl_certificate /etc/letsencrypt/live/n8nn.gamingcenter.top/fullchain.pem; #>
    # ssl_certificate_key /etc/letsencrypt/live/n8nn.gamingcenter.top/privkey.pem;>
    # include /etc/letsencrypt/options-ssl-nginx.conf; # managed by Certbot
    # ssl_dhparam /etc/letsencrypt/ssl-dhparams.pem; # managed by Certbot

}
server {
    if ($host = n8nn.gamingcenter.top) {
        return 301 https://$host$request_uri;
    } # managed by Certbot


    listen 80;
    server_name n8nn.gamingcenter.top;
    return 404; # managed by Certbot


}


