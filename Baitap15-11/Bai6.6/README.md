# Bài 6.6 – Hệ thống Quản lý Mượn trả sách Thư viện

Ứng dụng WinForms (C#, .NET Framework 4.8) theo kiến trúc 3 lớp DTO / DAL / BUS / GUI.
Mặc định chương trình kết nối **MySQL**. Cuối file có hướng dẫn chuyển sang **Microsoft SQL Server**.

## Cấu trúc thư mục

```
Bai6.6/
├── Database/
│   ├── ScriptDB.sql             # Tạo CSDL + dữ liệu mẫu cho MySQL
│   └── ScriptDB_SQLServer.sql   # Tạo CSDL + dữ liệu mẫu cho SQL Server
├── LibraryManagement/
│   ├── DTO/      # DocGiaDTO, DauSachDTO, PhieuMuonDTO, ChiTietMuonDTO, ThongKeTheLoaiDTO
│   ├── DAL/      # DBConnect, DocGiaDAL, DauSachDAL, PhieuMuonDAL
│   ├── BUS/      # DocGiaBUS, DauSachBUS, PhieuMuonBUS
│   ├── GUI/      # FormMain, FormDauSach, FormDocGia, FormMuonTra, FormThongKe
│   ├── App.config
│   └── Program.cs
├── LibraryManagement.slnx
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
3. Bấm nút tia sét ⚡ (**Execute**). Script tạo CSDL `LibraryDB` gồm 4 bảng `DocGia`, `DauSach`, `PhieuMuon`, `ChiTietMuon` kèm dữ liệu mẫu.

**Hoặc dùng dòng lệnh** (mở cmd tại thư mục `Bai6.6`):
```bash
mysql -u root -p < Database/ScriptDB.sql
```

> Script chạy `DROP DATABASE IF EXISTS LibraryDB` ở đầu, nên chạy lại sẽ đưa dữ liệu về trạng thái mẫu ban đầu.

### Bước 2. Sửa chuỗi kết nối

Mở `LibraryManagement/App.config` và sửa `Uid` / `Pwd` cho đúng tài khoản MySQL trên máy của bạn:

```xml
<add name="LibraryDB"
     connectionString="Server=localhost;Port=3306;Database=LibraryDB;Uid=root;Pwd=MAT_KHAU;CharSet=utf8mb4;AllowPublicKeyRetrieval=True;"
     providerName="MySql.Data.MySqlClient" />
```

- Nếu root không có mật khẩu thì để `Pwd=;`
- Nếu MySQL không chạy ở cổng 3306 thì sửa `Port=`.

### Bước 3. Chạy chương trình

1. Mở `LibraryManagement.slnx` bằng Visual Studio.
2. Lần đầu mở, Visual Studio tự tải gói NuGet **MySql.Data**. Nếu không tự tải, chuột phải Solution → **Restore NuGet Packages**.
3. Chọn **Build → Rebuild Solution**, sau đó bấm **F5**.

### Lỗi thường gặp

| Thông báo lỗi | Cách xử lý |
|---|---|
| `Access denied for user 'root'@'localhost'` | Sai mật khẩu trong `App.config`. Sửa lại `Pwd=` rồi **Rebuild**. |
| `Unknown database 'librarydb'` | Chưa chạy `ScriptDB.sql` (Bước 1). |
| `Unable to connect to any of the specified MySQL hosts` | MySQL chưa chạy (mở *Services* → khởi động `MySQL80`) hoặc sai `Port`. |
| `The process cannot access the file ... .exe because it is being used` | App vẫn đang mở. Tắt cửa sổ app rồi build lại. |
| Sửa `App.config` nhưng vẫn báo lỗi cũ | Phải **Rebuild** thì file `bin/.../LibraryManagement.exe.config` mới được cập nhật. |

---

## Cách 2: Chạy với Microsoft SQL Server

Code hiện viết cho MySQL (thư viện `MySql.Data`). Muốn chạy trên SQL Server, bạn cần tạo CSDL bằng script T-SQL, đổi chuỗi kết nối, rồi sửa tầng DAL/BUS theo các bước dưới đây. GUI và DTO **không cần sửa**.

### Bước 1. Tạo CSDL

1. Mở **SQL Server Management Studio (SSMS)** và kết nối tới server, ví dụ `localhost` hoặc `.\SQLEXPRESS`.
2. Chọn **File → Open → File…** rồi mở `Database/ScriptDB_SQLServer.sql`.
3. Bấm **Execute** (F5). Script tạo CSDL `LibraryDB` với cùng cấu trúc và dữ liệu mẫu như bản MySQL.

### Bước 2. Bỏ gói MySql.Data

Mở `LibraryManagement/LibraryManagement.csproj` và xóa dòng:

```xml
<PackageReference Include="MySql.Data" Version="8.0.33" />
```

Không cần cài thêm gói nào: `System.Data.SqlClient` có sẵn trong .NET Framework 4.8.

### Bước 3. Đổi chuỗi kết nối trong `App.config`

```xml
<!-- Đăng nhập bằng tài khoản Windows -->
<add name="LibraryDB"
     connectionString="Server=.\SQLEXPRESS;Database=LibraryDB;Trusted_Connection=True;"
     providerName="System.Data.SqlClient" />

<!-- Hoặc đăng nhập bằng tài khoản SQL (sa) -->
<add name="LibraryDB"
     connectionString="Server=localhost;Database=LibraryDB;User Id=sa;Password=MAT_KHAU;"
     providerName="System.Data.SqlClient" />
```

Chỉ giữ **một** dòng `<add name="LibraryDB" …>`. `Server=` phải đúng với tên server bạn dùng để đăng nhập SSMS.

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
| `ex.Number == 1451` | `ex.Number == 547` *(mã lỗi khóa ngoại của SQL Server)* |

`SqlDataReader` không có kiểu `rd.GetInt32("TenCot")` như MySQL. Vẫn trong hộp Replace, bật **Use regular expressions** rồi thay:

- Tìm: `rd\.Get(Int32|String|DateTime|Decimal)\("(\w+)"\)`
- Thay: `rd.Get$1(rd.GetOrdinal("$2"))`

### Bước 5. Lấy ID vừa INSERT (thay cho `LastInsertedId`)

MySQL dùng `cmd.LastInsertedId`. Với SQL Server, thêm `SELECT SCOPE_IDENTITY()` vào cuối câu INSERT và dùng `ExecuteScalar()`.

`PhieuMuonDAL.LapPhieu`:

```csharp
// MySQL
MySqlCommand cmdPhieu = new MySqlCommand(
    "INSERT INTO PhieuMuon (MaDocGia, NgayMuon, HanTra, TrangThai) VALUES (@maDG, NOW(), @hanTra, @tt)", conn, trans);
...
cmdPhieu.ExecuteNonQuery();
int maPhieu = (int)cmdPhieu.LastInsertedId;

// SQL Server
SqlCommand cmdPhieu = new SqlCommand(
    "INSERT INTO PhieuMuon (MaDocGia, NgayMuon, HanTra, TrangThai) VALUES (@maDG, GETDATE(), @hanTra, @tt); " +
    "SELECT CAST(SCOPE_IDENTITY() AS INT);", conn, trans);
...
int maPhieu = (int)cmdPhieu.ExecuteScalar();
```

### Bước 6. Sửa các câu SQL khác cú pháp

| File / hàm | MySQL (hiện tại) | SQL Server (sửa thành) |
|---|---|---|
| `DocGiaDAL.KiemTraTheConHan` | `NgayHetHanThe >= NOW()` | `NgayHetHanThe >= GETDATE()` |
| `PhieuMuonDAL.TraSach` | `UPDATE DauSach ds INNER JOIN ChiTietMuon ct ON ds.MaSach = ct.MaSach SET ds.SoLuongCon = ds.SoLuongCon + ct.SoLuong WHERE ct.MaPhieu = @ma` | `UPDATE ds SET ds.SoLuongCon = ds.SoLuongCon + ct.SoLuong FROM DauSach ds INNER JOIN ChiTietMuon ct ON ds.MaSach = ct.MaSach WHERE ct.MaPhieu = @ma` |
| `PhieuMuonDAL.CapNhatQuaHan` | `HanTra < NOW()` | `HanTra < GETDATE()` |
| `PhieuMuonDAL.GetPhieuQuaHan` | `DATEDIFF(NOW(), pm.HanTra)` và `pm.HanTra < NOW()` | `DATEDIFF(DAY, pm.HanTra, GETDATE())` và `pm.HanTra < GETDATE()` |
| `PhieuMuonDAL.ThongKeTheoTheLoai` | `IFNULL(ds.TheLoai, '(Chưa phân loại)')` | `ISNULL(ds.TheLoai, N'(Chưa phân loại)')` |

`CONCAT('%', @kw, '%')` trong `DauSachDAL` chạy được trên SQL Server 2012+, không cần sửa.

### Bước 7. Build và chạy

Chọn **Build → Rebuild Solution** rồi bấm **F5**. Nếu còn lỗi biên dịch, thường là do bỏ sót một chỗ ở Bước 4 hoặc Bước 5. Danh sách lỗi (Error List) sẽ chỉ đúng file và dòng cần sửa.

---

## Chức năng chính

- **Quản lý Đầu sách / Độc giả:** thêm, sửa, xóa, tìm kiếm qua đủ các lớp DTO → BUS → DAL.
- **Lập phiếu mượn:** kiểm tra hạn thẻ (`NgayHetHanThe >= NOW()`) trước khi lập. Lưu phiếu, chi tiết và trừ `SoLuongCon` trong cùng một transaction.
- **Lọc phiếu** theo độc giả và trạng thái (Đang mượn / Đã trả / Quá hạn).
- **Trả sách:** UPDATE có tham số, chuyển trạng thái sang "Đã trả" và cộng lại `SoLuongCon`.
- **Cảnh báo quá hạn:** đọc bằng DataReader các phiếu có `HanTra` đã qua mà chưa trả.
- **Thống kê** lượt mượn theo thể loại bằng `GROUP BY` và `ExecuteScalar`.

Dữ liệu mẫu có sẵn 2 độc giả hết hạn thẻ (Phạm Minh Dũng, Vũ Đức Huy) và 2 phiếu quá hạn để thử các chức năng trên.
