// Cấu hình IP của VPS - chỉ cần thay đổi ở đây một lần
const VPS_IP = "194.62.166.157"; // Thay đổi IP này thành IP của VPS bạn

// Khởi tạo replica set nếu chưa được khởi tạo
try {
    rs.status();
} catch (err) {
    print("Khởi tạo replica set...");

    rs.initiate({
        _id: "rs0",
        members: [
            // Cấu hình sử dụng IP:PORT cho các kết nối bên ngoài
            { _id: 0, host: "mongo1:27017", priority: 2 },
            { _id: 1, host: "mongo2:27017", priority: 1 },
            { _id: 2, host: "mongo3:27017", priority: 1 }
        ]
    });
}

// Đợi node trở thành PRIMARY
print("Đợi node trở thành PRIMARY...");
let isPrimary = false;
let maxAttempts = 30;
let attempts = 0;

while (!isPrimary && attempts < maxAttempts) {
    let status = rs.status();
    for (let i = 0; i < status.members.length; i++) {
        if (status.members[i].self && status.members[i].state === 1) {
            isPrimary = true;
            break;
        }
    }

    if (!isPrimary) {
        print("Vẫn đang chờ node trở thành PRIMARY... (" + (attempts + 1) + "/" + maxAttempts + ")");
        sleep(2000);
        attempts++;
    }
}

if (!isPrimary) {
    throw new Error("Không thể trở thành PRIMARY sau " + maxAttempts + " lần thử.");
}

print("Node đã trở thành PRIMARY, tiếp tục tạo user...");

// Chuyển sang database admin để tạo user
db = db.getSiblingDB('admin');

// Tạo user admin với quyền root
try {
    db.createUser({
        user: "admin",
        pwd: "abcd1234",
        roles: [{ role: "root", db: "admin" }]
    });
    print("User admin đã được tạo thành công!");
} catch (err) {
    print("Lỗi khi tạo user: " + err);
    // Kiểm tra xem user đã tồn tại chưa
    let users = db.getUsers();
    let adminExists = users.users.some(user => user.user === "admin");
    if (adminExists) {
        print("User admin đã tồn tại.");
    } else {
        throw err;
    }
}

// Hiển thị thông báo hoàn thành
print("MongoDB replica set đã được khởi tạo với user admin!");
print("User: admin");
print("Password: abcd1234");
print("\nKết nối từ bên ngoài với connection string:");
print(`mongodb://admin:abcd1234@${VPS_IP}:27017,${VPS_IP}:27018,${VPS_IP}:27019/?replicaSet=rs0&authSource=admin`); 