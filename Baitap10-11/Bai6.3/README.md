# Bài 6.3 – Quản lý dự án & công việc

WinForms .NET 10 + MySQL (`MySql.Data`). CSDL: `ProjectTaskDB` (bảng `DuAn`, `ThanhVien`, `CongViec`, `PhanCong`).

## A. Chạy với MySQL
1. Cài **MySQL Server 8 + MySQL Workbench** (MySQL Installer, cổng `3306`, nhớ mật khẩu `root`).
2. Workbench → mở [database/ProjectTaskDB.sql](database/ProjectTaskDB.sql) → ⚡ Execute.
3. Sửa mật khẩu `Pwd=` tại [DAL/DbConfig.cs – dòng 6](Bai6.3/DAL/DbConfig.cs#L6)
   *(hoặc đặt biến môi trường `PROJECT_TASK_MYSQL_CONNECTION` – dòng 9).*
4. Mở `Bai6.3.slnx` bằng Visual Studio 2026 → **F5**. Thấy "MySQL: đã kết nối" là thành công.

## B. Chạy với Microsoft SQL Server
Code viết cho MySQL, muốn dùng SQL Server (+ SSMS) phải sửa:

**Script** [database/ProjectTaskDB.sql](database/ProjectTaskDB.sql)
| Dòng | Sửa |
|---|---|
| 1 | `CREATE DATABASE ProjectTaskDB;` + `GO` (bỏ `IF NOT EXISTS`, `CHARACTER SET`) |
| 5, 13, 20 | `AUTO_INCREMENT` → `IDENTITY(1,1)` |
| 10, 17, 28, 37 | Bỏ `ENGINE=InnoDB` |
| 33 | `CURRENT_TIMESTAMP` → `GETDATE()` |
| 39 | `INSERT IGNORE` → `INSERT ... WHERE NOT EXISTS` |
| Toàn file | Chuỗi tiếng Việt thêm `N'...'` |

**Code**
| Vị trí | Sửa |
|---|---|
| [Bai6.3.csproj – dòng 13](Bai6.3/Bai6.3.csproj#L13) | `MySql.Data` → `Microsoft.Data.SqlClient` |
| [DbConfig.cs – dòng 6](Bai6.3/DAL/DbConfig.cs#L6) | `Server=localhost\SQLEXPRESS;Database=ProjectTaskDB;Trusted_Connection=True;TrustServerCertificate=True;` |
| [ProjectTaskDal.cs – dòng 2](Bai6.3/DAL/ProjectTaskDal.cs#L2) | `using Microsoft.Data.SqlClient;`, đổi `MySqlXxx` → `SqlXxx` |
| [ProjectTaskDal.cs – dòng 37](Bai6.3/DAL/ProjectTaskDal.cs#L37) | `INSERT IGNORE` → `INSERT ... WHERE NOT EXISTS` |
| [ProjectTaskDal.cs – dòng 65](Bai6.3/DAL/ProjectTaskDal.cs#L65) | `NOW()` → `GETDATE()` |
| [ProjectTaskDal.cs – dòng 89](Bai6.3/DAL/ProjectTaskDal.cs#L89) | `LAST_INSERT_ID()` → `SCOPE_IDENTITY()` |
