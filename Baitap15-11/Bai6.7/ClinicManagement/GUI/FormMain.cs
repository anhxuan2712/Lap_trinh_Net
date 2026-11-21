using System;
using System.Windows.Forms;

namespace ClinicManagement.GUI
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
        }

        private void btnTiepNhan_Click(object sender, EventArgs e)
        {
            using (FormTiepNhan f = new FormTiepNhan()) f.ShowDialog(this);
        }

        private void btnKeDon_Click(object sender, EventArgs e)
        {
            using (FormKeDon f = new FormKeDon()) f.ShowDialog(this);
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            using (FormThanhToan f = new FormThanhToan()) f.ShowDialog(this);
        }

        private void btnThuoc_Click(object sender, EventArgs e)
        {
            using (FormThuoc f = new FormThuoc()) f.ShowDialog(this);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
