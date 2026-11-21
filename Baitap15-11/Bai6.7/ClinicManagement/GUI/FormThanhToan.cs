using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using ClinicManagement.BUS;
using ClinicManagement.DTO;

namespace ClinicManagement.GUI
{
    public partial class FormThanhToan : Form
    {
        private const string TatCa = "-- Tất cả --";

        private readonly DonThuocBUS donBus = new DonThuocBUS();
        private readonly PrintDocument printDoc = new PrintDocument();

        private DonThuocDTO donDangChon;
        private List<ChiTietDonThuocDTO> chiTietDangChon = new List<ChiTietDonThuocDTO>();

        public FormThanhToan()
        {
            InitializeComponent();
            printDoc.PrintPage += PrintDoc_PrintPage;
        }

        private void FormThanhToan_Load(object sender, EventArgs e)
        {
            cboLocTrangThai.Items.AddRange(new object[] { TatCa, TrangThaiDon.ChoLayThuoc, TrangThaiDon.DaThanhToan });
            cboLocTrangThai.SelectedIndex = 1;
            LoadDon();
        }

        private void LoadDon()
        {
            try
            {
                string tt = cboLocTrangThai.Text == TatCa ? "" : cboLocTrangThai.Text;
                dgvDon.DataSource = donBus.GetDanhSach(tt);
                if (dgvDon.Columns["NgayKham"] != null) dgvDon.Columns["NgayKham"].DefaultCellStyle.Format = "dd/MM/yyyy";
                if (dgvDon.Columns["TienKham"] != null) dgvDon.Columns["TienKham"].DefaultCellStyle.Format = "N0";
                if (dgvDon.Columns["TongTien"] != null) dgvDon.Columns["TongTien"].DefaultCellStyle.Format = "N0";
                HienThiDon(null);
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private void HienThiDon(DonThuocDTO don)
        {
            donDangChon = don;
            if (don == null)
            {
                chiTietDangChon = new List<ChiTietDonThuocDTO>();
                dgvChiTiet.DataSource = null;
                lblThongTinBN.Text = "Chọn một đơn thuốc ở danh sách bên trái.";
                lblTienKhamTT.Text = lblTienThuoc.Text = lblTongCong.Text = "";
                btnThanhToan.Enabled = btnInHoaDon.Enabled = false;
                return;
            }

            chiTietDangChon = donBus.GetChiTiet(don.MaDon);
            dgvChiTiet.DataSource = chiTietDangChon;
            foreach (string col in new[] { "MaThuoc", "LieuDung", "DonViTinh" })
                if (dgvChiTiet.Columns[col] != null) dgvChiTiet.Columns[col].Visible = false;
            if (dgvChiTiet.Columns["DonGia"] != null) dgvChiTiet.Columns["DonGia"].DefaultCellStyle.Format = "N0";
            if (dgvChiTiet.Columns["ThanhTien"] != null) dgvChiTiet.Columns["ThanhTien"].DefaultCellStyle.Format = "N0";

            decimal tienThuoc = DonThuocBUS.TinhTongTien(0, chiTietDangChon);
            decimal tong = DonThuocBUS.TinhTongTien(don.TienKham, chiTietDangChon);

            lblThongTinBN.Text = "Đơn #" + don.MaDon + " – " + don.HoTenBN + " (" + don.SoDienThoai + ")" + Environment.NewLine +
                                 "Ngày khám: " + don.NgayKham.ToString("dd/MM/yyyy HH:mm") + Environment.NewLine +
                                 "Chẩn đoán: " + don.ChanDoan;
            lblTienKhamTT.Text = "Tiền khám:  " + don.TienKham.ToString("N0") + " đ";
            lblTienThuoc.Text = "Tiền thuốc: " + tienThuoc.ToString("N0") + " đ";
            lblTongCong.Text = "TỔNG CỘNG: " + tong.ToString("N0") + " đ";

            bool daThanhToan = don.TrangThai == TrangThaiDon.DaThanhToan;
            btnThanhToan.Enabled = !daThanhToan;
            btnInHoaDon.Enabled = true;
        }

        private void btnTaiLai_Click(object sender, EventArgs e)
        {
            LoadDon();
        }

        private void dgvDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            try
            {
                HienThiDon(dgvDon.Rows[e.RowIndex].DataBoundItem as DonThuocDTO);
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private void dgvDon_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DonThuocDTO d = dgvDon.Rows[e.RowIndex].DataBoundItem as DonThuocDTO;
            if (d == null) return;
            e.CellStyle.ForeColor = d.TrangThai == TrangThaiDon.DaThanhToan ? Color.SeaGreen : Color.DarkOrange;
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            try
            {
                if (donDangChon == null) throw new Exception("Vui lòng chọn đơn thuốc!");
                decimal tong = DonThuocBUS.TinhTongTien(donDangChon.TienKham, chiTietDangChon);
                if (MessageBox.Show("Xác nhận thanh toán đơn #" + donDangChon.MaDon + "\nTổng tiền: " + tong.ToString("N0") + " đ ?",
                        "Thanh toán", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

                DonThuocDTO don = donDangChon;
                donBus.ThanhToan(don);
                if (MessageBox.Show("Thanh toán thành công! Bạn có muốn in hóa đơn không?", "Thông báo",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    InHoaDon();
                }
                LoadDon();
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private void btnInHoaDon_Click(object sender, EventArgs e)
        {
            if (donDangChon == null) { ShowError(new Exception("Vui lòng chọn đơn thuốc!")); return; }
            InHoaDon();
        }

        private void InHoaDon()
        {
            using (PrintPreviewDialog dlg = new PrintPreviewDialog())
            {
                dlg.Document = printDoc;
                dlg.Width = 900;
                dlg.Height = 750;
                dlg.ShowDialog(this);
            }
        }

        private void PrintDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            DonThuocDTO d = donDangChon;
            if (d == null) return;
            Graphics g = e.Graphics;
            using (Font fTitle = new Font("Segoe UI", 18, FontStyle.Bold))
            using (Font fBold = new Font("Segoe UI", 11, FontStyle.Bold))
            using (Font f = new Font("Segoe UI", 11))
            {
                float x = 60, y = 50, w = e.PageBounds.Width - 120;
                StringFormat center = new StringFormat { Alignment = StringAlignment.Center };
                StringFormat right = new StringFormat { Alignment = StringAlignment.Far };

                g.DrawString("PHÒNG KHÁM ĐA KHOA", fBold, Brushes.Black, new RectangleF(x, y, w, 25), center); y += 30;
                g.DrawString("HÓA ĐƠN KHÁM BỆNH & THUỐC", fTitle, Brushes.Black, new RectangleF(x, y, w, 40), center); y += 50;

                g.DrawString("Số HĐ: " + d.MaDon + "        Ngày in: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm"), f, Brushes.Black, x, y); y += 25;
                g.DrawString("Bệnh nhân: " + d.HoTenBN + "        SĐT: " + d.SoDienThoai, f, Brushes.Black, x, y); y += 25;
                g.DrawString("Ngày khám: " + d.NgayKham.ToString("dd/MM/yyyy HH:mm"), f, Brushes.Black, x, y); y += 25;
                g.DrawString("Chẩn đoán: " + d.ChanDoan, f, Brushes.Black, new RectangleF(x, y, w, 45)); y += 50;

                float[] cols = { x, x + 40, x + 330, x + 420, x + 540 };
                g.DrawLine(Pens.Black, x, y, x + w, y); y += 5;
                g.DrawString("STT", fBold, Brushes.Black, cols[0], y);
                g.DrawString("Tên thuốc / Liều dùng", fBold, Brushes.Black, cols[1], y);
                g.DrawString("SL", fBold, Brushes.Black, cols[2], y);
                g.DrawString("Đơn giá", fBold, Brushes.Black, cols[3], y);
                g.DrawString("Thành tiền", fBold, Brushes.Black, new RectangleF(cols[4], y, x + w - cols[4], 25), right);
                y += 28;
                g.DrawLine(Pens.Black, x, y, x + w, y); y += 5;

                int stt = 1;
                foreach (ChiTietDonThuocDTO ct in chiTietDangChon)
                {
                    g.DrawString(stt++.ToString(), f, Brushes.Black, cols[0], y);
                    g.DrawString(ct.TenThuoc, f, Brushes.Black, cols[1], y);
                    g.DrawString(ct.SoLuong + " " + ct.DonViTinh, f, Brushes.Black, cols[2], y);
                    g.DrawString(ct.DonGia.ToString("N0"), f, Brushes.Black, cols[3], y);
                    g.DrawString(ct.ThanhTien.ToString("N0"), f, Brushes.Black, new RectangleF(cols[4], y, x + w - cols[4], 25), right);
                    y += 22;
                    if (!string.IsNullOrEmpty(ct.LieuDung))
                    {
                        g.DrawString("   Cách dùng: " + ct.LieuDung, f, Brushes.DimGray, cols[1], y);
                        y += 22;
                    }
                }
                g.DrawLine(Pens.Black, x, y, x + w, y); y += 10;

                decimal tienThuoc = DonThuocBUS.TinhTongTien(0, chiTietDangChon);
                decimal tong = d.TienKham + tienThuoc;
                g.DrawString("Tiền khám: " + d.TienKham.ToString("N0") + " đ", f, Brushes.Black, new RectangleF(x, y, w, 25), right); y += 25;
                g.DrawString("Tiền thuốc: " + tienThuoc.ToString("N0") + " đ", f, Brushes.Black, new RectangleF(x, y, w, 25), right); y += 30;
                g.DrawString("TỔNG CỘNG: " + tong.ToString("N0") + " đ", fTitle, Brushes.Black, new RectangleF(x, y, w, 40), right); y += 50;
                g.DrawString("Trạng thái: " + d.TrangThai, fBold, Brushes.Black, x, y); y += 60;
                g.DrawString("Người lập hóa đơn", fBold, Brushes.Black, new RectangleF(x, y, w, 25), right);
            }
        }

        private void ShowError(Exception ex)
        {
            MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
