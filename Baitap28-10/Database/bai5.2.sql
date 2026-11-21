
CREATE DATABASE IF NOT EXISTS CourseRegistrationDb
CHARACTER SET utf8mb4
COLLATE utf8mb4_unicode_ci;

USE CourseRegistrationDb;

-- 1. Bảng khóa học
CREATE TABLE IF NOT EXISTS Courses
(
    CourseId INT AUTO_INCREMENT PRIMARY KEY,
    CourseName VARCHAR(100) NOT NULL UNIQUE
) ENGINE=InnoDB;

-- 2. Bảng đăng ký
CREATE TABLE IF NOT EXISTS Registrations
(
    RegistrationId INT AUTO_INCREMENT PRIMARY KEY,
    FullName VARCHAR(100) NOT NULL,
    Gender VARCHAR(20) NOT NULL,
    CourseId INT NOT NULL,
    BirthDate DATE NOT NULL,
    Phone VARCHAR(20) NOT NULL,
    RegisteredAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT FK_Registrations_Courses
        FOREIGN KEY (CourseId)
        REFERENCES Courses(CourseId)
) ENGINE=InnoDB;

-- 3. Thêm dữ liệu khóa học mẫu
INSERT IGNORE INTO Courses (CourseName)
VALUES
    ('Lập trình C#'),
    ('Cơ sở dữ liệu SQL Server'),
    ('Phát triển ứng dụng Windows Forms');

-- 4. Kiểm tra danh sách khóa học
SELECT * FROM Courses;

-- 5. Kiểm tra dữ liệu đăng ký
SELECT
    r.RegistrationId,
    r.FullName,
    r.Gender,
    c.CourseName,
    r.BirthDate,
    r.Phone,
    r.RegisteredAt
FROM Registrations r
INNER JOIN Courses c
    ON r.CourseId = c.CourseId
ORDER BY r.RegistrationId DESC;
