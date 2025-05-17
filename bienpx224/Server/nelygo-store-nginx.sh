server {
    server_name nelygo.store;

    location / {
        proxy_pass http://localhost:4413;
        proxy_http_version 1.1;
        proxy_set_header Upgrade $http_upgrade;
        proxy_set_header Connection 'upgrade';
        proxy_set_header Host $host;
        proxy_cache_bypass $http_upgrade;
        proxy_set_header X-Real-IP $remote_addr;
        proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header X-Forwarded-Proto $scheme;
    }
    
    listen 443 ssl; # managed by Certbot
    ssl_certificate /etc/letsencrypt/live/nelygo.store/fullchain.pem;
    ssl_certificate_key /etc/letsencrypt/live/nelygo.store/privkey.pem;
    include /etc/letsencrypt/options-ssl-nginx.conf; # managed by Certbot
    ssl_dhparam /etc/letsencrypt/ssl-dhparams.pem; # managed by Certbot

}
server {
    if ($host = nelygo.store) {
        return 301 https://$host$request_uri;
    } # managed by Certbot


    listen 80;
    server_name nelygo.store;
    return 404; # managed by Certbot


}

sudo ln -s /etc/nginx/sites-available/nelygo.store /etc/nginx/sites-enabled/

nano /etc/nginx/sites-available/nelygo.store

ls -l /etc/nginx/sites-enabled/

sudo ln -s /etc/nginx/sites-available/nelygo.store /etc/nginx/sites-enabled/

sudo certbot --nginx -d nelygo.store


# Seting cho api.nelygo.store : 

nano /etc/nginx/sites-available/api.nelygo.store

server {
    server_name api.nelygo.store;
    location / {
        proxy_pass http://localhost:4412;
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

sudo ln -s /etc/nginx/sites-available/api.nelygo.store /etc/nginx/sites-enabled/

sudo systemctl reload nginx

sudo nginx -t

sudo certbot --nginx -d api.nelygo.store

sudo nginx -t
sudo systemctl restart nginx

nano /etc/nginx/sites-available/api.nelygo.store
nano /etc/nginx/sites-enabled/api.nelygo.store

