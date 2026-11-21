# Bài 6.5 – Quản lý karaoke

WinForms .NET 10 + MySQL (`MySql.Data`), 3 lớp DTO / DAL / BUS / GUI. CSDL: `KaraokeDB` (bảng `PhongHat`, `DichVu`, `HoaDon`, `ChiTietDichVu`; có sẵn 4 phòng + 5 dịch vụ mẫu).

## A. Chạy với MySQL
1. Cài **MySQL Server 8 + MySQL Workbench** (MySQL Installer, cổng `3306`, nhớ mật khẩu `root`).
2. Workbench → mở [database/KaraokeDB.sql](database/KaraokeDB.sql) → ⚡ Execute.
3. Sửa mật khẩu `Pwd=` tại [DAL/DbConfig.cs – dòng 9](Bai6.5/DAL/DbConfig.cs#L9).
4. Mở `Bai6.5.slnx` bằng Visual Studio 2026 → **F5**.
5. Thử: chọn phòng xanh → **Mở phòng** → **Gọi dịch vụ** → **Thanh toán**.

## B. Chạy với Microsoft SQL Server
Code viết cho MySQL, muốn dùng SQL Server (+ SSMS) phải sửa (chỉ trong `DAL/`, BUS và GUI giữ nguyên):

**Script** [database/KaraokeDB.sql](database/KaraokeDB.sql)
| Dòng | Sửa |
|---|---|
| 1–3 | `CREATE DATABASE KaraokeDB;` + `GO` (bỏ `IF NOT EXISTS`, `CHARACTER SET`) |
| 9, 21, 32 | `AUTO_INCREMENT` → `IDENTITY(1,1)` |
| 17, 28, 45, 61 | Bỏ `ENGINE = InnoDB` |
| 34 | `CURRENT_TIMESTAMP` → `GETDATE()` |
| 38 | `TINYINT(1)` → `TINYINT` |
| 40, 54 | Bỏ `KEY IX_...`, tạo riêng bằng `CREATE INDEX` |
| Toàn file | `VARCHAR` chứa tiếng Việt → `NVARCHAR`, chuỗi thêm `N'...'` |

**Code**
| Vị trí | Sửa |
|---|---|
| [Bai6.5.csproj – dòng 13](Bai6.5/Bai6.5.csproj#L13) | `MySql.Data` → `Microsoft.Data.SqlClient` |
| [DbConfig.cs – dòng 9](Bai6.5/DAL/DbConfig.cs#L9) | `Server=localhost\SQLEXPRESS;Database=KaraokeDB;Trusted_Connection=True;TrustServerCertificate=True;` |
| [KaraokeDal.cs – dòng 3](Bai6.5/DAL/KaraokeDal.cs#L3) | `using Microsoft.Data.SqlClient;`, đổi `MySqlXxx` → `SqlXxx` |
| [KaraokeDal.cs – dòng 98, 134, 146, 183](Bai6.5/DAL/KaraokeDal.cs#L98) | `FOR UPDATE` → `WITH (UPDLOCK, ROWLOCK)` sau tên bảng |
| [KaraokeDal.cs – dòng 125](Bai6.5/DAL/KaraokeDal.cs#L125) | `NOW()` → `GETDATE()` |
| [KaraokeDal.cs – dòng 216](Bai6.5/DAL/KaraokeDal.cs#L216) | `ON DUPLICATE KEY UPDATE` → `MERGE` hoặc `IF EXISTS ... UPDATE ... ELSE INSERT` |
