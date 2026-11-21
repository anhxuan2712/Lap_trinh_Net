# Bài 6.4 – Quản lý garage sửa xe

WinForms .NET 10 + MySQL (`MySql.Data`). CSDL: `GarageDB` (bảng `KhachHang`, `Xe`, `PhuTung`, `PhieuSuaChua`, `ChiTietSuaChua`).

## A. Chạy với MySQL
1. Cài **MySQL Server 8 + MySQL Workbench** (MySQL Installer, cổng `3306`, nhớ mật khẩu `root`).
2. Workbench → mở [Bai6.4/database/GarageDB.sql](Bai6.4/database/GarageDB.sql) → ⚡ Execute.
   *Script không có dữ liệu mẫu → thêm khách hàng, xe, phụ tùng trên giao diện trước khi lập phiếu.*
3. Sửa mật khẩu `Pwd=` tại [DAL/DbConfig.cs – dòng 21](Bai6.4/DAL/DbConfig.cs#L21)
   *(hoặc đặt biến môi trường `GARAGE_MYSQL_CONNECTION` – dòng 14).*
4. Mở `Bai6.4.slnx` bằng Visual Studio 2026 → **F5**.

## B. Chạy với Microsoft SQL Server
Code viết cho MySQL, muốn dùng SQL Server (+ SSMS) phải sửa:

**Script** [Bai6.4/database/GarageDB.sql](Bai6.4/database/GarageDB.sql)
| Dòng | Sửa |
|---|---|
| 1 | `CREATE DATABASE GarageDB;` + `GO` (bỏ `IF NOT EXISTS`, `CHARACTER SET`) |
| 5, 20, 29 | `AUTO_INCREMENT` → `IDENTITY(1,1)` |
| 7 | `UNIQUE` trên cột cho phép NULL → dùng `CREATE UNIQUE INDEX ... WHERE DienThoai IS NOT NULL` |
| 9, 17, 26, 36, 48 | Bỏ `ENGINE=InnoDB` |
| 31 | `CURRENT_TIMESTAMP` → `GETDATE()` |
| 32 | `LONGTEXT` → `NVARCHAR(MAX)` |
| Toàn file | Chuỗi tiếng Việt thêm `N'...'` |

**Code**
| Vị trí | Sửa |
|---|---|
| [Bai6.4.csproj – dòng 13](Bai6.4/Bai6.4.csproj#L13) | `MySql.Data` → `Microsoft.Data.SqlClient` |
| [DbConfig.cs](Bai6.4/DAL/DbConfig.cs) | Viết lại, chỉ trả về `Server=localhost\SQLEXPRESS;Database=GarageDB;Trusted_Connection=True;TrustServerCertificate=True;` (bỏ `MySqlConnectionStringBuilder`) |
| [GarageRepository.cs – dòng 1](Bai6.4/DAL/GarageRepository.cs#L1) | `using Microsoft.Data.SqlClient;`, đổi `MySqlXxx` → `SqlXxx` |
| [GarageRepository.cs – dòng 88](Bai6.4/DAL/GarageRepository.cs#L88) | `ON DUPLICATE KEY UPDATE` → `MERGE` hoặc `IF EXISTS ... UPDATE ... ELSE INSERT` |
| [GarageRepository.cs – dòng 184](Bai6.4/DAL/GarageRepository.cs#L184) | `FOR UPDATE` → `WITH (UPDLOCK, ROWLOCK)` sau tên bảng |
| [GarageRepository.cs – dòng 205](Bai6.4/DAL/GarageRepository.cs#L205) | `LAST_INSERT_ID()` → `SCOPE_IDENTITY()` |
