// Script khởi tạo Replica Set MongoDB
print("Bắt đầu khởi tạo Replica Set...");

// Cấu hình replica set
var config = {
    _id: "rs0",
    members: [
        { _id: 0, host: "mongo1:27017", priority: 2 },
        { _id: 1, host: "mongo2:27017", priority: 1 },
        { _id: 2, host: "mongo3:27017", priority: 1 }
    ]
};

// Khởi tạo replica set
rs.initiate(config);

// Đợi để replica set được khởi tạo
sleep(2000);

// Kiểm tra trạng thái replica set
rs.status();

// Đợi cho đến khi primary node được bầu
var isPrimary = false;
var attempts = 0;
var maxAttempts = 30;

while (!isPrimary && attempts < maxAttempts) {
    attempts++;
    print("Đang kiểm tra primary node lần " + attempts + "...");

    var status = rs.status();
    if (status.members) {
        var primaryMember = status.members.find(member => member.state === 1);
        if (primaryMember) {
            isPrimary = true;
            print("Node primary đã được bầu: " + primaryMember.name);
        }
    }

    if (!isPrimary) {
        print("Chưa có primary node, đợi thêm...");
        sleep(1000);
    }
}

// Tạo user admin
print("Đang tạo user admin...");
db = db.getSiblingDB("admin");

// Kiểm tra xem user đã tồn tại chưa trước khi tạo
var adminUser = db.getUser("bienpx");
if (!adminUser) {
    db.createUser({
        user: "bienpx",
        pwd: "3MCr09mTRq9NOWdhdwgf",
        roles: [{ role: "root", db: "admin" }]
    });
    print("Đã tạo user admin thành công");
} else {
    print("User admin đã tồn tại, bỏ qua bước tạo user");
}

// Tạo user cho kết nối từ xa
print("Đang tạo user cho kết nối từ xa...");
var remoteUser = db.getUser("remote_user");
if (!remoteUser) {
    db.createUser({
        user: "remote_user",
        pwd: "3MCr09mTRq9NOWdhdwgf",
        roles: [{ role: "root", db: "admin" }]
    });
    print("Đã tạo user remote_user thành công");
} else {
    print("User remote_user đã tồn tại, bỏ qua bước tạo user");
}

// Hiển thị thông tin connection string
print("\nConnection String cho ứng dụng nội bộ:");
print("mongodb://bienpx:3MCr09mTRq9NOWdhdwgf@mongo1:27017,mongo2:27017,mongo3:27017/?replicaSet=rs0&authSource=admin");

print("\nConnection String cho ứng dụng bên ngoài (thay SERVER_IP bằng IP thực):");
print("mongodb://remote_user:3MCr09mTRq9NOWdhdwgf@SERVER_IP:27017,SERVER_IP:27018,SERVER_IP:27019/?replicaSet=rs0&authSource=admin");

print("\nKhởi tạo Replica Set hoàn tất!"); 