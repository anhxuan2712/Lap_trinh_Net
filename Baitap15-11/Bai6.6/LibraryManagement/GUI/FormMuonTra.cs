using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using LibraryManagement.BUS;
using LibraryManagement.DTO;

namespace LibraryManagement.GUI
{
    public partial class FormMuonTra : Form
    {
        private const string TatCa = "-- Tất cả --";

        private readonly PhieuMuonBUS phieuBus = new PhieuMuonBUS();
        private readonly DocGiaBUS docGiaBus = new DocGiaBUS();
        private readonly DauSachBUS dauSachBus = new DauSachBUS();

        private readonly BindingList<ChiTietMuonDTO> gioSach = new BindingList<ChiTietMuonDTO>();

        public FormMuonTra()
        {
            InitializeComponent();
        }

        private void FormMuonTra_Load(object sender, EventArgs e)
        {
            dtpHanTra.Value = DateTime.Today.AddDays(14);
            dgvSachMuon.DataSource = gioSach;

            cboLocTrangThai.Items.Add(TatCa);
            cboLocTrangThai.Items.AddRange(TrangThaiPhieu.TatCa);
            cboLocTrangThai.SelectedIndex = 0;

            LoadDocGia();
            LoadSach();
            LoadPhieuMuon();
        }

        private void LoadDocGia()
        {
            try
            {
                List<DocGiaDTO> ds = docGiaBus.GetAll();
                cboDocGia.DataSource = ds;

                List<DocGiaDTO> dsLoc = new List<DocGiaDTO> { new DocGiaDTO(0, TatCa, "", null) };
                dsLoc.AddRange(ds);
                cboLocDocGia.DataSource = dsLoc;
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private void LoadSach()
        {
            try
            {
                cboSach.DataSource = dauSachBus.GetAll();
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private void cboDocGia_SelectedIndexChanged(object sender, EventArgs e)
        {
            DocGiaDTO dg = cboDocGia.SelectedItem as DocGiaDTO;
            if (dg == null) { lblTinhTrangThe.Text = ""; return; }
            if (dg.TheConHan)
            {
                lblTinhTrangThe.Text = "Thẻ còn hạn đến " + dg.NgayHetHanThe.Value.ToString("dd/MM/yyyy");
                lblTinhTrangThe.ForeColor = Color.SeaGreen;
            }
            else
            {
                lblTinhTrangThe.Text = "Thẻ đã hết hạn – không được mượn";
                lblTinhTrangThe.ForeColor = Color.Firebrick;
            }
        }

        private void btnThemSach_Click(object sender, EventArgs e)
        {
            DauSachDTO sach = cboSach.SelectedItem as DauSachDTO;
            if (sach == null) { ShowError(new Exception("Vui lòng chọn sách!")); return; }

            int soLuong = (int)nudSLMuon.Value;
            ChiTietMuonDTO daCo = gioSach.FirstOrDefault(x => x.MaSach == sach.MaSach);
            int tong = soLuong + (daCo != null ? daCo.SoLuong : 0);
            if (tong > sach.SoLuongCon)
            {
                ShowError(new Exception("Sách \"" + sach.TenSach + "\" chỉ còn " + sach.SoLuongCon + " cuốn!"));
                return;
            }

            if (daCo != null)
            {
                daCo.SoLuong = tong;
                gioSach.ResetBindings();
            }
            else
            {
                gioSach.Add(new ChiTietMuonDTO(sach.MaSach, sach.TenSach, soLuong));
            }
        }

        private void btnBoSach_Click(object sender, EventArgs e)
        {
            if (dgvSachMuon.CurrentRow == null) return;
            ChiTietMuonDTO ct = dgvSachMuon.CurrentRow.DataBoundItem as ChiTietMuonDTO;
            if (ct != null) gioSach.Remove(ct);
        }

        private void btnLapPhieu_Click(object sender, EventArgs e)
        {
            try
            {
                DocGiaDTO dgChon = cboDocGia.SelectedItem as DocGiaDTO;
                int maDocGia = dgChon == null ? 0 : dgChon.MaDocGia;
                int maPhieu = phieuBus.LapPhieu(maDocGia, dtpHanTra.Value, gioSach.ToList());
                MessageBox.Show("Lập phiếu mượn thành công! Mã phiếu: " + maPhieu, "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                gioSach.Clear();
                LoadSach();
                LoadPhieuMuon();
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private void LoadPhieuMuon()
        {
            try
            {
                DocGiaDTO dgLoc = cboLocDocGia.SelectedItem as DocGiaDTO;
                int maDocGia = dgLoc == null ? 0 : dgLoc.MaDocGia;
                string trangThai = cboLocTrangThai.Text == TatCa ? "" : cboLocTrangThai.Text;
                dgvPhieuMuon.DataSource = phieuBus.Loc(maDocGia, trangThai);
                if (dgvPhieuMuon.Columns["NgayMuon"] != null)
                    dgvPhieuMuon.Columns["NgayMuon"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                if (dgvPhieuMuon.Columns["HanTra"] != null)
                    dgvPhieuMuon.Columns["HanTra"].DefaultCellStyle.Format = "dd/MM/yyyy";
                dgvChiTiet.DataSource = null;
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            LoadPhieuMuon();
        }

        private void btnCapNhatQuaHan_Click(object sender, EventArgs e)
        {
            try
            {
                int n = phieuBus.CapNhatQuaHan();
                MessageBox.Show("Đã chuyển " + n + " phiếu sang trạng thái \"Quá hạn\".", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadPhieuMuon();
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private void dgvPhieuMuon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            PhieuMuonDTO pm = dgvPhieuMuon.Rows[e.RowIndex].DataBoundItem as PhieuMuonDTO;
            if (pm == null) return;
            try
            {
                dgvChiTiet.DataSource = phieuBus.GetChiTiet(pm.MaPhieu);
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private void btnTraSach_Click(object sender, EventArgs e)
        {
            try
            {
                PhieuMuonDTO pm = dgvPhieuMuon.CurrentRow == null ? null : dgvPhieuMuon.CurrentRow.DataBoundItem as PhieuMuonDTO;
                if (pm == null) throw new Exception("Vui lòng chọn phiếu mượn cần trả!");
                if (pm.TrangThai == TrangThaiPhieu.DaTra) throw new Exception("Phiếu này đã được trả trước đó!");
                if (MessageBox.Show("Xác nhận trả sách cho phiếu #" + pm.MaPhieu + " (" + pm.HoTenDocGia + ")?",
                        "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

                if (phieuBus.TraSach(pm.MaPhieu))
                {
                    MessageBox.Show("Trả sách thành công! Số lượng sách đã được cộng lại vào kho.", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadSach();
                    LoadPhieuMuon();
                }
                else
                {
                    ShowError(new Exception("Không thể trả sách: phiếu không tồn tại hoặc đã trả."));
                }
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private void dgvPhieuMuon_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;
            PhieuMuonDTO pm = dgvPhieuMuon.Rows[e.RowIndex].DataBoundItem as PhieuMuonDTO;
            if (pm == null) return;
            if (pm.TrangThai == TrangThaiPhieu.QuaHan)
                e.CellStyle.ForeColor = Color.Firebrick;
            else if (pm.TrangThai == TrangThaiPhieu.DaTra)
                e.CellStyle.ForeColor = Color.Gray;
            else if (pm.HanTra < DateTime.Now)
                e.CellStyle.ForeColor = Color.DarkOrange;
        }

        private void ShowError(Exception ex)
        {
            MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
