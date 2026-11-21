USE master;
GO
IF DB_ID(N'BilliardsDB') IS NOT NULL
BEGIN
    ALTER DATABASE BilliardsDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE BilliardsDB;
END
GO
CREATE DATABASE BilliardsDB;
GO
USE BilliardsDB;
GO

CREATE TABLE BanBida (
    MaBan      INT IDENTITY(1,1) PRIMARY KEY,
    TenBan     NVARCHAR(50) NOT NULL,
    LoaiBan    NVARCHAR(30) NULL,
    GiaTheoGio DECIMAL(18,2) NOT NULL DEFAULT 0,
    TrangThai  NVARCHAR(20) NOT NULL DEFAULT N'Trống',
    CONSTRAINT CK_BanBida_TrangThai CHECK (TrangThai IN (N'Trống', N'Đang chơi'))
);

CREATE TABLE MenuDichVu (
    MaDV   INT IDENTITY(1,1) PRIMARY KEY,
    TenDV  NVARCHAR(100) NOT NULL,
    DonGia DECIMAL(18,2) NOT NULL DEFAULT 0,
    TonKho INT NOT NULL DEFAULT 0
);

CREATE TABLE HoaDon (
    MaHD        INT IDENTITY(1,1) PRIMARY KEY,
    MaBan       INT NOT NULL,
    GioBatDau   DATETIME NOT NULL,
    GioKetThuc  DATETIME NULL,
    TienGioChoi DECIMAL(18,2) NOT NULL DEFAULT 0,
    TienDichVu  DECIMAL(18,2) NOT NULL DEFAULT 0,
    TrangThai   BIT NOT NULL DEFAULT 0,
    CONSTRAINT FK_HoaDon_BanBida FOREIGN KEY (MaBan) REFERENCES BanBida (MaBan)
);

CREATE TABLE ChiTietHoaDon (
    MaHD    INT NOT NULL,
    MaDV    INT NOT NULL,
    SoLuong INT NOT NULL,
    DonGia  DECIMAL(18,2) NOT NULL DEFAULT 0,
    CONSTRAINT PK_ChiTietHoaDon PRIMARY KEY (MaHD, MaDV),
    CONSTRAINT FK_ChiTietHoaDon_HoaDon FOREIGN KEY (MaHD) REFERENCES HoaDon (MaHD) ON DELETE CASCADE,
    CONSTRAINT FK_ChiTietHoaDon_MenuDichVu FOREIGN KEY (MaDV) REFERENCES MenuDichVu (MaDV),
    CONSTRAINT CK_ChiTietHoaDon_SoLuong CHECK (SoLuong > 0)
);
GO

INSERT INTO BanBida (TenBan, LoaiBan, GiaTheoGio, TrangThai) VALUES
(N'Bàn Lỗ 01',      N'Pool (Lỗ)',     60000, N'Đang chơi'),
(N'Bàn Lỗ 02',      N'Pool (Lỗ)',     60000, N'Trống'),
(N'Bàn Lỗ 03',      N'Pool (Lỗ)',     60000, N'Trống'),
(N'Bàn Lỗ 04',      N'Pool (Lỗ)',     70000, N'Trống'),
(N'Bàn Carom 01',   N'Carom (Phăng)', 50000, N'Đang chơi'),
(N'Bàn Carom 02',   N'Carom (Phăng)', 50000, N'Trống'),
(N'Bàn Carom 03',   N'Carom (Phăng)', 50000, N'Trống'),
(N'Bàn Snooker 01', N'Snooker',       90000, N'Trống'),
(N'Bàn Snooker 02', N'Snooker',       90000, N'Trống');

INSERT INTO MenuDichVu (TenDV, DonGia, TonKho) VALUES
(N'Nước tăng lực Sting', 15000, 100),
(N'Trà xanh 0 độ',       12000, 80),
(N'Nước suối Aquafina',  10000, 120),
(N'Bia Tiger',           25000, 60),
(N'Cà phê sữa đá',       20000, 50),
(N'Mì xào bò',           45000, 30),
(N'Khô bò',              35000, 3),
(N'Thuê gậy Carbon',     30000, 10),
(N'Thuê gậy thường',     15000, 20);

INSERT INTO HoaDon (MaBan, GioBatDau, GioKetThuc, TienGioChoi, TienDichVu, TrangThai) VALUES
(1, DATEADD(MINUTE, -75, GETDATE()), NULL, 0, 0, 0),
(5, DATEADD(MINUTE, -40, GETDATE()), NULL, 0, 0, 0);

INSERT INTO HoaDon (MaBan, GioBatDau, GioKetThuc, TienGioChoi, TienDichVu, TrangThai) VALUES
(2, DATEADD(DAY, -1, GETDATE()), DATEADD(MINUTE, 90, DATEADD(DAY, -1, GETDATE())), 90000, 45000, 1);

INSERT INTO ChiTietHoaDon (MaHD, MaDV, SoLuong, DonGia) VALUES
(1, 1, 2, 15000),
(1, 8, 1, 30000),
(2, 4, 3, 25000),
(3, 1, 1, 15000),
(3, 9, 2, 15000);
GO

SELECT * FROM BanBida;
SELECT * FROM MenuDichVu;
SELECT * FROM HoaDon;
SELECT * FROM ChiTietHoaDon;
