SET NAMES utf8mb4;

DROP DATABASE IF EXISTS LibraryDB;
CREATE DATABASE LibraryDB CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE LibraryDB;

CREATE TABLE DocGia (
    MaDocGia      INT AUTO_INCREMENT PRIMARY KEY,
    HoTen         VARCHAR(100) NOT NULL,
    Email         VARCHAR(100) NULL,
    NgayHetHanThe DATETIME NULL,
    CONSTRAINT UQ_DocGia_Email UNIQUE (Email)
) ENGINE = InnoDB;

CREATE TABLE DauSach (
    MaSach     INT AUTO_INCREMENT PRIMARY KEY,
    TenSach    VARCHAR(200) NOT NULL,
    TheLoai    VARCHAR(50) NULL,
    SoLuongCon INT NOT NULL DEFAULT 0,
    CONSTRAINT CK_DauSach_SoLuongCon CHECK (SoLuongCon >= 0)
) ENGINE = InnoDB;

CREATE TABLE PhieuMuon (
    MaPhieu   INT AUTO_INCREMENT PRIMARY KEY,
    MaDocGia  INT NOT NULL,
    NgayMuon  DATETIME DEFAULT CURRENT_TIMESTAMP,
    HanTra    DATETIME NULL,
    TrangThai VARCHAR(50) NOT NULL DEFAULT 'Đang mượn',
    CONSTRAINT FK_PhieuMuon_DocGia FOREIGN KEY (MaDocGia) REFERENCES DocGia (MaDocGia),
    CONSTRAINT CK_PhieuMuon_TrangThai CHECK (TrangThai IN ('Đang mượn', 'Đã trả', 'Quá hạn'))
) ENGINE = InnoDB;

CREATE TABLE ChiTietMuon (
    MaPhieu INT NOT NULL,
    MaSach  INT NOT NULL,
    SoLuong INT NOT NULL,
    CONSTRAINT PK_ChiTietMuon PRIMARY KEY (MaPhieu, MaSach),
    CONSTRAINT FK_ChiTietMuon_PhieuMuon FOREIGN KEY (MaPhieu) REFERENCES PhieuMuon (MaPhieu) ON DELETE CASCADE,
    CONSTRAINT FK_ChiTietMuon_DauSach FOREIGN KEY (MaSach) REFERENCES DauSach (MaSach),
    CONSTRAINT CK_ChiTietMuon_SoLuong CHECK (SoLuong > 0)
) ENGINE = InnoDB;

INSERT INTO DocGia (HoTen, Email, NgayHetHanThe) VALUES
('Nguyễn Văn An',   'an.nguyen@gmail.com',   DATE_ADD(NOW(), INTERVAL 1 YEAR)),
('Trần Thị Bình',   'binh.tran@gmail.com',   DATE_ADD(NOW(), INTERVAL 6 MONTH)),
('Lê Hoàng Cường',  'cuong.le@gmail.com',    DATE_ADD(NOW(), INTERVAL 2 YEAR)),
('Phạm Minh Dũng',  'dung.pham@gmail.com',   DATE_ADD(NOW(), INTERVAL -1 MONTH)),
('Hoàng Thu Hà',    'ha.hoang@gmail.com',    DATE_ADD(NOW(), INTERVAL 3 MONTH)),
('Vũ Đức Huy',      'huy.vu@gmail.com',      DATE_ADD(NOW(), INTERVAL -10 DAY));

INSERT INTO DauSach (TenSach, TheLoai, SoLuongCon) VALUES
('Lập trình C# căn bản',              'Công nghệ thông tin', 10),
('Cấu trúc dữ liệu và giải thuật',    'Công nghệ thông tin', 7),
('Cơ sở dữ liệu MySQL',               'Công nghệ thông tin', 5),
('Clean Code',                        'Công nghệ thông tin', 4),
('Kinh tế học vĩ mô',                 'Kinh tế', 6),
('Nguyên lý kế toán',                 'Kinh tế', 8),
('Marketing căn bản',                 'Kinh tế', 3),
('Truyện Kiều',                       'Văn học', 9),
('Số đỏ',                             'Văn học', 5),
('Dế Mèn phiêu lưu ký',               'Văn học', 12);

INSERT INTO PhieuMuon (MaDocGia, NgayMuon, HanTra, TrangThai) VALUES
(1, DATE_ADD(NOW(), INTERVAL -5 DAY),  DATE_ADD(NOW(), INTERVAL 9 DAY),   'Đang mượn'),
(2, DATE_ADD(NOW(), INTERVAL -30 DAY), DATE_ADD(NOW(), INTERVAL -16 DAY), 'Đang mượn'),
(3, DATE_ADD(NOW(), INTERVAL -40 DAY), DATE_ADD(NOW(), INTERVAL -26 DAY), 'Đã trả'),
(5, DATE_ADD(NOW(), INTERVAL -25 DAY), DATE_ADD(NOW(), INTERVAL -11 DAY), 'Quá hạn'),
(1, DATE_ADD(NOW(), INTERVAL -60 DAY), DATE_ADD(NOW(), INTERVAL -46 DAY), 'Đã trả'),
(3, DATE_ADD(NOW(), INTERVAL -2 DAY),  DATE_ADD(NOW(), INTERVAL 12 DAY),  'Đang mượn');

INSERT INTO ChiTietMuon (MaPhieu, MaSach, SoLuong) VALUES
(1, 1, 1), (1, 8, 1),
(2, 5, 1), (2, 2, 1),
(3, 9, 1),
(4, 3, 2), (4, 6, 1),
(5, 10, 1), (5, 1, 1),
(6, 4, 1), (6, 7, 1);

SELECT * FROM DocGia;
SELECT * FROM DauSach;
SELECT * FROM PhieuMuon;
SELECT * FROM ChiTietMuon;
