using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bai_5._4_10_10
{
    public partial class Form1 : Form
    {
        private readonly List<Employee> employees = new List<Employee>();

        public Form1()
        {
            InitializeComponent();
            InitializeImages();
            InitializeEmployees();
            InitializeDepartmentTree();
            cboViewMode.SelectedIndex = 0;
        }

        private void InitializeImages()
        {
            imageListTree.Images.Add(SystemIcons.Application);
            imageListTree.Images.Add(SystemIcons.Information);
            imageListTree.Images.Add(SystemIcons.WinLogo);
            imageListEmployees.Images.Add(SystemIcons.Application);
        }

        private void InitializeEmployees()
        {
            employees.AddRange(new[]
            {
                new Employee("NV001", "Nguyễn Minh Anh", "Trưởng phòng", "Phòng Công nghệ thông tin", "Phát triển phần mềm", new DateTime(2018, 3, 12)),
                new Employee("NV002", "Trần Quốc Bảo", "Lập trình viên", "Phòng Công nghệ thông tin", "Phát triển phần mềm", new DateTime(2020, 6, 1)),
                new Employee("NV003", "Lê Thu Hà", "Kiểm thử viên", "Phòng Công nghệ thông tin", "Đảm bảo chất lượng", new DateTime(2021, 2, 15)),
                new Employee("NV004", "Phạm Đức Long", "Trưởng nhóm", "Phòng Công nghệ thông tin", "Hạ tầng hệ thống", new DateTime(2019, 9, 23)),
                new Employee("NV005", "Võ Thanh Tùng", "Quản trị hệ thống", "Phòng Công nghệ thông tin", "Hạ tầng hệ thống", new DateTime(2022, 4, 4)),
                new Employee("NV006", "Đặng Ngọc Mai", "Trưởng phòng", "Phòng Kinh doanh", "Khách hàng doanh nghiệp", new DateTime(2017, 7, 10)),
                new Employee("NV007", "Bùi Hoàng Nam", "Chuyên viên kinh doanh", "Phòng Kinh doanh", "Khách hàng doanh nghiệp", new DateTime(2020, 11, 2)),
                new Employee("NV008", "Phan Mỹ Linh", "Chuyên viên kinh doanh", "Phòng Kinh doanh", "Khách hàng cá nhân", new DateTime(2021, 5, 17)),
                new Employee("NV009", "Ngô Tuấn Kiệt", "Trưởng phòng", "Phòng Nhân sự", "Tuyển dụng", new DateTime(2016, 1, 18)),
                new Employee("NV010", "Đỗ Khánh Vy", "Chuyên viên tuyển dụng", "Phòng Nhân sự", "Tuyển dụng", new DateTime(2022, 8, 8)),
                new Employee("NV011", "Hồ Gia Huy", "Chuyên viên nhân sự", "Phòng Nhân sự", "Chế độ và phúc lợi", new DateTime(2020, 10, 12))
            });
        }

        private void InitializeDepartmentTree()
        {
            TreeNode companyNode = new TreeNode("Công ty ABC", 0, 0);

            AddDepartment(companyNode, "Phòng Công nghệ thông tin", "Phát triển phần mềm", "Đảm bảo chất lượng", "Hạ tầng hệ thống");
            AddDepartment(companyNode, "Phòng Kinh doanh", "Khách hàng doanh nghiệp", "Khách hàng cá nhân");
            AddDepartment(companyNode, "Phòng Nhân sự", "Tuyển dụng", "Chế độ và phúc lợi");

            tvDepartments.Nodes.Add(companyNode);
            companyNode.Expand();
            tvDepartments.SelectedNode = companyNode;
        }

        private void AddDepartment(TreeNode companyNode, string department, params string[] teams)
        {
            TreeNode departmentNode = new TreeNode(department, 1, 1);
            departmentNode.Tag = department;

            foreach (string team in teams)
            {
                TreeNode teamNode = new TreeNode(team, 2, 2);
                teamNode.Tag = new TeamSelection(department, team);
                departmentNode.Nodes.Add(teamNode);
            }

            companyNode.Nodes.Add(departmentNode);
        }

        private void tvDepartments_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string department = null;
            string team = null;

            if (e.Node.Tag is string)
            {
                department = (string)e.Node.Tag;
            }
            else if (e.Node.Tag is TeamSelection)
            {
                TeamSelection selection = (TeamSelection)e.Node.Tag;
                department = selection.Department;
                team = selection.Team;
            }

            IEnumerable<Employee> filteredEmployees = employees;
            if (department != null)
            {
                filteredEmployees = filteredEmployees.Where(employee => employee.Department == department);
            }
            if (team != null)
            {
                filteredEmployees = filteredEmployees.Where(employee => employee.Team == team);
            }

            lsvEmployees.BeginUpdate();
            lsvEmployees.Items.Clear();
            foreach (Employee employee in filteredEmployees)
            {
                ListViewItem item = new ListViewItem(employee.Id, 0);
                item.SubItems.Add(employee.FullName);
                item.SubItems.Add(employee.Position);
                item.SubItems.Add(employee.HireDate.ToString("dd/MM/yyyy"));
                lsvEmployees.Items.Add(item);
            }
            lsvEmployees.EndUpdate();
        }

        private void cboViewMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cboViewMode.SelectedItem as string)
            {
                case "SmallIcon":
                    lsvEmployees.View = View.SmallIcon;
                    break;
                case "LargeIcon":
                    lsvEmployees.View = View.LargeIcon;
                    break;
                case "Tile":
                    lsvEmployees.View = View.Tile;
                    break;
                case "List":
                    lsvEmployees.View = View.List;
                    break;
                default:
                    lsvEmployees.View = View.Details;
                    break;
            }
        }

        private sealed class Employee
        {
            public Employee(string id, string fullName, string position, string department, string team, DateTime hireDate)
            {
                Id = id;
                FullName = fullName;
                Position = position;
                Department = department;
                Team = team;
                HireDate = hireDate;
            }

            public string Id { get; private set; }
            public string FullName { get; private set; }
            public string Position { get; private set; }
            public string Department { get; private set; }
            public string Team { get; private set; }
            public DateTime HireDate { get; private set; }
        }

        private sealed class TeamSelection
        {
            public TeamSelection(string department, string team)
            {
                Department = department;
                Team = team;
            }

            public string Department { get; private set; }
            public string Team { get; private set; }
        }
    }
}
