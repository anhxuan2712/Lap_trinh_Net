using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace test2._3_chieu23_10
{
    public partial class frmStudentList : Form
    {
        private BindingList<StudentModel> studentList;
        private int selectedRowIndex = -1;

        public frmStudentList()
        {
            InitializeComponent();
        }

        private void frmStudentList_Load(object sender, EventArgs e)
        {
            InitializeGrid();
            LoadSampleData();
        }

        private void InitializeGrid()
        {
            dgvStudents.AutoGenerateColumns = false;
            dgvStudents.ReadOnly = true;
            dgvStudents.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvStudents.MultiSelect = false;
            dgvStudents.RowHeadersVisible = false;
            dgvStudents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvStudents.Columns.Clear();
            var colId = new DataGridViewTextBoxColumn { HeaderText = "Mã SV", DataPropertyName = "StudentId", FillWeight = 80 };
            var colName = new DataGridViewTextBoxColumn { HeaderText = "Họ tên", DataPropertyName = "FullName", FillWeight = 150 };
            var colDoB = new DataGridViewTextBoxColumn { HeaderText = "Ngày sinh", DataPropertyName = "DateOfBirth", FillWeight = 100 };
            var colGender = new DataGridViewTextBoxColumn { HeaderText = "Giới tính", DataPropertyName = "Gender", FillWeight = 80 };
            var colClass = new DataGridViewTextBoxColumn { HeaderText = "Lớp", DataPropertyName = "ClassName", FillWeight = 80 };
            var colMajor = new DataGridViewTextBoxColumn { HeaderText = "Chuyên ngành", DataPropertyName = "Major", FillWeight = 120 };
            var colGpa = new DataGridViewTextBoxColumn { HeaderText = "GPA", DataPropertyName = "Gpa", FillWeight = 70, DefaultCellStyle = { Format = "N2" } };
            dgvStudents.Columns.AddRange(colId, colName, colDoB, colGender, colClass, colMajor, colGpa);
        }

        private void LoadSampleData()
        {
            studentList = new BindingList<StudentModel>
            {
                new StudentModel { StudentId = GenerateStudentId(), FullName = "Nguyễn Văn A", DateOfBirth = "01/01/2000", Gender = "Nam", ClassName = "20A1", Major = "CNTT", Gpa = 3.2 },
                new StudentModel { StudentId = GenerateStudentId(), FullName = "Trần Thị B", DateOfBirth = "15/05/2001", Gender = "Nữ", ClassName = "20A2", Major = "Kinh tế", Gpa = 3.6 }
            };
            dgvStudents.DataSource = studentList;
            ClearForm();
        }

        private string GenerateStudentId()
        {
            int max = 0;
            foreach (var s in studentList ?? new BindingList<StudentModel>())
            {
                if (s.StudentId != null && s.StudentId.StartsWith("SV"))
                {
                    if (int.TryParse(s.StudentId.Substring(2), out int n) && n > max) max = n;
                }
            }
            return $"SV{max + 1:D3}";
        }

        private void ClearForm()
        {
            txtStudentId.Text = GenerateStudentId();
            txtFullName.Clear();
            txtDateOfBirth.Clear();
            cboGender.SelectedIndex = -1;
            txtClassName.Clear();
            txtMajor.Clear();
            txtGpa.Clear();
            selectedRowIndex = -1;
            dgvStudents.ClearSelection();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs(out double gpa)) return;
            var newStu = new StudentModel
            {
                StudentId = txtStudentId.Text.Trim(),
                FullName = txtFullName.Text.Trim(),
                DateOfBirth = txtDateOfBirth.Text.Trim(),
                Gender = cboGender.SelectedItem?.ToString() ?? string.Empty,
                ClassName = txtClassName.Text.Trim(),
                Major = txtMajor.Text.Trim(),
                Gpa = gpa
            };
            studentList.Add(newStu);
            ClearForm();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedRowIndex < 0) return;
            if (!ValidateInputs(out double gpa)) return;
            var stu = studentList[selectedRowIndex];
            stu.FullName = txtFullName.Text.Trim();
            stu.DateOfBirth = txtDateOfBirth.Text.Trim();
            stu.Gender = cboGender.SelectedItem?.ToString() ?? string.Empty;
            stu.ClassName = txtClassName.Text.Trim();
            stu.Major = txtMajor.Text.Trim();
            stu.Gpa = gpa;
            studentList.ResetItem(selectedRowIndex);
            ClearForm();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedRowIndex < 0) return;
            studentList.RemoveAt(selectedRowIndex);
            ClearForm();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void dgvStudents_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < studentList.Count)
            {
                selectedRowIndex = e.RowIndex;
                var stu = studentList[e.RowIndex];
                txtStudentId.Text = stu.StudentId;
                txtFullName.Text = stu.FullName;
                txtDateOfBirth.Text = stu.DateOfBirth;
                cboGender.SelectedItem = stu.Gender;
                txtClassName.Text = stu.ClassName;
                txtMajor.Text = stu.Major;
                txtGpa.Text = stu.Gpa.ToString("N2");
            }
        }

        private bool ValidateInputs(out double gpa)
        {
            gpa = 0;
            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Họ tên không được để trống.");
                return false;
            }
            if (!double.TryParse(txtGpa.Text.Replace(",", ""), NumberStyles.Any, CultureInfo.InvariantCulture, out gpa) || gpa < 0)
            {
                MessageBox.Show("GPA không hợp lệ.");
                return false;
            }
            return true;
        }
    }
}
