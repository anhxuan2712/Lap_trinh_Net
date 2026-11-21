USE master;
GO
IF DB_ID(N'LibraryDB') IS NOT NULL
BEGIN
    ALTER DATABASE LibraryDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE LibraryDB;
END
GO
CREATE DATABASE LibraryDB;
GO
USE LibraryDB;
GO

CREATE TABLE DocGia (
    MaDocGia      INT IDENTITY(1,1) PRIMARY KEY,
    HoTen         NVARCHAR(100) NOT NULL,
    Email         VARCHAR(100) NULL,
    NgayHetHanThe DATETIME NULL
);
CREATE UNIQUE INDEX UQ_DocGia_Email ON DocGia (Email) WHERE Email IS NOT NULL;

CREATE TABLE DauSach (
    MaSach     INT IDENTITY(1,1) PRIMARY KEY,
    TenSach    NVARCHAR(200) NOT NULL,
    TheLoai    NVARCHAR(50) NULL,
    SoLuongCon INT NOT NULL DEFAULT 0,
    CONSTRAINT CK_DauSach_SoLuongCon CHECK (SoLuongCon >= 0)
);

CREATE TABLE PhieuMuon (
    MaPhieu   INT IDENTITY(1,1) PRIMARY KEY,
    MaDocGia  INT NOT NULL,
    NgayMuon  DATETIME DEFAULT GETDATE(),
    HanTra    DATETIME NULL,
    TrangThai NVARCHAR(50) NOT NULL DEFAULT N'Đang mượn',
    CONSTRAINT FK_PhieuMuon_DocGia FOREIGN KEY (MaDocGia) REFERENCES DocGia (MaDocGia),
    CONSTRAINT CK_PhieuMuon_TrangThai CHECK (TrangThai IN (N'Đang mượn', N'Đã trả', N'Quá hạn'))
);

CREATE TABLE ChiTietMuon (
    MaPhieu INT NOT NULL,
    MaSach  INT NOT NULL,
    SoLuong INT NOT NULL,
    CONSTRAINT PK_ChiTietMuon PRIMARY KEY (MaPhieu, MaSach),
    CONSTRAINT FK_ChiTietMuon_PhieuMuon FOREIGN KEY (MaPhieu) REFERENCES PhieuMuon (MaPhieu) ON DELETE CASCADE,
    CONSTRAINT FK_ChiTietMuon_DauSach FOREIGN KEY (MaSach) REFERENCES DauSach (MaSach),
    CONSTRAINT CK_ChiTietMuon_SoLuong CHECK (SoLuong > 0)
);
GO

INSERT INTO DocGia (HoTen, Email, NgayHetHanThe) VALUES
(N'Nguyễn Văn An',  'an.nguyen@gmail.com', DATEADD(YEAR, 1, GETDATE())),
(N'Trần Thị Bình',  'binh.tran@gmail.com', DATEADD(MONTH, 6, GETDATE())),
(N'Lê Hoàng Cường', 'cuong.le@gmail.com',  DATEADD(YEAR, 2, GETDATE())),
(N'Phạm Minh Dũng', 'dung.pham@gmail.com', DATEADD(MONTH, -1, GETDATE())),
(N'Hoàng Thu Hà',   'ha.hoang@gmail.com',  DATEADD(MONTH, 3, GETDATE())),
(N'Vũ Đức Huy',     'huy.vu@gmail.com',    DATEADD(DAY, -10, GETDATE()));

INSERT INTO DauSach (TenSach, TheLoai, SoLuongCon) VALUES
(N'Lập trình C# căn bản',           N'Công nghệ thông tin', 10),
(N'Cấu trúc dữ liệu và giải thuật', N'Công nghệ thông tin', 7),
(N'Cơ sở dữ liệu MySQL',            N'Công nghệ thông tin', 5),
(N'Clean Code',                     N'Công nghệ thông tin', 4),
(N'Kinh tế học vĩ mô',              N'Kinh tế', 6),
(N'Nguyên lý kế toán',              N'Kinh tế', 8),
(N'Marketing căn bản',              N'Kinh tế', 3),
(N'Truyện Kiều',                    N'Văn học', 9),
(N'Số đỏ',                          N'Văn học', 5),
(N'Dế Mèn phiêu lưu ký',            N'Văn học', 12);

INSERT INTO PhieuMuon (MaDocGia, NgayMuon, HanTra, TrangThai) VALUES
(1, DATEADD(DAY, -5, GETDATE()),  DATEADD(DAY, 9, GETDATE()),   N'Đang mượn'),
(2, DATEADD(DAY, -30, GETDATE()), DATEADD(DAY, -16, GETDATE()), N'Đang mượn'),
(3, DATEADD(DAY, -40, GETDATE()), DATEADD(DAY, -26, GETDATE()), N'Đã trả'),
(5, DATEADD(DAY, -25, GETDATE()), DATEADD(DAY, -11, GETDATE()), N'Quá hạn'),
(1, DATEADD(DAY, -60, GETDATE()), DATEADD(DAY, -46, GETDATE()), N'Đã trả'),
(3, DATEADD(DAY, -2, GETDATE()),  DATEADD(DAY, 12, GETDATE()),  N'Đang mượn');

INSERT INTO ChiTietMuon (MaPhieu, MaSach, SoLuong) VALUES
(1, 1, 1), (1, 8, 1),
(2, 5, 1), (2, 2, 1),
(3, 9, 1),
(4, 3, 2), (4, 6, 1),
(5, 10, 1), (5, 1, 1),
(6, 4, 1), (6, 7, 1);
GO

SELECT * FROM DocGia;
SELECT * FROM DauSach;
SELECT * FROM PhieuMuon;
SELECT * FROM ChiTietMuon;
