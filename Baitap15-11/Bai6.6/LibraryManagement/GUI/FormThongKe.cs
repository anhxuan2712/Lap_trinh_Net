using System;
using System.Drawing;
using System.Windows.Forms;
using LibraryManagement.BUS;
using LibraryManagement.DTO;

namespace LibraryManagement.GUI
{
    public partial class FormThongKe : Form
    {
        private readonly PhieuMuonBUS phieuBus = new PhieuMuonBUS();
        private readonly DauSachBUS dauSachBus = new DauSachBUS();

        public FormThongKe()
        {
            InitializeComponent();
        }

        private void FormThongKe_Load(object sender, EventArgs e)
        {
            lvQuaHan.Columns.Add("Mã phiếu", 80);
            lvQuaHan.Columns.Add("Độc giả", 180);
            lvQuaHan.Columns.Add("Email", 200);
            lvQuaHan.Columns.Add("Ngày mượn", 110);
            lvQuaHan.Columns.Add("Hạn trả", 110);
            lvQuaHan.Columns.Add("Số ngày quá hạn", 120);
            lvQuaHan.Columns.Add("Trạng thái", 110);

            LoadQuaHan();
            LoadThongKe();
        }

        private void LoadQuaHan()
        {
            try
            {
                lvQuaHan.Items.Clear();
                foreach (PhieuMuonDTO pm in phieuBus.GetPhieuQuaHan())
                {
                    ListViewItem item = new ListViewItem(pm.MaPhieu.ToString());
                    item.SubItems.Add(pm.HoTenDocGia);
                    item.SubItems.Add(pm.Email);
                    item.SubItems.Add(pm.NgayMuon.ToString("dd/MM/yyyy"));
                    item.SubItems.Add(pm.HanTra.ToString("dd/MM/yyyy"));
                    item.SubItems.Add(pm.SoNgayQuaHan + " ngày");
                    item.SubItems.Add(pm.TrangThai);
                    item.ForeColor = pm.SoNgayQuaHan > 14 ? Color.Firebrick : Color.DarkOrange;
                    lvQuaHan.Items.Add(item);
                }
                lblTongQuaHan.Text = lvQuaHan.Items.Count == 0
                    ? "Không có phiếu mượn nào quá hạn."
                    : "Có " + lvQuaHan.Items.Count + " phiếu mượn đã quá hạn trả!";
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private void LoadThongKe()
        {
            try
            {
                dgvThongKe.DataSource = phieuBus.ThongKeTheoTheLoai();

                cboTheLoaiTK.Items.Clear();
                foreach (string tl in dauSachBus.GetDanhSachTheLoai()) cboTheLoaiTK.Items.Add(tl);
                if (cboTheLoaiTK.Items.Count > 0) cboTheLoaiTK.SelectedIndex = 0;

                lblTongPhieu.Text =
                    "Tổng số phiếu mượn: " + phieuBus.DemPhieu("") + Environment.NewLine +
                    "  - Đang mượn: " + phieuBus.DemPhieu(TrangThaiPhieu.DangMuon) + Environment.NewLine +
                    "  - Đã trả: " + phieuBus.DemPhieu(TrangThaiPhieu.DaTra) + Environment.NewLine +
                    "  - Quá hạn: " + phieuBus.DemPhieu(TrangThaiPhieu.QuaHan);
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private void btnTaiQuaHan_Click(object sender, EventArgs e)
        {
            LoadQuaHan();
            LoadThongKe();
        }

        private void btnDem_Click(object sender, EventArgs e)
        {
            try
            {
                string tl = cboTheLoaiTK.Text;
                int n = phieuBus.DemLuotMuonTheoTheLoai(tl);
                lblKetQuaScalar.Text = "Thể loại \"" + tl + "\" có " + n + " lượt mượn (ExecuteScalar).";
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private void ShowError(Exception ex)
        {
            MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
