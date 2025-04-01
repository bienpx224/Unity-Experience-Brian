// Script thiết lập xác thực cho MongoDB Replica Set
print("Đang cấu hình xác thực cho MongoDB Replica Set...");

// Đăng nhập với tài khoản admin
db = db.getSiblingDB("admin");

// Hiển thị danh sách users hiện tại
print("Danh sách users hiện tại:");
db.getUsers();

// Kiểm tra trạng thái replica set
print("Trạng thái Replica Set:");
rs.status();

// Hiển thị thông tin về kết nối
print("\nConnection String với xác thực cho ứng dụng nội bộ:");
print("mongodb://bienpx:3MCr09mTRq9NOWdhdwgf@mongo1:27017,mongo2:27017,mongo3:27017/?replicaSet=rs0&authSource=admin");

print("\nConnection String với xác thực cho ứng dụng bên ngoài (thay SERVER_IP bằng IP thực):");
print("mongodb://remote_user:3MCr09mTRq9NOWdhdwgf@SERVER_IP:27017,SERVER_IP:27018,SERVER_IP:27019/?replicaSet=rs0&authSource=admin");

print("\nThiết lập xác thực hoàn tất!"); 