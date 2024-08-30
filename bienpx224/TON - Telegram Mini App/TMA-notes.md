

## Xác thực định danh user : 
- Khi user mở Mini app, trong React lấy được thông tin initData, userId, mã hash,... 
- Để xác thực định danh user đó có đúng không thì thực hiện 
mã hoá sha256 + bottoken + mã hash 
Chi tiết bổ sung sau. Đại loại là kết hợp như thế.

- Code example : 
```javascript
const parsedData = querystring.parse(tgWebAppData);
const receivedHash = parsedData['hash'] as string;
delete parsedData['hash'];

const sortedParams = Object.keys(parsedData)
  .sort()
  .map((key) => `${key}=${parsedData[key]}`)
  .join('\n');

const secretKey = crypto
  .createHmac('sha256', 'WebAppData')
  .update(telegramConfig().telegramBotGameToken)
  .digest();
const hmac = crypto
  .createHmac('sha256', secretKey)
  .update(sortedParams.toString())
  .digest('hex');
if (hmac === receivedHash) {
  return {
    user: JSON.parse(parsedData['user'].toString()) as IUser,
    referralId: parsedData['start_param']
      ? (parsedData['start_param'] as string)
      : undefined,
  };
} else {
  return null;
}
```