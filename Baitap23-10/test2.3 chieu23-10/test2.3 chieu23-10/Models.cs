using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace test2._3_chieu23_10
{
    public class StudentModel
    {
        public string StudentId { get; set; }
        public string FullName { get; set; }
        public string DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string ClassName { get; set; }
        public string Major { get; set; }
        public double Gpa { get; set; }
    }

    public class CourseModel
    {
        public string CourseId { get; set; }
        public string CourseName { get; set; }
        public int Credits { get; set; }
        public string Department { get; set; }
        public string Instructor { get; set; }
        public int MaxStudents { get; set; }
    }
}
