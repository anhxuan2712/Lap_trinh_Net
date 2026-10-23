using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace test2._3_chieu23_10
{
    public partial class frmCourseList : Form
    {
        private BindingList<CourseModel> courseList;
        private int selectedRowIndex = -1;

        public frmCourseList()
        {
            InitializeComponent();
        }

        private void frmCourseList_Load(object sender, EventArgs e)
        {
            InitializeGrid();
            LoadSampleData();
        }

        private void InitializeGrid()
        {
            dgvCourses.AutoGenerateColumns = false;
            dgvCourses.ReadOnly = true;
            dgvCourses.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCourses.MultiSelect = false;
            dgvCourses.RowHeadersVisible = false;
            dgvCourses.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvCourses.Columns.Clear();
            var colId = new DataGridViewTextBoxColumn { HeaderText = "Mã môn", DataPropertyName = "CourseId", FillWeight = 80 };
            var colName = new DataGridViewTextBoxColumn { HeaderText = "Tên môn học", DataPropertyName = "CourseName", FillWeight = 160 };
            var colCredits = new DataGridViewTextBoxColumn { HeaderText = "Số tín chỉ", DataPropertyName = "Credits", FillWeight = 70 };
            var colDept = new DataGridViewTextBoxColumn { HeaderText = "Khoa", DataPropertyName = "Department", FillWeight = 130 };
            var colInst = new DataGridViewTextBoxColumn { HeaderText = "Giảng viên", DataPropertyName = "Instructor", FillWeight = 120 };
            var colMax = new DataGridViewTextBoxColumn { HeaderText = "Sĩ số max", DataPropertyName = "MaxStudents", FillWeight = 70 };

            dgvCourses.Columns.AddRange(colId, colName, colCredits, colDept, colInst, colMax);
        }

        private void LoadSampleData()
        {
            courseList = new BindingList<CourseModel>
            {
                new CourseModel { CourseId = "MH001", CourseName = "Lập trình Windows Forms", Credits = 3, Department = "Công nghệ thông tin", Instructor = "ThS. Hoàng Văn C", MaxStudents = 60 },
                new CourseModel { CourseId = "MH002", CourseName = "Cơ sở dữ liệu nâng cao", Credits = 4, Department = "Công nghệ thông tin", Instructor = "TS. Lê Đình D", MaxStudents = 50 },
                new CourseModel { CourseId = "MH003", CourseName = "Quản trị học đại cương", Credits = 3, Department = "Kinh tế & Quản trị", Instructor = "ThS. Phạm Thu E", MaxStudents = 80 }
            };

            dgvCourses.DataSource = courseList;
            ClearForm();
        }

        private string GenerateCourseId()
        {
            int max = 0;
            foreach (var c in courseList ?? new BindingList<CourseModel>())
            {
                if (c.CourseId != null && c.CourseId.StartsWith("MH"))
                {
                    int n;
                    if (int.TryParse(c.CourseId.Substring(2), out n) && n > max)
                    {
                        max = n;
                    }
                }
            }
            return string.Format("MH{0:D3}", max + 1);
        }

        private void ClearForm()
        {
            txtCourseId.Text = GenerateCourseId();
            txtCourseName.Clear();
            numCredits.Value = 3;
            cboDepartment.SelectedIndex = cboDepartment.Items.Count > 0 ? 0 : -1;
            txtInstructor.Clear();
            numMaxStudents.Value = 60;
            selectedRowIndex = -1;
            dgvCourses.ClearSelection();
            txtCourseName.Focus();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCourseName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên môn học!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCourseName.Focus();
                return;
            }

            var course = new CourseModel
            {
                CourseId = GenerateCourseId(),
                CourseName = txtCourseName.Text.Trim(),
                Credits = (int)numCredits.Value,
                Department = cboDepartment.SelectedItem != null ? cboDepartment.SelectedItem.ToString() : string.Empty,
                Instructor = txtInstructor.Text.Trim(),
                MaxStudents = (int)numMaxStudents.Value
            };

            courseList.Add(course);
            ClearForm();
            MessageBox.Show("Thêm môn học thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedRowIndex < 0 || selectedRowIndex >= courseList.Count)
            {
                MessageBox.Show("Vui lòng chọn môn học cần cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtCourseName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên môn học!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCourseName.Focus();
                return;
            }

            var course = courseList[selectedRowIndex];
            course.CourseName = txtCourseName.Text.Trim();
            course.Credits = (int)numCredits.Value;
            course.Department = cboDepartment.SelectedItem != null ? cboDepartment.SelectedItem.ToString() : string.Empty;
            course.Instructor = txtInstructor.Text.Trim();
            course.MaxStudents = (int)numMaxStudents.Value;

            courseList.ResetItem(selectedRowIndex);
            ClearForm();
            MessageBox.Show("Cập nhật môn học thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedRowIndex < 0 || selectedRowIndex >= courseList.Count)
            {
                MessageBox.Show("Vui lòng chọn môn học cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var course = courseList[selectedRowIndex];
            var result = MessageBox.Show(
                string.Format("Bạn có chắc chắn muốn xóa môn [{0}] {1}?", course.CourseId, course.CourseName),
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                courseList.RemoveAt(selectedRowIndex);
                ClearForm();
                MessageBox.Show("Xóa môn học thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void dgvCourses_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < courseList.Count)
            {
                selectedRowIndex = e.RowIndex;
                var course = courseList[e.RowIndex];
                txtCourseId.Text = course.CourseId;
                txtCourseName.Text = course.CourseName;
                numCredits.Value = Math.Max(numCredits.Minimum, Math.Min(numCredits.Maximum, course.Credits));
                cboDepartment.SelectedItem = course.Department;
                txtInstructor.Text = course.Instructor;
                numMaxStudents.Value = Math.Max(numMaxStudents.Minimum, Math.Min(numMaxStudents.Maximum, course.MaxStudents));
            }
        }
    }
}
