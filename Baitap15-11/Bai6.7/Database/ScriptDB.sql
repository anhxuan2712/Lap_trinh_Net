SET NAMES utf8mb4;

DROP DATABASE IF EXISTS ClinicDB;
CREATE DATABASE ClinicDB CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE ClinicDB;

CREATE TABLE BenhNhan (
    MaBN        INT AUTO_INCREMENT PRIMARY KEY,
    HoTen       VARCHAR(100) NOT NULL,
    SoDienThoai VARCHAR(15) NULL,
    NamSinh     INT NULL,
    CONSTRAINT UQ_BenhNhan_SoDienThoai UNIQUE (SoDienThoai)
) ENGINE = InnoDB;

CREATE TABLE HoSoKham (
    MaHoSo     INT AUTO_INCREMENT PRIMARY KEY,
    MaBN       INT NOT NULL,
    NgayKham   DATETIME DEFAULT CURRENT_TIMESTAMP,
    TrieuChung LONGTEXT NULL,
    ChanDoan   VARCHAR(250) NULL,
    CONSTRAINT FK_HoSoKham_BenhNhan FOREIGN KEY (MaBN) REFERENCES BenhNhan (MaBN)
) ENGINE = InnoDB;

CREATE TABLE Thuoc (
    MaThuoc    INT AUTO_INCREMENT PRIMARY KEY,
    TenThuoc   VARCHAR(150) NOT NULL,
    DonViTinh  VARCHAR(20) NULL,
    DonGia     DECIMAL(18,2) NOT NULL DEFAULT 0,
    SoLuongTon INT NOT NULL DEFAULT 0,
    CONSTRAINT CK_Thuoc_SoLuongTon CHECK (SoLuongTon >= 0)
) ENGINE = InnoDB;

CREATE TABLE DonThuoc (
    MaDon     INT AUTO_INCREMENT PRIMARY KEY,
    MaHoSo    INT NOT NULL,
    TienKham  DECIMAL(18,2) NOT NULL DEFAULT 0,
    TongTien  DECIMAL(18,2) NOT NULL DEFAULT 0,
    TrangThai VARCHAR(30) NOT NULL DEFAULT 'Chờ lấy thuốc',
    CONSTRAINT FK_DonThuoc_HoSoKham FOREIGN KEY (MaHoSo) REFERENCES HoSoKham (MaHoSo)
) ENGINE = InnoDB;

CREATE TABLE ChiTietDonThuoc (
    MaDon    INT NOT NULL,
    MaThuoc  INT NOT NULL,
    SoLuong  INT NOT NULL,
    LieuDung VARCHAR(100) NULL,
    CONSTRAINT PK_ChiTietDonThuoc PRIMARY KEY (MaDon, MaThuoc),
    CONSTRAINT FK_ChiTietDonThuoc_DonThuoc FOREIGN KEY (MaDon) REFERENCES DonThuoc (MaDon) ON DELETE CASCADE,
    CONSTRAINT FK_ChiTietDonThuoc_Thuoc FOREIGN KEY (MaThuoc) REFERENCES Thuoc (MaThuoc),
    CONSTRAINT CK_ChiTietDonThuoc_SoLuong CHECK (SoLuong > 0)
) ENGINE = InnoDB;

INSERT INTO BenhNhan (HoTen, SoDienThoai, NamSinh) VALUES
('Nguyễn Văn Nam',   '0901234567', 1985),
('Trần Thị Lan',     '0912345678', 1992),
('Lê Văn Phúc',      '0987654321', 1970),
('Phạm Thị Mai',     '0933444555', 2001),
('Đỗ Quang Huy',     '0977888999', 1998);

INSERT INTO Thuoc (TenThuoc, DonViTinh, DonGia, SoLuongTon) VALUES
('Paracetamol 500mg',        'Viên', 1000,   500),
('Amoxicillin 500mg',        'Viên', 2500,   300),
('Vitamin C 500mg',          'Viên', 800,    400),
('Berberin',                 'Vỉ',   12000,  50),
('Siro ho Prospan 100ml',    'Chai', 85000,  20),
('Omeprazol 20mg',           'Viên', 3000,   200),
('Oresol',                   'Gói',  2000,   150),
('Nước muối sinh lý 0.9%',   'Chai', 5000,   5);

INSERT INTO HoSoKham (MaBN, NgayKham, TrieuChung, ChanDoan) VALUES
(1, DATE_ADD(NOW(), INTERVAL -3 DAY), 'Sốt cao 39 độ, đau đầu, mệt mỏi', 'Sốt siêu vi'),
(2, DATE_ADD(NOW(), INTERVAL -1 DAY), 'Ho nhiều về đêm, đau họng', 'Viêm họng cấp'),
(3, NOW(), 'Đau thượng vị, ợ chua sau ăn', NULL),
(4, NOW(), 'Tiêu chảy 3 lần/ngày, đau bụng nhẹ', NULL);

INSERT INTO DonThuoc (MaHoSo, TienKham, TongTien, TrangThai) VALUES
(1, 100000, 100000 + 10*1000 + 10*800,   'Đã thanh toán'),
(2, 100000, 100000 + 14*2500 + 1*85000,  'Chờ lấy thuốc');

INSERT INTO ChiTietDonThuoc (MaDon, MaThuoc, SoLuong, LieuDung) VALUES
(1, 1, 10, 'Uống 1 viên khi sốt trên 38.5 độ, cách 4-6 giờ'),
(1, 3, 10, 'Sáng 1 viên sau ăn'),
(2, 2, 14, 'Sáng 1 viên, Chiều 1 viên sau ăn'),
(2, 5, 1,  'Ngày 3 lần, mỗi lần 5ml');

SELECT * FROM BenhNhan;
SELECT * FROM HoSoKham;
SELECT * FROM Thuoc;
SELECT * FROM DonThuoc;
SELECT * FROM ChiTietDonThuoc;
