
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace bai5._2
{
    internal sealed class RegistrationRepository
    {
        private const string DatabaseName = "CourseRegistrationDb";

        private const string MasterConnectionString =
            "Server=localhost;Port=3306;Uid=root;Pwd=familo23;";

        private static readonly string DatabaseConnectionString =
            "Server=localhost;Port=3306;Database=CourseRegistrationDb;Uid=root;Pwd=familo23;";

        public RegistrationRepository()
        {
            InitializeDatabase();
        }

        public List<Course> GetCourses()
        {
            var courses = new List<Course>();

            using (var connection = new MySqlConnection(DatabaseConnectionString))
            {
                const string sql =
                    "SELECT CourseId, CourseName FROM Courses ORDER BY CourseName";

                using (var command = new MySqlCommand(sql, connection))
                {
                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            courses.Add(
                                new Course(
                                    reader.GetInt32("CourseId"),
                                    reader.GetString("CourseName")
                                )
                            );
                        }
                    }
                }
            }

            return courses;
        }

        public void SaveRegistration(
            string fullName,
            string gender,
            int courseId,
            DateTime birthDate,
            string phone)
        {
            const string sql = @"
                INSERT INTO Registrations
                    (FullName, Gender, CourseId, BirthDate, Phone)
                VALUES
                    (@FullName, @Gender, @CourseId, @BirthDate, @Phone)";

            using (var connection = new MySqlConnection(DatabaseConnectionString))
            using (var command = new MySqlCommand(sql, connection))
            {
                command.Parameters.Add("@FullName", MySqlDbType.VarChar, 100)
                    .Value = fullName;

                command.Parameters.Add("@Gender", MySqlDbType.VarChar, 20)
                    .Value = gender;

                command.Parameters.Add("@CourseId", MySqlDbType.Int32)
                    .Value = courseId;

                command.Parameters.Add("@BirthDate", MySqlDbType.Date)
                    .Value = birthDate.Date;

                command.Parameters.Add("@Phone", MySqlDbType.VarChar, 20)
                    .Value = phone;

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        private static void InitializeDatabase()
        {
            using (var connection = new MySqlConnection(MasterConnectionString))
            using (var command = new MySqlCommand(
                "CREATE DATABASE IF NOT EXISTS `" + DatabaseName +
                "` CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci",
                connection))
            {
                connection.Open();
                command.ExecuteNonQuery();
            }

            const string initializeSql = @"
                CREATE TABLE IF NOT EXISTS Courses
                (
                    CourseId INT AUTO_INCREMENT PRIMARY KEY,
                    CourseName VARCHAR(100) NOT NULL UNIQUE
                ) ENGINE=InnoDB;

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

                INSERT IGNORE INTO Courses (CourseName)
                VALUES
                    ('Lập trình C#'),
                    ('Cơ sở dữ liệu SQL Server'),
                    ('Phát triển ứng dụng Windows Forms');
            ";

            using (var connection =
                new MySqlConnection(DatabaseConnectionString))
            using (var command =
                new MySqlCommand(initializeSql, connection))
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }

    internal sealed record Course(int Id, string Name);
}
