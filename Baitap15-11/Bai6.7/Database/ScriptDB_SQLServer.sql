USE master;
GO
IF DB_ID(N'ClinicDB') IS NOT NULL
BEGIN
    ALTER DATABASE ClinicDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE ClinicDB;
END
GO
CREATE DATABASE ClinicDB;
GO
USE ClinicDB;
GO

CREATE TABLE BenhNhan (
    MaBN        INT IDENTITY(1,1) PRIMARY KEY,
    HoTen       NVARCHAR(100) NOT NULL,
    SoDienThoai VARCHAR(15) NULL,
    NamSinh     INT NULL
);
CREATE UNIQUE INDEX UQ_BenhNhan_SoDienThoai ON BenhNhan (SoDienThoai) WHERE SoDienThoai IS NOT NULL;

CREATE TABLE HoSoKham (
    MaHoSo     INT IDENTITY(1,1) PRIMARY KEY,
    MaBN       INT NOT NULL,
    NgayKham   DATETIME DEFAULT GETDATE(),
    TrieuChung NVARCHAR(MAX) NULL,
    ChanDoan   NVARCHAR(250) NULL,
    CONSTRAINT FK_HoSoKham_BenhNhan FOREIGN KEY (MaBN) REFERENCES BenhNhan (MaBN)
);

CREATE TABLE Thuoc (
    MaThuoc    INT IDENTITY(1,1) PRIMARY KEY,
    TenThuoc   NVARCHAR(150) NOT NULL,
    DonViTinh  NVARCHAR(20) NULL,
    DonGia     DECIMAL(18,2) NOT NULL DEFAULT 0,
    SoLuongTon INT NOT NULL DEFAULT 0,
    CONSTRAINT CK_Thuoc_SoLuongTon CHECK (SoLuongTon >= 0)
);

CREATE TABLE DonThuoc (
    MaDon     INT IDENTITY(1,1) PRIMARY KEY,
    MaHoSo    INT NOT NULL,
    TienKham  DECIMAL(18,2) NOT NULL DEFAULT 0,
    TongTien  DECIMAL(18,2) NOT NULL DEFAULT 0,
    TrangThai NVARCHAR(30) NOT NULL DEFAULT N'Chờ lấy thuốc',
    CONSTRAINT FK_DonThuoc_HoSoKham FOREIGN KEY (MaHoSo) REFERENCES HoSoKham (MaHoSo)
);

CREATE TABLE ChiTietDonThuoc (
    MaDon    INT NOT NULL,
    MaThuoc  INT NOT NULL,
    SoLuong  INT NOT NULL,
    LieuDung NVARCHAR(100) NULL,
    CONSTRAINT PK_ChiTietDonThuoc PRIMARY KEY (MaDon, MaThuoc),
    CONSTRAINT FK_ChiTietDonThuoc_DonThuoc FOREIGN KEY (MaDon) REFERENCES DonThuoc (MaDon) ON DELETE CASCADE,
    CONSTRAINT FK_ChiTietDonThuoc_Thuoc FOREIGN KEY (MaThuoc) REFERENCES Thuoc (MaThuoc),
    CONSTRAINT CK_ChiTietDonThuoc_SoLuong CHECK (SoLuong > 0)
);
GO

INSERT INTO BenhNhan (HoTen, SoDienThoai, NamSinh) VALUES
(N'Nguyễn Văn Nam', '0901234567', 1985),
(N'Trần Thị Lan',   '0912345678', 1992),
(N'Lê Văn Phúc',    '0987654321', 1970),
(N'Phạm Thị Mai',   '0933444555', 2001),
(N'Đỗ Quang Huy',   '0977888999', 1998);

INSERT INTO Thuoc (TenThuoc, DonViTinh, DonGia, SoLuongTon) VALUES
(N'Paracetamol 500mg',      N'Viên', 1000,  500),
(N'Amoxicillin 500mg',      N'Viên', 2500,  300),
(N'Vitamin C 500mg',        N'Viên', 800,   400),
(N'Berberin',               N'Vỉ',   12000, 50),
(N'Siro ho Prospan 100ml',  N'Chai', 85000, 20),
(N'Omeprazol 20mg',         N'Viên', 3000,  200),
(N'Oresol',                 N'Gói',  2000,  150),
(N'Nước muối sinh lý 0.9%', N'Chai', 5000,  5);

INSERT INTO HoSoKham (MaBN, NgayKham, TrieuChung, ChanDoan) VALUES
(1, DATEADD(DAY, -3, GETDATE()), N'Sốt cao 39 độ, đau đầu, mệt mỏi', N'Sốt siêu vi'),
(2, DATEADD(DAY, -1, GETDATE()), N'Ho nhiều về đêm, đau họng', N'Viêm họng cấp'),
(3, GETDATE(), N'Đau thượng vị, ợ chua sau ăn', NULL),
(4, GETDATE(), N'Tiêu chảy 3 lần/ngày, đau bụng nhẹ', NULL);

INSERT INTO DonThuoc (MaHoSo, TienKham, TongTien, TrangThai) VALUES
(1, 100000, 100000 + 10*1000 + 10*800,  N'Đã thanh toán'),
(2, 100000, 100000 + 14*2500 + 1*85000, N'Chờ lấy thuốc');

INSERT INTO ChiTietDonThuoc (MaDon, MaThuoc, SoLuong, LieuDung) VALUES
(1, 1, 10, N'Uống 1 viên khi sốt trên 38.5 độ, cách 4-6 giờ'),
(1, 3, 10, N'Sáng 1 viên sau ăn'),
(2, 2, 14, N'Sáng 1 viên, Chiều 1 viên sau ăn'),
(2, 5, 1,  N'Ngày 3 lần, mỗi lần 5ml');
GO

SELECT * FROM BenhNhan;
SELECT * FROM HoSoKham;
SELECT * FROM Thuoc;
SELECT * FROM DonThuoc;
SELECT * FROM ChiTietDonThuoc;
