SET NAMES utf8mb4;

DROP DATABASE IF EXISTS BilliardsDB;
CREATE DATABASE BilliardsDB CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE BilliardsDB;

CREATE TABLE BanBida (
    MaBan      INT AUTO_INCREMENT PRIMARY KEY,
    TenBan     VARCHAR(50) NOT NULL,
    LoaiBan    VARCHAR(30) NULL,
    GiaTheoGio DECIMAL(18,2) NOT NULL DEFAULT 0,
    TrangThai  VARCHAR(20) NOT NULL DEFAULT 'Trống',
    CONSTRAINT CK_BanBida_TrangThai CHECK (TrangThai IN ('Trống', 'Đang chơi'))
) ENGINE = InnoDB;

CREATE TABLE MenuDichVu (
    MaDV   INT AUTO_INCREMENT PRIMARY KEY,
    TenDV  VARCHAR(100) NOT NULL,
    DonGia DECIMAL(18,2) NOT NULL DEFAULT 0,
    TonKho INT NOT NULL DEFAULT 0
) ENGINE = InnoDB;

CREATE TABLE HoaDon (
    MaHD        INT AUTO_INCREMENT PRIMARY KEY,
    MaBan       INT NOT NULL,
    GioBatDau   DATETIME NOT NULL,
    GioKetThuc  DATETIME NULL,
    TienGioChoi DECIMAL(18,2) NOT NULL DEFAULT 0,
    TienDichVu  DECIMAL(18,2) NOT NULL DEFAULT 0,
    TrangThai   BIT(1) NOT NULL DEFAULT b'0',
    CONSTRAINT FK_HoaDon_BanBida FOREIGN KEY (MaBan) REFERENCES BanBida (MaBan)
) ENGINE = InnoDB;

CREATE TABLE ChiTietHoaDon (
    MaHD    INT NOT NULL,
    MaDV    INT NOT NULL,
    SoLuong INT NOT NULL,
    DonGia  DECIMAL(18,2) NOT NULL DEFAULT 0,
    CONSTRAINT PK_ChiTietHoaDon PRIMARY KEY (MaHD, MaDV),
    CONSTRAINT FK_ChiTietHoaDon_HoaDon FOREIGN KEY (MaHD) REFERENCES HoaDon (MaHD) ON DELETE CASCADE,
    CONSTRAINT FK_ChiTietHoaDon_MenuDichVu FOREIGN KEY (MaDV) REFERENCES MenuDichVu (MaDV),
    CONSTRAINT CK_ChiTietHoaDon_SoLuong CHECK (SoLuong > 0)
) ENGINE = InnoDB;

INSERT INTO BanBida (TenBan, LoaiBan, GiaTheoGio, TrangThai) VALUES
('Bàn Lỗ 01',     'Pool (Lỗ)',      60000,  'Đang chơi'),
('Bàn Lỗ 02',     'Pool (Lỗ)',      60000,  'Trống'),
('Bàn Lỗ 03',     'Pool (Lỗ)',      60000,  'Trống'),
('Bàn Lỗ 04',     'Pool (Lỗ)',      70000,  'Trống'),
('Bàn Carom 01',  'Carom (Phăng)',  50000,  'Đang chơi'),
('Bàn Carom 02',  'Carom (Phăng)',  50000,  'Trống'),
('Bàn Carom 03',  'Carom (Phăng)',  50000,  'Trống'),
('Bàn Snooker 01','Snooker',        90000,  'Trống'),
('Bàn Snooker 02','Snooker',        90000,  'Trống');

INSERT INTO MenuDichVu (TenDV, DonGia, TonKho) VALUES
('Nước tăng lực Sting',  15000, 100),
('Trà xanh 0 độ',        12000, 80),
('Nước suối Aquafina',   10000, 120),
('Bia Tiger',            25000, 60),
('Cà phê sữa đá',        20000, 50),
('Mì xào bò',            45000, 30),
('Khô bò',               35000, 3),
('Thuê gậy Carbon',      30000, 10),
('Thuê gậy thường',      15000, 20);

INSERT INTO HoaDon (MaBan, GioBatDau, GioKetThuc, TienGioChoi, TienDichVu, TrangThai) VALUES
(1, DATE_ADD(NOW(), INTERVAL -75 MINUTE), NULL, 0, 0, b'0'),
(5, DATE_ADD(NOW(), INTERVAL -40 MINUTE), NULL, 0, 0, b'0');

INSERT INTO HoaDon (MaBan, GioBatDau, GioKetThuc, TienGioChoi, TienDichVu, TrangThai) VALUES
(2, DATE_ADD(NOW(), INTERVAL -1 DAY), DATE_ADD(DATE_ADD(NOW(), INTERVAL -1 DAY), INTERVAL 90 MINUTE), 90000, 45000, b'1');

INSERT INTO ChiTietHoaDon (MaHD, MaDV, SoLuong, DonGia) VALUES
(1, 1, 2, 15000),
(1, 8, 1, 30000),
(2, 4, 3, 25000),
(3, 1, 1, 15000),
(3, 9, 2, 15000);

SELECT * FROM BanBida;
SELECT * FROM MenuDichVu;
SELECT MaHD, MaBan, GioBatDau, GioKetThuc, TienGioChoi, TienDichVu, TrangThai + 0 AS TrangThai FROM HoaDon;
SELECT * FROM ChiTietHoaDon;
