## Ngắn gọn hơn : 
- Thực hiện theo các lệnh sau : 

nano /etc/nginx/sites-available/stock.zenai.ai.vn

- Coppy nội dung file etc-nginx-sites-available-sample.sh vào file trên. 

sudo ln -s /etc/nginx/sites-available/stock.zenai.ai.vn /etc/nginx/sites-enabled/

sudo systemctl reload nginx

sudo certbot --nginx -d stock.zenai.ai.vn

- Sau khi thực hiện lệnh kia xong thì mặc định trong file site-enabled đã có link tới file site-available sẽ tự có thêm ssl và server config . 

sudo nginx -t
sudo systemctl restart nginx


## Đầy đủ ban đầu : 
cp /etc/nginx/sites-available/game.gamingcenter.top /etc/nginx/sites-available/chatbot.api.zenai.ai.vn

nano /etc/nginx/sites-available/chatbot.zenai.ai.vn

sudo ln -s /etc/nginx/sites-available/chatbot.zenai.ai.vn /etc/nginx/sites-enabled/

sudo systemctl reload nginx

sudo certbot --nginx -d chatbot.zenai.ai.vn

nano /etc/nginx/sites-available/chatbot.zenai.ai.vn

ls -l /etc/nginx/sites-enabled/

sudo nginx -t
sudo systemctl reload nginx
sudo systemctl restart nginx
sudo systemctl status nginx