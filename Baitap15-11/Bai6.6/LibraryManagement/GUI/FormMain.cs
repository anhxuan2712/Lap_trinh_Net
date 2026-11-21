using System;
using System.Windows.Forms;

namespace LibraryManagement.GUI
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

        private void btnDauSach_Click(object sender, EventArgs e)
        {
            using (FormDauSach f = new FormDauSach()) f.ShowDialog(this);
        }

        private void btnDocGia_Click(object sender, EventArgs e)
        {
            using (FormDocGia f = new FormDocGia()) f.ShowDialog(this);
        }

        private void btnMuonTra_Click(object sender, EventArgs e)
        {
            using (FormMuonTra f = new FormMuonTra()) f.ShowDialog(this);
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            using (FormThongKe f = new FormThongKe()) f.ShowDialog(this);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
