# Bài 6.7 – Hệ thống Quản lý Phòng khám & Kê đơn thuốc

Ứng dụng WinForms (C#, .NET Framework 4.8) theo kiến trúc 3 lớp DTO / DAL / BUS / GUI.
Mặc định chương trình kết nối **MySQL**. Cuối file có hướng dẫn chuyển sang **Microsoft SQL Server**.

## Cấu trúc thư mục

```
Bai6.7/
├── Database/
│   ├── ScriptDB.sql             # Tạo CSDL + dữ liệu mẫu cho MySQL
│   └── ScriptDB_SQLServer.sql   # Tạo CSDL + dữ liệu mẫu cho SQL Server
├── ClinicManagement/
│   ├── DTO/      # BenhNhanDTO, HoSoKhamDTO, ThuocDTO, DonThuocDTO, ChiTietDonThuocDTO, ThieuThuocException
│   ├── DAL/      # DBConnect, BenhNhanDAL, HoSoKhamDAL, ThuocDAL, DonThuocDAL
│   ├── BUS/      # BenhNhanBUS, HoSoKhamBUS, ThuocBUS, DonThuocBUS
│   ├── GUI/      # FormMain, FormTiepNhan, FormKeDon, FormThanhToan, FormThuoc
│   ├── App.config
│   └── Program.cs
├── ClinicManagement.slnx
└── README.md
```

## Yêu cầu

- Visual Studio 2022 bản **17.13 trở lên** (để mở được file `.slnx`), có cài workload **.NET desktop development**.
- .NET Framework 4.8.
- MySQL Server 8.0 và MySQL Workbench, **hoặc** SQL Server 2016+ và SQL Server Management Studio (SSMS).

---

## Cách 1: Chạy với MySQL (mặc định)

### Bước 1. Tạo CSDL

**Dùng MySQL Workbench:**
1. Mở Workbench và đăng nhập vào kết nối `root@localhost`.
2. Chọn **File → Open SQL Script…** rồi mở `Database/ScriptDB.sql`.
3. Bấm nút tia sét ⚡ (**Execute**). Script tạo CSDL `ClinicDB` gồm 5 bảng `BenhNhan`, `HoSoKham`, `Thuoc`, `DonThuoc`, `ChiTietDonThuoc` kèm dữ liệu mẫu.

**Hoặc dùng dòng lệnh** (mở cmd tại thư mục `Bai6.7`):
```bash
mysql -u root -p < Database/ScriptDB.sql
```

> Script chạy `DROP DATABASE IF EXISTS ClinicDB` ở đầu, nên chạy lại sẽ đưa dữ liệu về trạng thái mẫu ban đầu.

### Bước 2. Sửa chuỗi kết nối

Mở `ClinicManagement/App.config` và sửa `Uid` / `Pwd` cho đúng tài khoản MySQL trên máy của bạn:

```xml
<add name="ClinicDB"
     connectionString="Server=localhost;Port=3306;Database=ClinicDB;Uid=root;Pwd=MAT_KHAU;CharSet=utf8mb4;AllowPublicKeyRetrieval=True;"
     providerName="MySql.Data.MySqlClient" />
```

- Nếu root không có mật khẩu thì để `Pwd=;`
- Nếu MySQL không chạy ở cổng 3306 thì sửa `Port=`.

### Bước 3. Chạy chương trình

1. Mở `ClinicManagement.slnx` bằng Visual Studio.
2. Lần đầu mở, Visual Studio tự tải gói NuGet **MySql.Data**. Nếu không tự tải, chuột phải Solution → **Restore NuGet Packages**.
3. Chọn **Build → Rebuild Solution**, sau đó bấm **F5**.

### Lỗi thường gặp

| Thông báo lỗi | Cách xử lý |
|---|---|
| `Access denied for user 'root'@'localhost'` | Sai mật khẩu trong `App.config`. Sửa lại `Pwd=` rồi **Rebuild**. |
| `Unknown database 'clinicdb'` | Chưa chạy `ScriptDB.sql` (Bước 1). |
| `Unable to connect to any of the specified MySQL hosts` | MySQL chưa chạy (mở *Services* → khởi động `MySQL80`) hoặc sai `Port`. |
| `The process cannot access the file ... .exe because it is being used` | App vẫn đang mở. Tắt cửa sổ app rồi build lại. |
| Sửa `App.config` nhưng vẫn báo lỗi cũ | Phải **Rebuild** thì file `bin/.../ClinicManagement.exe.config` mới được cập nhật. |

---

## Cách 2: Chạy với Microsoft SQL Server

Code hiện viết cho MySQL (thư viện `MySql.Data`). Muốn chạy trên SQL Server, bạn cần tạo CSDL bằng script T-SQL, đổi chuỗi kết nối, rồi sửa tầng DAL/BUS theo các bước dưới đây. GUI và DTO **không cần sửa**.

### Bước 1. Tạo CSDL

1. Mở **SQL Server Management Studio (SSMS)** và kết nối tới server, ví dụ `localhost` hoặc `.\SQLEXPRESS`.
2. Chọn **File → Open → File…** rồi mở `Database/ScriptDB_SQLServer.sql`.
3. Bấm **Execute** (F5). Script tạo CSDL `ClinicDB` với cùng cấu trúc và dữ liệu mẫu như bản MySQL.

### Bước 2. Bỏ gói MySql.Data

Mở `ClinicManagement/ClinicManagement.csproj` và xóa dòng:

```xml
<PackageReference Include="MySql.Data" Version="8.0.33" />
```

Không cần cài thêm gói nào: `System.Data.SqlClient` có sẵn trong .NET Framework 4.8.

### Bước 3. Đổi chuỗi kết nối trong `App.config`

```xml
<!-- Đăng nhập bằng tài khoản Windows -->
<add name="ClinicDB"
     connectionString="Server=.\SQLEXPRESS;Database=ClinicDB;Trusted_Connection=True;"
     providerName="System.Data.SqlClient" />

<!-- Hoặc đăng nhập bằng tài khoản SQL (sa) -->
<add name="ClinicDB"
     connectionString="Server=localhost;Database=ClinicDB;User Id=sa;Password=MAT_KHAU;"
     providerName="System.Data.SqlClient" />
```

Chỉ giữ **một** dòng `<add name="ClinicDB" …>`. `Server=` phải đúng với tên server bạn dùng để đăng nhập SSMS.

### Bước 4. Đổi lớp ADO.NET

Trong Visual Studio, mở **Edit → Find and Replace → Replace in Files** (`Ctrl+Shift+H`), chọn phạm vi *Entire solution* rồi thay lần lượt:

| Tìm | Thay bằng |
|---|---|
| `using MySql.Data.MySqlClient;` | `using System.Data.SqlClient;` |
| `MySqlConnection` | `SqlConnection` |
| `MySqlCommand` | `SqlCommand` |
| `MySqlDataReader` | `SqlDataReader` |
| `MySqlDataAdapter` | `SqlDataAdapter` |
| `MySqlParameter` | `SqlParameter` |
| `MySqlTransaction` | `SqlTransaction` |
| `MySqlException` | `SqlException` |
| `MySqlDbType` | `SqlDbType` *(thêm `using System.Data;` ở đầu `BenhNhanDAL.cs`)* |
| `ex.Number == 1451` | `ex.Number == 547` *(mã lỗi khóa ngoại của SQL Server)* |

`SqlDataReader` không có kiểu `rd.GetInt32("TenCot")` như MySQL. Vẫn trong hộp Replace, bật **Use regular expressions** rồi thay:

- Tìm: `rd\.Get(Int32|String|DateTime|Decimal)\("(\w+)"\)`
- Thay: `rd.Get$1(rd.GetOrdinal("$2"))`

### Bước 5. Lấy ID vừa INSERT (thay cho `LastInsertedId`)

MySQL dùng `cmd.LastInsertedId`. Với SQL Server, thêm `SELECT SCOPE_IDENTITY()` vào cuối câu INSERT và dùng `ExecuteScalar()`. Có **3 chỗ** cần sửa:

- `BenhNhanDAL.Insert`
- `HoSoKhamDAL.Insert`
- `DonThuocDAL.LuuVaXuatDon` (bản ghi `DonThuoc`)

Ví dụ với `HoSoKhamDAL.Insert`:

```csharp
// MySQL
const string sql = "INSERT INTO HoSoKham (MaBN, NgayKham, TrieuChung) VALUES (@maBN, NOW(), @trieuChung)";
...
cmd.ExecuteNonQuery();
return (int)cmd.LastInsertedId;

// SQL Server
const string sql = "INSERT INTO HoSoKham (MaBN, NgayKham, TrieuChung) VALUES (@maBN, GETDATE(), @trieuChung); " +
                   "SELECT CAST(SCOPE_IDENTITY() AS INT);";
...
return (int)cmd.ExecuteScalar();
```

Hai chỗ còn lại làm tương tự: bỏ `cmd.ExecuteNonQuery()` và thay `(int)cmd.LastInsertedId` bằng `(int)cmd.ExecuteScalar()`.

### Bước 6. Sửa các câu SQL khác cú pháp

| File / hàm | MySQL (hiện tại) | SQL Server (sửa thành) |
|---|---|---|
| `HoSoKhamDAL.Insert` | `NOW()` | `GETDATE()` *(đã gộp ở Bước 5)* |
| `DonThuocDAL.LuuVaXuatDon` (tính `TongTien`) | `UPDATE DonThuoc d SET d.TongTien = d.TienKham + IFNULL((SELECT ...), 0) WHERE d.MaDon = @maDon` | `UPDATE d SET d.TongTien = d.TienKham + ISNULL((SELECT ...), 0) FROM DonThuoc d WHERE d.MaDon = @maDon` |

Câu tính `TongTien` sau khi sửa đầy đủ:

```sql
UPDATE d SET d.TongTien = d.TienKham + ISNULL((
    SELECT SUM(ct.SoLuong * t.DonGia) FROM ChiTietDonThuoc ct
    INNER JOIN Thuoc t ON ct.MaThuoc = t.MaThuoc WHERE ct.MaDon = d.MaDon), 0)
FROM DonThuoc d
WHERE d.MaDon = @maDon
```

`CONCAT('%', @kw, '%')` trong `ThuocDAL` chạy được trên SQL Server 2012+, không cần sửa.

### Bước 7. Build và chạy

Chọn **Build → Rebuild Solution** rồi bấm **F5**. Nếu còn lỗi biên dịch, thường là do bỏ sót một chỗ ở Bước 4 hoặc Bước 5. Danh sách lỗi (Error List) sẽ chỉ đúng file và dòng cần sửa.

---

## Chức năng chính

- **Tiếp nhận bệnh nhân:** tra cứu theo số điện thoại bằng tham số SQL. Nếu là bệnh nhân mới thì nhập thông tin, sau đó tạo hồ sơ khám.
- **Danh mục thuốc:** thêm thuốc mới, cập nhật đơn giá, nhập thêm tồn kho.
- **Kê đơn ("Lưu & Xuất đơn")** trong một transaction: tạo `DonThuoc`, tạo `ChiTietDonThuoc`, trừ `SoLuongTon`. Nếu một thuốc không đủ tồn kho thì gọi `trans.Rollback()` và hiện cảnh báo.
- **Thanh toán:** Tổng = `TienKham + Σ(SoLuong × DonGia)`, chuyển trạng thái sang "Đã thanh toán", xem trước và in hóa đơn.

Dữ liệu mẫu có thuốc **"Nước muối sinh lý 0.9%"** chỉ còn 5 chai: kê từ 6 chai trở lên để thử trường hợp Rollback.
