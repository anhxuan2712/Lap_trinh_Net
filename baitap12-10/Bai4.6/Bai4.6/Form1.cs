using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace Bai4._6
{
    public partial class Form1 : Form
    {
        private readonly BindingList<Student> _students = new();
        private readonly BindingSource _studentSource = new();

        public Form1()
        {
            InitializeComponent();
            ConfigureGrid();
            ConfigureBindings();
            WireEvents();

            _students.Add(new Student
            {
                StudentId = "SV001",
                FullName = "Nguyễn Văn An",
                Age = 20,
                Gender = "Nam"
            });
            _students.Add(new Student
            {
                StudentId = "SV002",
                FullName = "Trần Thị Bình",
                Age = 21,
                Gender = "Nữ"
            });
            _studentSource.Position = 0;
            UpdateAvatar();
        }

        private void ConfigureGrid()
        {
            dgvStudents.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(Student.StudentId),
                HeaderText = "Mã SV",
                Width = 120
            });
            dgvStudents.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(Student.FullName),
                HeaderText = "Họ tên",
                Width = 250
            });
            dgvStudents.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(Student.Age),
                HeaderText = "Tuổi",
                Width = 80
            });
            dgvStudents.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(Student.Gender),
                HeaderText = "Giới tính",
                Width = 120
            });
            dgvStudents.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(Student.AvatarPath),
                HeaderText = "AvatarPath",
                Visible = false
            });
        }

        private void ConfigureBindings()
        {
            _studentSource.DataSource = _students;
            dgvStudents.DataSource = _studentSource;

            txtStudentId.DataBindings.Add(nameof(TextBox.Text), _studentSource, nameof(Student.StudentId), true, DataSourceUpdateMode.OnPropertyChanged);
            txtFullName.DataBindings.Add(nameof(TextBox.Text), _studentSource, nameof(Student.FullName), true, DataSourceUpdateMode.OnPropertyChanged);
            txtAge.DataBindings.Add(nameof(TextBox.Text), _studentSource, nameof(Student.Age), true, DataSourceUpdateMode.OnPropertyChanged, 0, "0", CultureInfo.InvariantCulture);
            cboGender.DataSource = new[] { "Nam", "Nữ" };
            cboGender.DataBindings.Add(nameof(ComboBox.SelectedItem), _studentSource, nameof(Student.Gender), true, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void WireEvents()
        {
            _studentSource.PositionChanged += (_, _) => UpdateAvatar();
            _studentSource.CurrentChanged += (_, _) => UpdateAvatar();
            btnChooseAvatar.Click += ChooseAvatar_Click;
            btnAdd.Click += AddStudent_Click;
            btnDelete.Click += DeleteStudent_Click;
            btnUpdate.Click += UpdateStudent_Click;
            btnExportCsv.Click += ExportCsv_Click;
        }

        private Student? CurrentStudent => _studentSource.Current as Student;

        private void ChooseAvatar_Click(object? sender, EventArgs e)
        {
            if (CurrentStudent is null)
            {
                MessageBox.Show("Vui lòng chọn sinh viên trước.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using OpenFileDialog dialog = new()
            {
                Filter = "Ảnh (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp",
                Title = "Chọn ảnh đại diện"
            };

            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                CurrentStudent.AvatarPath = dialog.FileName;
                UpdateAvatar();
            }
        }

        private void AddStudent_Click(object? sender, EventArgs e)
        {
            if (!TryReadStudent(out Student? student))
            {
                return;
            }

            if (_students.Any(item => string.Equals(item.StudentId, student.StudentId, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("Mã sinh viên đã tồn tại.", "Dữ liệu không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _students.Add(student);
            _studentSource.Position = _students.Count - 1;
        }

        private void DeleteStudent_Click(object? sender, EventArgs e)
        {
            if (CurrentStudent is not Student student)
            {
                return;
            }

            DialogResult result = MessageBox.Show(
                $"Bạn có chắc muốn xóa sinh viên {student.FullName} không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                int position = _studentSource.Position;
                _students.Remove(student);
                if (_students.Count > 0)
                {
                    _studentSource.Position = Math.Min(position, _students.Count - 1);
                }
                else
                {
                    UpdateAvatar();
                }
            }
        }

        private void UpdateStudent_Click(object? sender, EventArgs e)
        {
            if (CurrentStudent is null)
            {
                MessageBox.Show("Vui lòng chọn sinh viên cần cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!TryReadStudent(out Student? values))
            {
                return;
            }

            if (_students.Any(item => !ReferenceEquals(item, CurrentStudent) && string.Equals(item.StudentId, values.StudentId, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("Mã sinh viên đã tồn tại.", "Dữ liệu không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CurrentStudent.StudentId = values.StudentId;
            CurrentStudent.FullName = values.FullName;
            CurrentStudent.Age = values.Age;
            CurrentStudent.Gender = values.Gender;
            _studentSource.EndEdit();
            dgvStudents.Refresh();
            MessageBox.Show("Đã cập nhật thông tin sinh viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private bool TryReadStudent(out Student student)
        {
            student = new Student();
            string studentId = txtStudentId.Text.Trim();
            string fullName = txtFullName.Text.Trim();
            if (string.IsNullOrWhiteSpace(studentId) || string.IsNullOrWhiteSpace(fullName))
            {
                MessageBox.Show("Mã sinh viên và họ tên không được để trống.", "Dữ liệu không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!int.TryParse(txtAge.Text, NumberStyles.Integer, CultureInfo.InvariantCulture, out int age) || age < 1 || age > 150)
            {
                MessageBox.Show("Tuổi phải là số nguyên từ 1 đến 150.", "Dữ liệu không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            student.StudentId = studentId;
            student.FullName = fullName;
            student.Age = age;
            student.Gender = cboGender.SelectedItem?.ToString() ?? "Nam";
            student.AvatarPath = CurrentStudent?.AvatarPath ?? string.Empty;
            return true;
        }

        private void UpdateAvatar()
        {
            Image? previousImage = picAvatar.Image;
            picAvatar.Image = LoadAvatar(CurrentStudent?.AvatarPath);
            previousImage?.Dispose();
        }

        private static Image? LoadAvatar(string? path)
        {
            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
            {
                return null;
            }

            try
            {
                using Image source = Image.FromFile(path);
                return new Bitmap(source);
            }
            catch (Exception) when (true)
            {
                return null;
            }
        }

        private void ExportCsv_Click(object? sender, EventArgs e)
        {
            using SaveFileDialog dialog = new()
            {
                Filter = "CSV UTF-8 (*.csv)|*.csv|Tất cả tệp (*.*)|*.*",
                DefaultExt = "csv",
                AddExtension = true,
                FileName = "students.csv",
                Title = "Xuất danh sách sinh viên"
            };

            if (dialog.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            try
            {
                using StreamWriter writer = new(dialog.FileName, false, new UTF8Encoding(true));
                writer.WriteLine("StudentId,FullName,Age,Gender,AvatarPath");
                foreach (Student student in _students)
                {
                    writer.WriteLine(string.Join(",",
                        CsvValue(student.StudentId),
                        CsvValue(student.FullName),
                        student.Age.ToString(CultureInfo.InvariantCulture),
                        CsvValue(student.Gender),
                        CsvValue(student.AvatarPath)));
                }

                MessageBox.Show("Đã xuất file CSV thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (IOException exception)
            {
                MessageBox.Show($"Không thể ghi file: {exception.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static string CsvValue(string? value)
        {
            string text = value ?? string.Empty;
            return $"\"{text.Replace("\"", "\"\"")}\"";
        }
    }
}
