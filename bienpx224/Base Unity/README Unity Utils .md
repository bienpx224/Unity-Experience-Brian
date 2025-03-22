# UNITY UTILS 

## Test Responsive Screen tren Unity : 
- Vào Project Settings > Package Manager > Tích chọn Enable Preview Packages . 
- Giờ thì vào Package Manager > Unity Registry > Tìm kiếm : device simulator 
- Install device simulator vào để test hiển thị trên các device. đặc biệt các device tai thỏ. 
- Vào Window > Gereral > Device Simulator là có thể xem responsive dc ròi. 

## Convert number float trên IOS bị sai khác so với các nền tảng khác : 
- Khi sử dụng float.tryParse thì cần truyền thêm option 
VD như sau : 
```c#
n = float.Parse(str, CultureInfo.InvariantCulture);
```