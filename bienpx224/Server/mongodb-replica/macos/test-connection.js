// Script để test kết nối vào MongoDB
try {
    // Kết nối vào admin database
    db = db.getSiblingDB('admin');

    // Hiển thị thông tin về MongoDB
    print("=== Thông tin MongoDB ===");
    print("MongoDB version: " + db.version());

    // Kiểm tra xác thực
    let authResult = db.auth("admin", "abcd1234");
    print("Xác thực admin: " + (authResult.ok ? "Thành công" : "Thất bại"));

    if (authResult.ok) {
        // Liệt kê các database
        print("\n=== Danh sách database ===");
        db.getMongo().getDBNames().forEach(function (dbname) {
            print(" - " + dbname);
        });

        // Kiểm tra replica set
        print("\n=== Thông tin Replica Set ===");
        let rsStatus = rs.status();
        print("Replica Set: " + rsStatus.set);
        print("Số lượng thành viên: " + rsStatus.members.length);

        let primary = rsStatus.members.find(m => m.state === 1);
        if (primary) {
            print("PRIMARY: " + primary.name);
        }

        print("\nSecondary nodes:");
        rsStatus.members.filter(m => m.state === 2).forEach(function (node) {
            print(" - " + node.name);
        });
    }

    print("\n=== Kết nối thành công! ===");
} catch (err) {
    print("Lỗi kết nối: " + err);
} 