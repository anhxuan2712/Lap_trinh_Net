# Bài 6.8 – Hệ thống Quản lý Bàn Bida & Dịch vụ giải trí

Ứng dụng WinForms (C#, .NET Framework 4.8) theo kiến trúc 3 lớp DTO / DAL / BUS / GUI.
Mặc định chương trình kết nối **MySQL**. Cuối file có hướng dẫn chuyển sang **Microsoft SQL Server**.

## Cấu trúc thư mục

```
Bai6.8/
├── Database/
│   ├── ScriptDB.sql             # Tạo CSDL + dữ liệu mẫu cho MySQL
│   └── ScriptDB_SQLServer.sql   # Tạo CSDL + dữ liệu mẫu cho SQL Server
├── BilliardsManagement/
│   ├── DTO/      # BanBidaDTO, MenuDichVuDTO, HoaDonDTO, ChiTietHoaDonDTO
│   ├── DAL/      # DBConnect, BanBidaDAL, MenuDichVuDAL, HoaDonDAL, ChiTietHoaDonDAL
│   ├── BUS/      # BanBidaBUS, DichVuBUS, HoaDonBUS
│   ├── GUI/      # FormMain (sơ đồ bàn + gọi dịch vụ + thanh toán)
│   ├── App.config
│   └── Program.cs
├── BilliardsManagement.slnx
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
3. Bấm nút tia sét ⚡ (**Execute**). Script tạo CSDL `BilliardsDB` gồm 4 bảng `BanBida`, `MenuDichVu`, `HoaDon`, `ChiTietHoaDon` kèm dữ liệu mẫu.

**Hoặc dùng dòng lệnh** (mở cmd tại thư mục `Bai6.8`):
```bash
mysql -u root -p < Database/ScriptDB.sql
```

> Script chạy `DROP DATABASE IF EXISTS BilliardsDB` ở đầu, nên chạy lại sẽ đưa dữ liệu về trạng thái mẫu ban đầu.

### Bước 2. Sửa chuỗi kết nối

Mở `BilliardsManagement/App.config` và sửa `Uid` / `Pwd` cho đúng tài khoản MySQL trên máy của bạn:

```xml
<add name="BilliardsDB"
     connectionString="Server=localhost;Port=3306;Database=BilliardsDB;Uid=root;Pwd=MAT_KHAU;CharSet=utf8mb4;AllowPublicKeyRetrieval=True;"
     providerName="MySql.Data.MySqlClient" />
```

- Nếu root không có mật khẩu thì để `Pwd=;`
- Nếu MySQL không chạy ở cổng 3306 thì sửa `Port=`.

### Bước 3. Chạy chương trình

1. Mở `BilliardsManagement.slnx` bằng Visual Studio.
2. Lần đầu mở, Visual Studio tự tải gói NuGet **MySql.Data**. Nếu không tự tải, chuột phải Solution → **Restore NuGet Packages**.
3. Chọn **Build → Rebuild Solution**, sau đó bấm **F5**.

### Lỗi thường gặp

| Thông báo lỗi | Cách xử lý |
|---|---|
| `Access denied for user 'root'@'localhost'` | Sai mật khẩu trong `App.config`. Sửa lại `Pwd=` rồi **Rebuild**. |
| `Unknown database 'billiardsdb'` | Chưa chạy `ScriptDB.sql` (Bước 1). |
| `Unable to connect to any of the specified MySQL hosts` | MySQL chưa chạy (mở *Services* → khởi động `MySQL80`) hoặc sai `Port`. |
| `The process cannot access the file ... .exe because it is being used` | App vẫn đang mở. Tắt cửa sổ app rồi build lại. |
| Sửa `App.config` nhưng vẫn báo lỗi cũ | Phải **Rebuild** thì file `bin/.../BilliardsManagement.exe.config` mới được cập nhật. |

---

## Cách 2: Chạy với Microsoft SQL Server

Code hiện viết cho MySQL (thư viện `MySql.Data`). Muốn chạy trên SQL Server, bạn cần tạo CSDL bằng script T-SQL, đổi chuỗi kết nối, rồi sửa tầng DAL theo các bước dưới đây. GUI, BUS và DTO **không cần sửa**.

### Bước 1. Tạo CSDL

1. Mở **SQL Server Management Studio (SSMS)** và kết nối tới server, ví dụ `localhost` hoặc `.\SQLEXPRESS`.
2. Chọn **File → Open → File…** rồi mở `Database/ScriptDB_SQLServer.sql`.
3. Bấm **Execute** (F5). Script tạo CSDL `BilliardsDB` với cùng cấu trúc và dữ liệu mẫu như bản MySQL. Cột `HoaDon.TrangThai` dùng kiểu `BIT`.

### Bước 2. Bỏ gói MySql.Data

Mở `BilliardsManagement/BilliardsManagement.csproj` và xóa dòng:

```xml
<PackageReference Include="MySql.Data" Version="8.0.33" />
```

Không cần cài thêm gói nào: `System.Data.SqlClient` có sẵn trong .NET Framework 4.8.

### Bước 3. Đổi chuỗi kết nối trong `App.config`

```xml
<!-- Đăng nhập bằng tài khoản Windows -->
<add name="BilliardsDB"
     connectionString="Server=.\SQLEXPRESS;Database=BilliardsDB;Trusted_Connection=True;"
     providerName="System.Data.SqlClient" />

<!-- Hoặc đăng nhập bằng tài khoản SQL (sa) -->
<add name="BilliardsDB"
     connectionString="Server=localhost;Database=BilliardsDB;User Id=sa;Password=MAT_KHAU;"
     providerName="System.Data.SqlClient" />
```

Chỉ giữ **một** dòng `<add name="BilliardsDB" …>`. `Server=` phải đúng với tên server bạn dùng để đăng nhập SSMS.

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
| `MySqlDbType.Int32` | `SqlDbType.Int` |
| `MySqlDbType.Decimal` | `SqlDbType.Decimal` |

`ChiTietHoaDonDAL.cs` đã có sẵn `using System.Data;` nên dùng được `SqlDbType`.

`SqlDataReader` không có kiểu `rd.GetInt32("TenCot")` như MySQL. Vẫn trong hộp Replace, bật **Use regular expressions** rồi thay:

- Tìm: `rd\.Get(Int32|String|DateTime|Decimal)\("(\w+)"\)`
- Thay: `rd.Get$1(rd.GetOrdinal("$2"))`

### Bước 5. Lấy ID vừa INSERT (thay cho `LastInsertedId`)

Chỉ có **1 chỗ**, trong `HoaDonDAL.MoBan`:

```csharp
// MySQL
MySqlCommand cmdHD = new MySqlCommand(
    "INSERT INTO HoaDon (MaBan, GioBatDau, GioKetThuc, TienGioChoi, TienDichVu, TrangThai) " +
    "VALUES (@maBan, NOW(), NULL, 0, 0, 0)", conn, trans);
cmdHD.Parameters.AddWithValue("@maBan", maBan);
cmdHD.ExecuteNonQuery();
int maHD = (int)cmdHD.LastInsertedId;

// SQL Server
SqlCommand cmdHD = new SqlCommand(
    "INSERT INTO HoaDon (MaBan, GioBatDau, GioKetThuc, TienGioChoi, TienDichVu, TrangThai) " +
    "VALUES (@maBan, GETDATE(), NULL, 0, 0, 0); SELECT CAST(SCOPE_IDENTITY() AS INT);", conn, trans);
cmdHD.Parameters.AddWithValue("@maBan", maBan);
int maHD = (int)cmdHD.ExecuteScalar();
```

### Bước 6. Sửa các câu SQL khác cú pháp (đều trong `HoaDonDAL.cs`)

| Hàm | MySQL (hiện tại) | SQL Server (sửa thành) |
|---|---|---|
| `GetHoaDonDangMo` | `SELECT MaHD, ... FROM HoaDon WHERE ... ORDER BY MaHD DESC LIMIT 1` | `SELECT TOP 1 MaHD, ... FROM HoaDon WHERE ... ORDER BY MaHD DESC` |
| `ThanhToan` (tính tiền DV) | `IFNULL(SUM(SoLuong * DonGia), 0)` | `ISNULL(SUM(SoLuong * DonGia), 0)` |
| `ThanhToan` (kiểm tra tồn) | `... INNER JOIN MenuDichVu m ON ... WHERE ... m.TenDV NOT LIKE 'Thuê%' ... FOR UPDATE` | `... INNER JOIN MenuDichVu m WITH (UPDLOCK) ON ... WHERE ... m.TenDV NOT LIKE N'Thuê%' ...` (bỏ `FOR UPDATE`) |
| `ThanhToan` (trừ tồn kho) | `UPDATE MenuDichVu m INNER JOIN ChiTietHoaDon ct ON m.MaDV = ct.MaDV SET m.TonKho = m.TonKho - ct.SoLuong WHERE ct.MaHD = @maHD AND m.TenDV NOT LIKE 'Thuê%'` | `UPDATE m SET m.TonKho = m.TonKho - ct.SoLuong FROM MenuDichVu m INNER JOIN ChiTietHoaDon ct ON m.MaDV = ct.MaDV WHERE ct.MaHD = @maHD AND m.TenDV NOT LIKE N'Thuê%'` |

> Chuỗi có dấu tiếng Việt viết thẳng trong câu SQL (như `'Thuê%'`) phải thêm tiền tố `N` trên SQL Server, nếu không sẽ không so khớp đúng.

Phần gọi món dùng `SqlDataAdapter` + `DataTable` (`ChiTietHoaDonDAL`) giữ nguyên logic. Chỉ cần đổi tên lớp như ở Bước 4.

### Bước 7. Build và chạy

Chọn **Build → Rebuild Solution** rồi bấm **F5**. Nếu còn lỗi biên dịch, thường là do bỏ sót một chỗ ở Bước 4 hoặc Bước 5. Danh sách lỗi (Error List) sẽ chỉ đúng file và dòng cần sửa.

---

## Chức năng chính

- **Sơ đồ bàn:** các nút bàn được sinh động từ CSDL. Bàn "Trống" màu xanh lá, bàn "Đang chơi" màu cam/đỏ và hiện thời gian đã chơi, cập nhật mỗi giây.
- **Mở bàn:** chọn bàn trống rồi bấm "MỞ BÀN". Chương trình tạo `HoaDon` với `GioBatDau = NOW()` và đổi bàn sang "Đang chơi".
- **Gọi dịch vụ (mô hình ngắt kết nối):** danh sách món nạp vào `DataTable` bằng `DataAdapter`. Có thể thêm món, sửa số lượng ngay trên lưới, xóa món, rồi bấm "Lưu dịch vụ" để gọi `adapter.Update()`.
- **Thanh toán** trong một transaction: cập nhật `GioKetThuc`, `TienGioChoi`, `TienDichVu`, `TrangThai = 1`; trừ `TonKho` (bỏ qua dịch vụ thuê gậy); trả bàn về "Trống"; rồi `Commit()`.
- **Tính giờ:** chọn làm tròn **block 15 phút** (làm tròn lên, tối thiểu 15 phút) hoặc **chính xác theo phút**. `TienGioChoi = (số phút / 60) × GiaTheoGio`.

Quy ước: dịch vụ có tên bắt đầu bằng **"Thuê"** (Thuê gậy Carbon, Thuê gậy thường) không bị trừ tồn kho.
Dữ liệu mẫu có sẵn 2 bàn đang chơi (Bàn Lỗ 01, Bàn Carom 01). Món **"Khô bò"** chỉ còn 3 phần: gọi từ 4 phần trở lên để thử trường hợp thiếu tồn kho khi thanh toán.
