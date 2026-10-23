using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace test2._2_chieu_23_10
{
    public partial class frmEmployeeManager : Form
    {
        private BindingList<EmployeeModel> employeeList;
        private string selectedAvatarPath = string.Empty;
        private int selectedRowIndex = -1;

        public frmEmployeeManager()
        {
            InitializeComponent();
        }

        private void frmEmployeeManager_Load(object sender, EventArgs e)
        {
            InitializeGrid();
            InitializeData();
            timerClock.Start();
            UpdateStatusStrip();
        }

        private void InitializeGrid()
        {
            dgvEmployees.AutoGenerateColumns = false;
            dgvEmployees.ReadOnly = true;
            dgvEmployees.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEmployees.MultiSelect = false;
            dgvEmployees.AllowUserToAddRows = false;
            dgvEmployees.AllowUserToDeleteRows = false;
            dgvEmployees.RowHeadersVisible = false;
            dgvEmployees.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvEmployees.Columns.Clear();

            DataGridViewTextBoxColumn colId = new DataGridViewTextBoxColumn
            {
                Name = "colEmployeeId",
                HeaderText = "Mã NV",
                DataPropertyName = "EmployeeId",
                FillWeight = 75
            };

            DataGridViewTextBoxColumn colName = new DataGridViewTextBoxColumn
            {
                Name = "colFullName",
                HeaderText = "Họ tên",
                DataPropertyName = "FullName",
                FillWeight = 150
            };

            DataGridViewTextBoxColumn colDept = new DataGridViewTextBoxColumn
            {
                Name = "colDepartment",
                HeaderText = "Phòng ban",
                DataPropertyName = "Department",
                FillWeight = 100
            };

            DataGridViewTextBoxColumn colPos = new DataGridViewTextBoxColumn
            {
                Name = "colPosition",
                HeaderText = "Chức vụ",
                DataPropertyName = "Position",
                FillWeight = 100
            };

            DataGridViewTextBoxColumn colBase = new DataGridViewTextBoxColumn
            {
                Name = "colBaseSalary",
                HeaderText = "Lương CB",
                DataPropertyName = "BaseSalary",
                FillWeight = 100
            };
            colBase.DefaultCellStyle.Format = "N0";

            DataGridViewTextBoxColumn colAllow = new DataGridViewTextBoxColumn
            {
                Name = "colAllowance",
                HeaderText = "Phụ cấp",
                DataPropertyName = "Allowance",
                FillWeight = 90
            };
            colAllow.DefaultCellStyle.Format = "N0";

            DataGridViewTextBoxColumn colTotal = new DataGridViewTextBoxColumn
            {
                Name = "colTotalSalary",
                HeaderText = "Tổng thu nhập",
                DataPropertyName = "TotalSalary",
                FillWeight = 115
            };
            colTotal.DefaultCellStyle.Format = "N0";

            DataGridViewTextBoxColumn colAvatar = new DataGridViewTextBoxColumn
            {
                Name = "colAvatarPath",
                HeaderText = "Ảnh (tên file)",
                DataPropertyName = "AvatarPath",
                FillWeight = 100
            };

            dgvEmployees.Columns.AddRange(new DataGridViewColumn[]
            {
                colId, colName, colDept, colPos, colBase, colAllow, colTotal, colAvatar
            });
        }

        private void InitializeData()
        {
            employeeList = new BindingList<EmployeeModel>
            {
                new EmployeeModel
                {
                    EmployeeId = "NV001",
                    FullName = "Nguyễn Văn An",
                    Department = "Kinh doanh",
                    Position = "Trưởng phòng",
                    BaseSalary = 15000000,
                    Allowance = 3000000,
                    AvatarPath = string.Empty
                },
                new EmployeeModel
                {
                    EmployeeId = "NV002",
                    FullName = "Trần Thị Bình",
                    Department = "Kế toán",
                    Position = "Nhân viên",
                    BaseSalary = 10000000,
                    Allowance = 1500000,
                    AvatarPath = string.Empty
                }
            };

            dgvEmployees.DataSource = employeeList;
            ClearForm();
        }

        private string GenerateEmployeeId()
        {
            int maxId = 0;
            foreach (EmployeeModel emp in employeeList)
            {
                if (!string.IsNullOrEmpty(emp.EmployeeId) && emp.EmployeeId.StartsWith("NV"))
                {
                    int num;
                    if (int.TryParse(emp.EmployeeId.Substring(2), out num))
                    {
                        if (num > maxId)
                        {
                            maxId = num;
                        }
                    }
                }
            }
            return string.Format("NV{0:D3}", maxId + 1);
        }

        private void ClearForm()
        {
            txtEmployeeId.Text = GenerateEmployeeId();
            txtFullName.Clear();
            cboDepartment.SelectedIndex = cboDepartment.Items.Count > 0 ? 0 : -1;
            txtPosition.Clear();
            txtBaseSalary.Clear();
            txtAllowance.Clear();
            txtTotalSalary.Clear();
            picAvatar.Image = null;
            picAvatar.BackColor = Color.WhiteSmoke;
            selectedAvatarPath = string.Empty;
            selectedRowIndex = -1;
            dgvEmployees.ClearSelection();
            txtFullName.Focus();
        }

        private void DisplayEmployee(EmployeeModel emp)
        {
            txtEmployeeId.Text = emp.EmployeeId;
            txtFullName.Text = emp.FullName;
            cboDepartment.SelectedItem = emp.Department;
            txtPosition.Text = emp.Position;
            txtBaseSalary.Text = emp.BaseSalary.ToString("N0");
            txtAllowance.Text = emp.Allowance.ToString("N0");
            txtTotalSalary.Text = emp.TotalSalary.ToString("N0");
            selectedAvatarPath = emp.AvatarPath ?? string.Empty;

            if (!string.IsNullOrEmpty(emp.AvatarPath) && File.Exists(emp.AvatarPath))
            {
                try
                {
                    picAvatar.Image = Image.FromFile(emp.AvatarPath);
                }
                catch
                {
                    picAvatar.Image = null;
                    picAvatar.BackColor = Color.WhiteSmoke;
                }
            }
            else
            {
                picAvatar.Image = null;
                picAvatar.BackColor = Color.WhiteSmoke;
            }
        }

        private bool ParseInputs(out decimal baseSalary, out decimal allowance)
        {
            baseSalary = 0;
            allowance = 0;

            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Họ tên không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFullName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPosition.Text))
            {
                MessageBox.Show("Chức vụ không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPosition.Focus();
                return false;
            }

            string rawBase = txtBaseSalary.Text.Replace(",", "").Replace(".", "").Trim();
            if (!decimal.TryParse(rawBase, NumberStyles.Any, CultureInfo.InvariantCulture, out baseSalary) || baseSalary < 0)
            {
                MessageBox.Show("Lương cơ bản phải là số không âm!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBaseSalary.Focus();
                return false;
            }

            string rawAllow = txtAllowance.Text.Replace(",", "").Replace(".", "").Trim();
            if (string.IsNullOrWhiteSpace(rawAllow))
            {
                allowance = 0;
            }
            else if (!decimal.TryParse(rawAllow, NumberStyles.Any, CultureInfo.InvariantCulture, out allowance) || allowance < 0)
            {
                MessageBox.Show("Phụ cấp phải là số không âm!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAllowance.Focus();
                return false;
            }

            return true;
        }

        private void UpdateStatusStrip()
        {
            int total = employeeList.Count;
            decimal totalSalary = 0;
            foreach (EmployeeModel emp in employeeList)
            {
                totalSalary += emp.TotalSalary;
            }
            lblStatusTotal.Text = string.Format("Tổng nhân viên: {0}", total);
            lblStatusSalary.Text = string.Format("Tổng quỹ lương: {0:N0} đ", totalSalary);
        }

        private void timerClock_Tick(object sender, EventArgs e)
        {
            lblStatusTime.Text = DateTime.Now.ToString("HH:mm:ss  dd/MM/yyyy");
        }

        private void dgvEmployees_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < employeeList.Count)
            {
                selectedRowIndex = e.RowIndex;
                DisplayEmployee(employeeList[e.RowIndex]);
            }
        }

        private void btnChooseAvatar_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Chọn ảnh chân dung nhân viên";
                ofd.Filter = "Ảnh (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp";
                ofd.FilterIndex = 1;

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    selectedAvatarPath = ofd.FileName;
                    try
                    {
                        picAvatar.Image = Image.FromFile(ofd.FileName);
                        picAvatar.BackColor = Color.Transparent;
                    }
                    catch
                    {
                        MessageBox.Show("Không thể nạp ảnh! Vui lòng chọn file ảnh hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        selectedAvatarPath = string.Empty;
                    }
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            decimal baseSalary, allowance;
            if (!ParseInputs(out baseSalary, out allowance))
            {
                return;
            }

            EmployeeModel newEmp = new EmployeeModel
            {
                EmployeeId = GenerateEmployeeId(),
                FullName = txtFullName.Text.Trim(),
                Department = cboDepartment.SelectedItem != null ? cboDepartment.SelectedItem.ToString() : string.Empty,
                Position = txtPosition.Text.Trim(),
                BaseSalary = baseSalary,
                Allowance = allowance,
                AvatarPath = selectedAvatarPath
            };

            employeeList.Add(newEmp);
            UpdateStatusStrip();
            ClearForm();
            MessageBox.Show("Thêm nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedRowIndex < 0 || selectedRowIndex >= employeeList.Count)
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal baseSalary, allowance;
            if (!ParseInputs(out baseSalary, out allowance))
            {
                return;
            }

            EmployeeModel emp = employeeList[selectedRowIndex];
            emp.FullName = txtFullName.Text.Trim();
            emp.Department = cboDepartment.SelectedItem != null ? cboDepartment.SelectedItem.ToString() : string.Empty;
            emp.Position = txtPosition.Text.Trim();
            emp.BaseSalary = baseSalary;
            emp.Allowance = allowance;
            emp.AvatarPath = selectedAvatarPath;

            employeeList.ResetItem(selectedRowIndex);
            UpdateStatusStrip();
            MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedRowIndex < 0 || selectedRowIndex >= employeeList.Count)
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            EmployeeModel emp = employeeList[selectedRowIndex];
            DialogResult result = MessageBox.Show(
                string.Format("Bạn có chắc chắn muốn xóa nhân viên [{0}] {1} không?", emp.EmployeeId, emp.FullName),
                "Cảnh báo",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                employeeList.RemoveAt(selectedRowIndex);
                UpdateStatusStrip();
                ClearForm();
                MessageBox.Show("Xóa nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(keyword))
            {
                dgvEmployees.DataSource = employeeList;
                return;
            }

            BindingList<EmployeeModel> filtered = new BindingList<EmployeeModel>();
            foreach (EmployeeModel emp in employeeList)
            {
                bool matchId = emp.EmployeeId != null && emp.EmployeeId.ToLower().Contains(keyword);
                bool matchName = emp.FullName != null && emp.FullName.ToLower().Contains(keyword);
                bool matchDept = emp.Department != null && emp.Department.ToLower().Contains(keyword);
                bool matchPos = emp.Position != null && emp.Position.ToLower().Contains(keyword);

                if (matchId || matchName || matchDept || matchPos)
                {
                    filtered.Add(emp);
                }
            }

            dgvEmployees.DataSource = filtered;
            lblStatusTotal.Text = string.Format("Tìm thấy: {0} nhân viên", filtered.Count);
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            dgvEmployees.DataSource = employeeList;
            UpdateStatusStrip();
        }

        private void mnuOpenCSV_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Mở danh sách nhân sự từ CSV";
                ofd.Filter = "CSV Files (*.csv)|*.csv";
                ofd.FilterIndex = 1;

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        employeeList.Clear();
                        string[] lines = File.ReadAllLines(ofd.FileName, System.Text.Encoding.UTF8);

                        foreach (string line in lines)
                        {
                            if (string.IsNullOrWhiteSpace(line))
                            {
                                continue;
                            }

                            if (line.TrimStart().StartsWith("EmployeeId") || line.TrimStart().StartsWith("Mã"))
                            {
                                continue;
                            }

                            EmployeeModel emp = EmployeeModel.FromCsvLine(line);
                            if (emp != null)
                            {
                                employeeList.Add(emp);
                            }
                        }

                        dgvEmployees.DataSource = employeeList;
                        UpdateStatusStrip();
                        ClearForm();
                        MessageBox.Show(
                            string.Format("Đã nhập {0} nhân viên từ file CSV.", employeeList.Count),
                            "Thành công",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi đọc file CSV: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void mnuSaveCSV_Click(object sender, EventArgs e)
        {
            if (employeeList.Count == 0)
            {
                MessageBox.Show("Danh sách nhân viên đang trống, không có gì để lưu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Title = "Lưu danh sách nhân sự ra CSV";
                sfd.Filter = "CSV Files (*.csv)|*.csv";
                sfd.FilterIndex = 1;
                sfd.DefaultExt = "csv";
                sfd.FileName = string.Format("DanhSachNhanSu_{0:yyyyMMdd}", DateTime.Today);

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (StreamWriter sw = new StreamWriter(sfd.FileName, false, System.Text.Encoding.UTF8))
                        {
                            sw.WriteLine("EmployeeId,FullName,Department,Position,BaseSalary,Allowance,AvatarPath");
                            foreach (EmployeeModel emp in employeeList)
                            {
                                sw.WriteLine(emp.ToCsvLine());
                            }
                        }

                        MessageBox.Show(
                            string.Format("Đã lưu {0} nhân viên vào file:\n{1}", employeeList.Count, sfd.FileName),
                            "Thành công",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi lưu file CSV: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn thoát chương trình không?",
                "Xác nhận thoát",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
